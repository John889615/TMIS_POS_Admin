using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.BookingHeaders
{
  public abstract class BookingHeader_Base
  {
       #region Properties
       
      public int? BookingHeaderID { get; set; }

      public string PartyName { get; set; }

      public string BookingReference { get; set; }

      public DateTime? TravelStart { get; set; }

      public DateTime? TravelEnd { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public bool? IsStaffBooking { get; set; }
       #endregion
  }
}
