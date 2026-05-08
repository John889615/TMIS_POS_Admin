using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POS_Api.ServiceInterfaces.BusinessCentral;
using POS_Common.Models.BusinessCentral;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services.BusinessCentral
{
    /// <summary>
    /// Spec 3: periodic BC push. Every PushSweepIntervalHours hours,
    /// selects unpushed paid invoices and pushes each via Bc_Push_Service.
    ///
    /// Set BusinessCentral:PushSweepIntervalHours = 0 to disable.
    /// Single-flight via SemaphoreSlim - if a sweep is already running
    /// the next tick is skipped.
    /// </summary>
    public class BcPushHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly ILogger<BcPushHostedService> _logger;

        private static readonly SemaphoreSlim _sweepSemaphore = new(1, 1);

        public BcPushHostedService(IServiceProvider serviceProvider, IConfiguration config, ILogger<BcPushHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _config = config;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            var hours = settings.PushSweepIntervalHours;
            if (hours <= 0)
            {
                _logger.LogInformation("BcPushHostedService disabled (PushSweepIntervalHours = {Hours}).", hours);
                return;
            }

            // Stagger initial sweep so it doesn't race the master-data pull
            // service on cold start.
            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken).ContinueWith(_ => { });

            var interval = TimeSpan.FromHours(hours);
            _logger.LogInformation("BcPushHostedService running every {Hours}h.", hours);

            using var timer = new PeriodicTimer(interval);
            do
            {
                try
                {
                    await RunSweepOnceAsync(stoppingToken);
                }
                catch (OperationCanceledException) { break; }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "BcPushHostedService unhandled sweep error.");
                }
            }
            while (await timer.WaitForNextTickAsync(stoppingToken));
        }

        private async Task RunSweepOnceAsync(CancellationToken token)
        {
            if (!await _sweepSemaphore.WaitAsync(0, token))
            {
                _logger.LogInformation("BC push sweep skipped - previous sweep still running.");
                return;
            }

            try
            {
                var candidates = await LoadCandidatesAsync(token);
                if (candidates.Count == 0)
                {
                    _logger.LogInformation("BC push sweep: no candidates.");
                    return;
                }

                _logger.LogInformation("BC push sweep: {Count} candidate(s).", candidates.Count);

                using var scope = _serviceProvider.CreateScope();
                var pushService = scope.ServiceProvider.GetRequiredService<IBc_Push_Service>();

                int succeeded = 0, failed = 0, alreadyPushed = 0;
                foreach (var invoiceId in candidates)
                {
                    token.ThrowIfCancellationRequested();
                    try
                    {
                        var resp = await pushService.PushInvoiceAsync(invoiceId, token);
                        if (resp?.Success == true)
                        {
                            if (resp.Data?.AlreadyPushed == true) alreadyPushed++;
                            else succeeded++;
                        }
                        else
                        {
                            failed++;
                        }
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogWarning(ex, "BC push for invoice {Id} threw.", invoiceId);
                    }
                }

                _logger.LogInformation("BC push sweep done. ok={Ok} alreadyPushed={Already} failed={Failed}.",
                    succeeded, alreadyPushed, failed);
            }
            finally
            {
                _sweepSemaphore.Release();
            }
        }

        private async Task<List<Guid>> LoadCandidatesAsync(CancellationToken token)
        {
            var ids = new List<Guid>();
            var connectionString = _config.GetConnectionString("ApplicationDb_1");
            using var conn = SqlClient.CreateInstance(connectionString);
            await conn.OpenAsync(token);

            using var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                conn, "POS_InvoiceHeader_BC_select_candidates",
                new SqlParameter { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@MaxRows", Value = 500 });

            while (await reader.ReadAsync(token))
            {
                ids.Add((Guid)reader["InvoiceHeaderID"]);
            }
            return ids;
        }
    }
}
