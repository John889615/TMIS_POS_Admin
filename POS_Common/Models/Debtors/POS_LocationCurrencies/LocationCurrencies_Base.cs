using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Debtors.POS_LocationCurrencies
{
  public abstract class LocationCurrencies_Base
  {
       #region Properties
       
      public int? LocationCurrencyID { get; set; }

      public int? FK_CurrencyID { get; set; }

      public int? FK_LocationID { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateCreated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
