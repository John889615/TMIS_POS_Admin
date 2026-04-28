using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_ProductExtraCategories
{
  public abstract class ProductExtraCategory_Base
  {
       #region Properties
       
      public int? ProductExtraCategoryID { get; set; }

      public string Category { get; set; }

      public int? DisplayOrder { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
