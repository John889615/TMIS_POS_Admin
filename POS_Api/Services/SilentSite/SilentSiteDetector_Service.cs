using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POS_Api.Models.Email;
using POS_Api.ServiceInterfaces.Email;
using POS_Api.Services.Sync;
using POS_Common.Models.Sync.Custom.SelectLocationsSilentSites;
using POS_Common.Models.Sync.Custom.SetLocationsSilentAlert;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS_Api.Services.SilentSite;

public class SilentSiteDetector_Service : IHostedService, IDisposable
{
    private readonly IServiceProvider _services;
    private readonly IConfiguration _configuration;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);
    private readonly int _silentMinutes = 120; // 2 hours
    private CancellationTokenSource? _cts;

    public SilentSiteDetector_Service(IServiceProvider services, IConfiguration configuration)
    {
        _services = services;
        _configuration = configuration;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _ = RunLoopAsync(_cts.Token);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts?.Cancel();
        return Task.CompletedTask;
    }

    public void Dispose() => _cts?.Dispose();

    private async Task RunLoopAsync(CancellationToken token)
    {
        using var timer = new PeriodicTimer(_checkInterval);
        Log.Information("SilentSiteDetector started (interval {Min}min, threshold {SilentMin}min).",
            _checkInterval.TotalMinutes, _silentMinutes);

        try
        {
            do
            {
                try { await CheckOnceAsync(token); }
                catch (OperationCanceledException) { break; }
                catch (Exception ex) { Log.Error(ex, "SilentSiteDetector tick failed"); }
            }
            while (await timer.WaitForNextTickAsync(token));
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Log.Error(ex, "Fatal error in SilentSiteDetector loop");
        }
        finally { Log.Information("SilentSiteDetector stopped."); }
    }

    private async Task CheckOnceAsync(CancellationToken token)
    {
        // Single webservice serves all tenants; databases are per-tenant via
        // ConnectionStrings:ApplicationDb_<int>. Enumerate every configured
        // tenant DB and scan it for silent sites. Dedup by connection-string
        // value so two tenant keys pointing at the same DB don't get scanned twice.
        var tenantDbs = EnumerateTenantConnectionStrings();
        if (tenantDbs.Count == 0)
        {
            Log.Warning("SilentSiteDetector: no ApplicationDb_<int> connection strings configured; skipping tick.");
            return;
        }

        using var scope = _services.CreateScope();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmail_Service>();

        foreach (var (tenantId, connectionString) in tenantDbs)
        {
            token.ThrowIfCancellationRequested();
            try
            {
                await ScanTenantAsync(tenantId, connectionString, emailService, token);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                Log.Error(ex, "SilentSiteDetector: scan failed for tenant {TenantId}; continuing", tenantId);
            }
        }
    }

    private List<(int TenantId, string ConnectionString)> EnumerateTenantConnectionStrings()
    {
        var result = new List<(int, string)>();
        var seenConnStrs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        var section = _configuration.GetSection("ConnectionStrings");
        foreach (var child in section.GetChildren())
        {
            var key = child.Key ?? string.Empty;
            const string prefix = "ApplicationDb_";
            if (!key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) continue;

            var suffix = key.Substring(prefix.Length);
            if (!int.TryParse(suffix, out var tenantId) || tenantId <= 0) continue; // skips ApplicationDb_{Tenant} template

            var value = child.Value;
            if (string.IsNullOrWhiteSpace(value)) continue;
            if (!seenConnStrs.Add(value)) continue; // already scanned

            result.Add((tenantId, value));
        }

        return result;
    }

    private async Task ScanTenantAsync(int tenantId, string connectionString, IEmail_Service emailService, CancellationToken token)
    {
        var silentSites = await Sync_Custom_SP_Service.SelectLocationsSilentSites(
            new Req_SelectLocationsSilentSites { CutoffMinutes = _silentMinutes },
            connectionString);

        if (silentSites == null || silentSites.Count == 0) return;

        foreach (var site in silentSites)
        {
            token.ThrowIfCancellationRequested();

            // Stage 1: send the email. If this throws, no stamp — the next tick will retry.
            try
            {
                await emailService.Send_Site_Silent_Email(new SiteSilentEmail
                {
                    SiteId = site.LocationID,
                    SiteName = site.LocationName,
                    LastSeenAt = site.LastSyncSeenAt,
                    To = new[] { site.ContactEmail, site.SupportEmail }
                         .Where(s => !string.IsNullOrWhiteSpace(s)).ToList(),
                });
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to send silent-site email for tenant {TenantId} site {SiteId}", tenantId, site.LocationID);
                continue; // skip the stamp so we retry next tick
            }

            // Stage 2: stamp the alert in its own try so a logging hiccup or post-stamp failure
            // can't cause a re-send loop. If we got here, the email is gone — even if stamping
            // fails, accepting one duplicate next tick is better than losing the alert entirely.
            try
            {
                await Sync_Custom_SP_Service.SetLocationsSilentAlert(
                    new Req_SetLocationsSilentAlert { SiteId = site.LocationID, AlertedAt = DateTime.UtcNow },
                    connectionString);

                Log.Information("Silent-site alert sent for tenant {TenantId} site {SiteId}", tenantId, site.LocationID);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                Log.Error(ex, "Silent-site email sent but stamping SilentAlertSentAt failed for tenant {TenantId} site {SiteId}; next tick will re-send", tenantId, site.LocationID);
            }
        }
    }
}
