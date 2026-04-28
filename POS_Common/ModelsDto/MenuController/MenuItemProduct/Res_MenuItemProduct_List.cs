using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.MenuItemProduct
{
    public class Res_MenuItemProduct_List
    {
        #region Properties

        public int? POS_MenuItemProductID { get; set; }

        public int? FK_MenuItemID { get; set; }

        public string Item { get; set; }

        public int? FK_ProductID { get; set; }

        public string ProductName { get; set; }
        #endregion
    }
}
