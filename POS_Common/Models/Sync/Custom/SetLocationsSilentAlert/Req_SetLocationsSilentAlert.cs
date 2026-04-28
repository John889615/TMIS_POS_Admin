using System;

namespace POS_Common.Models.Sync.Custom.SetLocationsSilentAlert
{
    public class Req_SetLocationsSilentAlert
    {
        #region Properties

        /// <summary>
        /// Maps to @SiteId
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Maps to @AlertedAt
        /// </summary>
        public DateTime? AlertedAt { get; set; }

        #endregion
    }
}
