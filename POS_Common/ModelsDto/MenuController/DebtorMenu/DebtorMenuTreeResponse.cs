using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.DebtorMenu
{
    public class DebtorMenuTreeResponse
    {
        public int DebtorMenuID { get; set; }
        public string MenuName { get; set; } 
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public List<DebtorMenuItemNode> MenuItems { get; set; } = new List<DebtorMenuItemNode>();
    }

    public class DebtorMenuItemNode
    {
        public int ItemID { get; set; }
        public string Item { get; set; }
        public List<DebtorMenuItemNode> ChildItem { get; set; } = new List<DebtorMenuItemNode>();
        public List<DebtorMenuProduct> Product { get; set; } = new List<DebtorMenuProduct>();
    }

    public class DebtorMenuProduct
    {
        public int? DebtorMenuItemProductID { get; set; }
        public int ProductID { get; set; }
        public string Product { get; set; }
    }
}
