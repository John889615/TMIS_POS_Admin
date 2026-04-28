using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController.CostCenter
{
    public class Res_CostCenter_List
    {
        public int? CostCenterID { get; set; }

        public string Name { get; set; }

        public int? DebtorID { get; set; }

        public string Debtor { get; set; }

        public int? StatusID { get; set; }

        public string Status { get; set; }

        public int? CostCenterTypeID { get; set; }

        public string Type { get; set; }

        public string BillingReference { get; set; }
    }
}
