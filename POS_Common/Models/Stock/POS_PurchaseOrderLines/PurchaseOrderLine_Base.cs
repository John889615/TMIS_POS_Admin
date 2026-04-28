using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_PurchaseOrderLines
{
  public abstract class PurchaseOrderLine_Base
  {
       #region Properties
       
      public int? PurchaseOrderLineID { get; set; }

      public int? FK_PurchaseOrderID { get; set; }

      public int? FK_ProductID { get; set; }

      public decimal? Quantity { get; set; }

      public decimal? UnitCostIncl { get; set; }

      public decimal? UnitCostExcl { get; set; }

      public int? FK_TaxTypeID { get; set; }

      public decimal? TaxRate { get; set; }

      public decimal? TotalCostIncl { get; set; }

      public decimal? TotalCostExcl { get; set; }

      public string Notes { get; set; }

      public string ManagerNotes { get; set; }

      public bool? IsDeclined { get; set; }
       #endregion
  }
}
