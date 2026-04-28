using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_MenuItems
{
  public abstract class MenuItem_Base
  {
       #region Properties
       
      public int? MenuItemID { get; set; }

      public int? FK_MenuID { get; set; }

      public string Item { get; set; }

      public string Description { get; set; }

      public int? FK_MenuItemID { get; set; }

      public DateTime? DateCreated { get; set; }

      public int? FK_CreatedUserID { get; set; }

      public DateTime? DateUpdated { get; set; }

      public int? FK_UpdatedUserID { get; set; }
       #endregion
  }
}
