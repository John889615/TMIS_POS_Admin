using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.Menu
{
    public class MenuTreeResponse
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public List<MenuItemNode> MenuItems { get; set; } = new List<MenuItemNode>();
    }

    public class MenuItemNode
    {
        public int ItemID { get; set; }
        public string Item { get; set; }
        public List<MenuItemNode> ChildItem { get; set; } = new List<MenuItemNode>();
        public List<MenuProduct> Product { get; set; } = new List<MenuProduct>();
    }

    public class MenuProduct
    {
        public int? POS_MenuItemProductID { get; set; }
        public int ProductID { get; set; }
        public string Product { get; set; }
    }
}
