using System;

namespace POS_Common.Models.Sync.Custom.SelectSiteSyncStatus
{
    public class Res_SelectSiteSyncStatus
    {
        #region Properties

        /// <summary>
        /// Maps to SiteId
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Maps to TypeName
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Maps to LastSuccessAt
        /// </summary>
        public DateTime? LastSuccessAt { get; set; }

        /// <summary>
        /// Maps to LastFailureAt
        /// </summary>
        public DateTime? LastFailureAt { get; set; }

        /// <summary>
        /// Maps to ConsecutiveFailures
        /// </summary>
        public int? ConsecutiveFailures { get; set; }

        /// <summary>
        /// Maps to LastErrorMessage
        /// </summary>
        public string LastErrorMessage { get; set; }

        /// <summary>
        /// Maps to LastReportedAt
        /// </summary>
        public DateTime? LastReportedAt { get; set; }

        /// <summary>
        /// Maps to AlertSentAt
        /// </summary>
        public DateTime? AlertSentAt { get; set; }

        #endregion
    }
}
