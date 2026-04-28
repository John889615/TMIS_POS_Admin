using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.MenuItem
{
    public class Res_MenuItem_List
    {
        #region Properties

        public int? POS_MenuItemID { get; set; }

        public int? FK_MenuID { get; set; }

        public string MenuName { get; set; }

        public string Item { get; set; }

        public string Description { get; set; }

        public int? FK_ParentMenuItemID { get; set; }

        public string ParentItem { get; set; }
        #endregion
    }
}
