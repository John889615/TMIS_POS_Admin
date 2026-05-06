using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.BookingGuests
{
  public abstract class BookingGuest_Base
  {
       #region Properties
       
      public int? BookingGuestID { get; set; }

      public int? FK_GuestID { get; set; }

      public int? FK_BookingHeaderID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
