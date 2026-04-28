using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_CostCenterProducts
{
  public abstract class CostCenterProduct_Base
  {
       #region Properties
       
      public int? CostCenterProductID { get; set; }

      public int? FK_ProductID { get; set; }

      public int? FK_CostCenterID { get; set; }

      public int? FK_TaxTypeID { get; set; }

      public decimal? Value { get; set; }

      public decimal? Vat { get; set; }

      public decimal? ItemPrice { get; set; }

      public int? FK_SellUnitID { get; set; }

      public decimal? QuantityOnHand { get; set; }

      public bool? IsAvailable { get; set; }

      public bool? IsActive { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
