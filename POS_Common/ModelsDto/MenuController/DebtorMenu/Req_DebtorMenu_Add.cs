using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.DebtorMenu
{
    public class Req_DebtorMenu_Add
    {
        public int? MenuID { get; set; }

        public int? DebtorID { get; set; }

        public int? CostCenterID { get; set; }

        public string MenuName { get; set; }

        public string Description { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
    }
}
