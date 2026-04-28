using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_ProductCostCenters
{
  public abstract class POS_ProductCostCenter_Base
  {
       #region Properties
       
      public int? POS_ProductCostCenterID { get; set; }

      public int? FK_ProductID { get; set; }

      public int? FK_CostCenterID { get; set; }

      public int? FK_TaxTypeID { get; set; }

      public decimal? Price { get; set; }

      public int? FK_SellUnitID { get; set; }

      public decimal? QuantityOnHand { get; set; }

      public bool? IsAvailable { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateAdded { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
