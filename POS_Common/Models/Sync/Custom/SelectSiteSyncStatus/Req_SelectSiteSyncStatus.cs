using System;

namespace POS_Common.Models.Sync.Custom.SelectSiteSyncStatus
{
    public class Req_SelectSiteSyncStatus
    {
        #region Properties

        /// <summary>
        /// Maps to @SiteId
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Maps to @TypeName
        /// </summary>
        public string TypeName { get; set; }

        #endregion
    }
}
