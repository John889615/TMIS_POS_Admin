using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController.CostCenterPrinter
{
    public class Req_CostCenterPrinter_Add
    {
        public int FK_CostCenterID { get; set; }
        public int FK_PrinterID { get; set; }

        public int? FK_InvoiceSlipTypeID { get; set; }

        public int? FK_TabSlipTypeID { get; set; }
    }
}
