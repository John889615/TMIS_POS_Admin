using System;

namespace POS_Common.Models.Sync.Custom.SelectLocationRecipients
{
    public class Res_SelectLocationRecipients
    {
        #region Properties

        /// <summary>
        /// Maps to SiteId
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Maps to SiteName
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Maps to ContactEmail
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Maps to SupportEmail
        /// </summary>
        public string SupportEmail { get; set; }

        #endregion
    }
}
