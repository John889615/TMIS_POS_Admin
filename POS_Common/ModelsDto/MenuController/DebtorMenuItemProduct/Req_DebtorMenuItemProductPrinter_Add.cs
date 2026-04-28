using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.DebtorMenuItemProduct
{
    public class Req_DebtorMenuItemProductPrinter_Add
    {
        public int? ProductID { get; set; }

        public int? MenuItemID { get; set; }

        public int? PrinterID { get; set; }
    }
}
