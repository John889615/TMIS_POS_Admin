using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Sync.POS_TabLines
{
  public abstract class TabLine_Base
  {
       #region Properties
       
      public Guid? TabLineID { get; set; }

      public Guid? FK_TabID { get; set; }

      public int? FK_ProductID { get; set; }

      public int? FK_PriceCodeID { get; set; }

      public Guid? FK_PointerID { get; set; }

      public decimal? UnitCostExcl { get; set; }

      public decimal? Vat { get; set; }

      public decimal? UnitCostIncl { get; set; }

      public string Product { get; set; }

      public decimal? Quantity { get; set; }

      public decimal? Discount { get; set; }

      public decimal? DiscountPerc { get; set; }

      public bool? IsVoided { get; set; }

      public string Notes { get; set; }

      public string AutoNotes { get; set; }

      public string CreatedBy { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }

      public string ServedAs { get; set; }

      public bool? ServedAsQuantified { get; set; }

      public decimal? ServedAsQuantity { get; set; }

      public int? FK_MenuID { get; set; }

      public string MenuName { get; set; }

      public decimal? Gratuity { get; set; }

      public decimal? GratuityPerc { get; set; }

      public int? FK_CostCenterID { get; set; }
       #endregion
  }
}
