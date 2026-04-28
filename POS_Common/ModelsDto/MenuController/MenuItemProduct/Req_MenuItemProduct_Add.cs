using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.MenuItemProduct
{
    public class Req_MenuItemProduct_Add
    {
        #region Properties

        public int? FK_MenuItemID { get; set; }

        public int? FK_ProductID { get; set; }
        #endregion
    }
}
