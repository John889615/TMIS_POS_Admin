using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_TabLineGuests
{
  public abstract class TabLineGuest_Base
  {
       #region Properties
       
      public Guid? TabLineGuestID { get; set; }

      public Guid? FK_TabLineID { get; set; }

      public int? FK_GuestID { get; set; }

      public string Note { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
