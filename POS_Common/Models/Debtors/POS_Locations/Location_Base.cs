using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.POS_Locations
{
  public abstract class Location_Base
  {
       #region Properties
       
      public int? LocationID { get; set; }

      public int? FK_CurrencyID { get; set; }

      public string BC_ID { get; set; }

      public string ShortCode { get; set; }

      public string Name { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateCreated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public string ContactEmail { get; set; }

      public string SupportEmail { get; set; }

      public DateTime? LastSyncSeenAt { get; set; }

      public DateTime? SilentAlertSentAt { get; set; }
       #endregion
  }
}
