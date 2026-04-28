using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.DebtorMenuPrinter
{
    public class Res_DebtorMenuPrinter_List
    {
        #region Properties

        public int? DebtorMenuPrinterID { get; set; }

        public int? FK_DebtorMenuID { get; set; }

        public int? FK_PrinterID { get; set; }

        public string PrinterName { get; set; }

        public int? FK_OrderSlipTypeID { get; set; }

        public string SlipCode { get; set; }

        public string SlipDescription { get; set; }
        #endregion
    }
}
