using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_Arrivals
{
  public abstract class Arrival_Base
  {
       #region Properties
       
      public Guid? ArrivalID { get; set; }

      public int? FK_GuestID { get; set; }

      public string CheckedInBy { get; set; }

      public DateTime? CheckInDate { get; set; }

      public string CheckedOutBy { get; set; }

      public DateTime? CheckOutDate { get; set; }
       #endregion
  }
}
