using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.TimeZones
{
  public abstract class _TimeZone_Base
  {
       #region Properties
       
      public int? TimeZoneID { get; set; }

      public string TimeZone { get; set; }

      public string UTCOffset { get; set; }

      public bool? ObservesDST { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
