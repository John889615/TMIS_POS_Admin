using System;

namespace POS_Common.Models.Sync.Custom.SelectLocationsSilentSites
{
    public class Req_SelectLocationsSilentSites
    {
        #region Properties

        /// <summary>
        /// Maps to @CutoffMinutes
        /// </summary>
        public int? CutoffMinutes { get; set; }

        #endregion
    }
}
