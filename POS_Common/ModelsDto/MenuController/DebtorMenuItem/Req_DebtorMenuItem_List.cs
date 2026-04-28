using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.MenuController.DebtorMenuItem
{
    public class Req_DebtorMenuItem_List
    {
        public int? MenuID { get; set; }

        public bool? IsDefault { get; set; }
    }
}
