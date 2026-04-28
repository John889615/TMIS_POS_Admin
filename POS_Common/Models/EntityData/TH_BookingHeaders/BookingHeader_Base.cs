using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.EntityData.TH_BookingHeaders
{
  public abstract class BookingHeader_Base
  {
       #region Properties
       
      public int? BookingHeaderID { get; set; }

      public string PartyName { get; set; }

      public string BookingReference { get; set; }

      public int? FK_AgentDebtorID { get; set; }

      public int? FK_BranchID { get; set; }

      public int? FK_DepartmentID { get; set; }

      public int? FK_CurrencyID { get; set; }

      public decimal? QuoteTotal { get; set; }

      public decimal? BookingTotal { get; set; }

      public int? FK_BookingStatusID { get; set; }

      public DateTime? TravelStart { get; set; }

      public DateTime? TravelEnd { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
