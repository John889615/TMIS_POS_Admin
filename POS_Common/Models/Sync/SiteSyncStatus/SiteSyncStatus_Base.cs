using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.SiteSyncStatus
{
  public abstract class SiteSyncStatus_Base
  {
       #region Properties
       
      public int? SiteId { get; set; }

      public string TypeName { get; set; }

      public DateTime? LastSuccessAt { get; set; }

      public DateTime? LastFailureAt { get; set; }

      public int? ConsecutiveFailures { get; set; }

      public string LastErrorMessage { get; set; }

      public DateTime? LastReportedAt { get; set; }

      public DateTime? AlertSentAt { get; set; }
       #endregion
  }
}
