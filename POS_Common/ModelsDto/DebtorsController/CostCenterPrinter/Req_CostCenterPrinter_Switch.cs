using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController.CostCenterPrinter
{
    public class Req_CostCenterPrinter_Switch
    {
        public int FK_CostCenterID { get; set; }
        public int FK_PrinterID { get; set; }
    }
}
