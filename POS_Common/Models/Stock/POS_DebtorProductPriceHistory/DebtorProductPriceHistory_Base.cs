using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_DebtorProductPriceHistory
{
  public abstract class DebtorProductPriceHistory_Base
  {
       #region Properties
       
      public int? DebtorProductPriceHistoryID { get; set; }

      public int? FK_DebtorProductID { get; set; }

      public decimal? Value { get; set; }

      public decimal? Vat { get; set; }

      public decimal? ItemPrice { get; set; }

      public DateTime? ValidFrom { get; set; }

      public DateTime? ValidTo { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
