using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_VoidLogs
{
  public abstract class VoidLog_Base
  {
       #region Properties
       
      public Guid? VoidLogID { get; set; }

      public Guid? FK_TabID { get; set; }

      public Guid? FK_TabLineID { get; set; }

      public string VoidedBy { get; set; }

      public string Note { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
