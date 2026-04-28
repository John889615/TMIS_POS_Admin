using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_ProductPreparation
{
  public abstract class ProductPreparation_Base
  {
       #region Properties
       
      public int? ProductPreparationID { get; set; }

      public int? FK_ProductID { get; set; }

      public int? FK_ProductPreparationMethodID { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public DateTime? DateCreated { get; set; }

      public DateTime? DateUpdated { get; set; }
       #endregion
  }
}
