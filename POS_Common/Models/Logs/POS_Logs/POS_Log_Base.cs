using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Logs.POS_Logs
{
  public abstract class POS_Log_Base
  {
       #region Properties
       
      public int? AuditLogID { get; set; }

      public string Action { get; set; }

      public int? ItemID { get; set; }

      public string Item { get; set; }

      public int? FK_UserID { get; set; }

      public DateTime? ActionDate { get; set; }
       #endregion
  }
}
