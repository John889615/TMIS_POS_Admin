using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_Products
{
  public abstract class Product_Base
  {
       #region Properties
       
      public int? ProductID { get; set; }

      public string ProductName { get; set; }

      public string Description { get; set; }

      public string ItemNo { get; set; }

      public int? FK_ProductTypeID { get; set; }

      public bool? IsStockTracked { get; set; }

      public int? FK_UnitID { get; set; }

      public int? FK_ProductCategoryID { get; set; }

      public int? FK_DefaultUnitID { get; set; }

      public string BC_ID { get; set; }

      public string SKU { get; set; }

      public string Barcode { get; set; }

      public string QrCode { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateAdded { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
