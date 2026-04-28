using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using POS_Api.Services.Sync;
using POS_Common.Models.Sync.Custom.UpdateLocationsLastSeen;
using System;
using System.Threading.Tasks;

namespace POS_Api.Middleware;

public class SiteHeartbeatMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SiteHeartbeatMiddleware> _logger;
    private readonly IConfiguration _configuration;

    public SiteHeartbeatMiddleware(RequestDelegate next, ILogger<SiteHeartbeatMiddleware> logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Heartbeat only fires on authenticated /api/sync/* requests.
        if (context.User?.Identity?.IsAuthenticated == true &&
            context.Request.Path.StartsWithSegments("/api/sync", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                // Pull TenantID claim — per-tenant DB pattern: each tenant has its own DB,
                // and TenantID is the right key for the per-site POS_Locations row.
                var tenantClaim = context.User.FindFirst("TenantID")?.Value;
                if (int.TryParse(tenantClaim, out var siteId) && siteId > 0)
                {
                    var connectionString = _configuration.GetConnectionString($"ApplicationDb_{siteId}");
                    if (!string.IsNullOrWhiteSpace(connectionString))
                    {
                        await Sync_Custom_SP_Service.UpdateLocationsLastSeen(
                            new Req_UpdateLocationsLastSeen
                            {
                                SiteId = siteId,
                                SeenAt = DateTime.UtcNow,
                            },
                            connectionString);
                    }
                }
            }
            catch (Exception ex)
            {
                // Never fail the request on heartbeat issues — log and continue.
                _logger.LogWarning(ex, "SiteHeartbeatMiddleware failed to update LastSyncSeenAt for TenantID claim");
            }
        }

        await _next(context);
    }
}
