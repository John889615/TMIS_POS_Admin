using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_DebtorMenuItemProducts
{
  public abstract class DebtorMenuItemProduct_Base
  {
       #region Properties
       
      public int? MenuItemProductID { get; set; }

      public int? FK_DebtorMenuItemID { get; set; }

      public int? FK_ProductID { get; set; }

      public bool? IsActive { get; set; }

      public DateTime? DateCreated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
