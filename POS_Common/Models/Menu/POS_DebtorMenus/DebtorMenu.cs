using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_DebtorMenus
{
   public class DebtorMenu : DebtorMenu_Base
    {
        #region Additional Properties
        
        public int? MenuID { get; set; }

        public int? ItemID { get; set; }

        public string Item { get; set; }

        public int? ParentItemID { get; set; }

        public string ParentItem { get; set; }

        public int? MenuItemProductID { get; set; }

        public int? ProductID { get; set; }

        public string Product { get; set; }

        public string SourceType { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }
        #endregion
    }
}
