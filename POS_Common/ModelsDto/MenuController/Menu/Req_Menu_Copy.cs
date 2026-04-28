using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.Menu
{
    public class Req_Menu_Copy
    {
        public int? SourceMenuID { get; set; }

        public int? TargetDebtorID { get; set; }

        public int? TargetCostCenterID { get; set; }

        //public int? DefaultSlipPrinterID { get; set; }

        public bool? Override { get; set; }
    }
}
