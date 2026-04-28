using System;

namespace POS_Common.Models.Sync.Custom.SelectLocationsSilentSites
{
    public class Res_SelectLocationsSilentSites
    {
        #region Properties

        /// <summary>
        /// Maps to LocationID
        /// </summary>
        public int? LocationID { get; set; }

        /// <summary>
        /// Maps to LocationName
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Maps to LastSyncSeenAt
        /// </summary>
        public DateTime? LastSyncSeenAt { get; set; }

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
