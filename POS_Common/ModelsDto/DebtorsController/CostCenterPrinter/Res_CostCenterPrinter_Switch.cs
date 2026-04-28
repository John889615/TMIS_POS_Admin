using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController.CostCenterPrinter
{
    public class Res_CostCenterPrinter_Switch
    {
        public bool? Success { get; set; }
        public bool? IsLinked { get; set; }
        public bool? DeleteBlocked { get; set; }
        public string ActionTaken { get; set; }
        public int? CostCenterPrinterID { get; set; }
        public string Message { get; set; }
    }
}
