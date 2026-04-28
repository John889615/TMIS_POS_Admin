using System;

namespace POS_Common.Models.Sync.Custom.UpdateLocationsLastSeen
{
    public class Req_UpdateLocationsLastSeen
    {
        #region Properties

        /// <summary>
        /// Maps to @SiteId
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Maps to @SeenAt
        /// </summary>
        public DateTime? SeenAt { get; set; }

        #endregion
    }
}
