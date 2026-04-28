using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.DebtorMenuPrinter
{
    public class Req_DebtorMenuPrinter_Add
    {
        #region Properties

        public int? FK_DebtorMenuID { get; set; }

        public int? FK_PrinterID { get; set; }

        public int? FK_OrderSlipTypeID { get; set; }
        #endregion
    }
}
