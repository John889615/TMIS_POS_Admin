using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_ProductCombinations
{
  public abstract class ProductCombination_Base
  {
       #region Properties
       
      public int? ProductCombinationID { get; set; }

      public int? FK_ProductID { get; set; }

      public int? FK_ProductItemID { get; set; }

      public bool? IsQuantified { get; set; }

      public decimal? Quantity { get; set; }

      public bool? IsOptional { get; set; }

      public bool? IsExtraCharge { get; set; }

      public int? DisplayOrder { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
