using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_Accounts
{
  public abstract class Account_Base
  {
       #region Properties
       
      public Guid? AccountID { get; set; }

      public string Name { get; set; }

      public int? FK_BookingHeaderID { get; set; }

      public bool? IsClosed { get; set; }

      public int? FK_ResponsibleID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
