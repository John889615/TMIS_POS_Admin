using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_AccountGuests
{
  public abstract class AccountGuest_Base
  {
       #region Properties
       
      public Guid? AccountGuestID { get; set; }

      public Guid? FK_AccountID { get; set; }

      public int? FK_GuestID { get; set; }

      public bool? IsResponsible { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
