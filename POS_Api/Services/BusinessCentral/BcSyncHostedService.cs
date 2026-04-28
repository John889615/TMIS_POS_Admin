using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POS_Api.ServiceInterfaces.BusinessCentral;
using POS_Common.Models.BusinessCentral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.Services.BusinessCentral
{
    public class BcSyncHostedService : BackgroundService
    {
        private readonly ILogger<BcSyncHostedService> _logger;
        private readonly IConfiguration _config;
        private readonly IBusinessCentral_Service _bcService;

        public BcSyncHostedService(ILogger<BcSyncHostedService> logger, IConfiguration config, IBusinessCentral_Service bcService)
        {
            _logger = logger;
            _config = config;
            _bcService = bcService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_config["BusinessCentralSync"] == "false")
            {
                _logger.LogWarning("BusinessCentralSync config is deprecated. Please use BusinessCentral:HostedService:Enabled instead.");
                return;
            }

            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            if (!settings.HostedService.Enabled)
            {
                _logger.LogInformation("BcSyncHostedService disabled via config.");
                return;
            }

            if (settings.HostedService.InitialDelaySeconds > 0)
            {
                _logger.LogInformation("BcSyncHostedService initial delay {Seconds}s", settings.HostedService.InitialDelaySeconds);
                await Task.Delay(TimeSpan.FromSeconds(settings.HostedService.InitialDelaySeconds), stoppingToken);
            }

            var interval = TimeSpan.FromSeconds(Math.Max(30, settings.HostedService.IntervalSeconds));
            _logger.LogInformation("BcSyncHostedService running every {Seconds}s", interval.TotalSeconds);

            using var timer = new PeriodicTimer(interval);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // 👇 Replace this with your actual recurring job (e.g., push invoices / pull items)
                    var ok = await _bcService.PingAsync();
                    _logger.LogInformation("BC Ping result: {Result}", ok);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in BcSyncHostedService loop");
                }

                try
                {
                    await timer.WaitForNextTickAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
