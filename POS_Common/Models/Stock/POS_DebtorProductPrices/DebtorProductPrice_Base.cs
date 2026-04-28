using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_DebtorProductPrices
{
  public abstract class DebtorProductPrice_Base
  {
       #region Properties
       
      public int? DebtorProductPriceID { get; set; }

      public int? FK_DebtorProductID { get; set; }

      public int? FK_PriceCodeID { get; set; }

      public int? FK_TaxID { get; set; }

      public decimal? ItemPrice { get; set; }

      public bool? Inclusive { get; set; }

      public decimal? Vat { get; set; }

      public DateTime? StartDate { get; set; }

      public DateTime? EndDate { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_DefaultUnitID { get; set; }
       #endregion
  }
}
