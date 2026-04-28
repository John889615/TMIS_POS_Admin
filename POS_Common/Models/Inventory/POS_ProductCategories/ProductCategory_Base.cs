using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_ProductCategories
{
  public abstract class ProductCategory_Base
  {
       #region Properties
       
      public int? ProductCategoryID { get; set; }

      public string CategoryName { get; set; }

      public int? FK_ProductCategoryID { get; set; }

      public string BC_ID { get; set; }

      public bool? IsMaster { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateAdded { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
