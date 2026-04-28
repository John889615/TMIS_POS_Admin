using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralSettings
    {
        public string TenantId { get; set; } = "";
        public string Environment { get; set; } = "Production";
        public string CompanyId { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public string Authority { get; set; } = "https://login.microsoftonline.com";
        public string Scope { get; set; } = "https://api.businesscentral.dynamics.com/.default";
        public string BaseUrl { get; set; } = "https://api.businesscentral.dynamics.com/v2.0";
        public int RequestTimeoutSeconds { get; set; } = 100;

        public HostedServiceSettings HostedService { get; set; } = new HostedServiceSettings();

        public class HostedServiceSettings
        {
            public bool Enabled { get; set; } = true;
            public int IntervalSeconds { get; set; } = 300;
            public int InitialDelaySeconds { get; set; } = 5;
        }
    }
}
