using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Menu.POS_Menus
{
   public class _Menu : _Menu_Base
    {
        #region Additional Properties
        
        public int? ItemID { get; set; }

        public string Item {  get; set; }

        public int? ParentItemID { get; set; }

        public string ParentItem { get; set; }

        public int? MenuItemProductID { get; set; }

        public int? ProductID { get; set; }

        public string Product { get; set; }

        public int? DebtorMenuID { get; set; }

        public int? FK_CostCenterID { get; set; }

        public int? FK_MenuID { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public int? FK_LocationID { get; set; }

        public int? DebtorID { get; set; }

        public int? CostCenterID { get; set; }

        public bool? Override { get; set; }

        public int? UserID { get; set; }
        #endregion
    }
}
