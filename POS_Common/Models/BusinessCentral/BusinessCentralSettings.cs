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

        // Spec 3 additions:
        public string PosCustomerNo { get; set; } = "";
        public int PushSweepIntervalHours { get; set; } = 6;

        /// <summary>
        /// Spec 3: when true (default), Bc_Push_Service calls
        /// Microsoft.NAV.shipAndInvoice to post the order so stock
        /// deducts immediately. When false, the order is created in
        /// BC and left Open - no posting, no stock decrement, no
        /// posted-invoice id. Use this only as a temporary bypass
        /// while BC config (General/VAT Posting Setup, blocked
        /// flags, missing G/L accounts) is fixed. Manual posting
        /// in BC required to actually deduct stock.
        /// </summary>
        public bool AutoPost { get; set; } = true;

        public HostedServiceSettings HostedService { get; set; } = new HostedServiceSettings();

        public class HostedServiceSettings
        {
            public bool Enabled { get; set; } = true;
            public int IntervalSeconds { get; set; } = 300;
            public int InitialDelaySeconds { get; set; } = 5;
        }
    }
}
