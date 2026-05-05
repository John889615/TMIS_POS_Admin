using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_MenuItemProducts
{
  public abstract class MenuItemProduct_Base
  {
       #region Properties
       
      public int? MenuItemProductID { get; set; }

      public int? FK_MenuItemID { get; set; }

      public int? FK_ProductID { get; set; }

      public DateTime? DateCreated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_UpdatedUserID { get; set; }

      public int? DisplayOrder { get; set; }
       #endregion
  }
}
