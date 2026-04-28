using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_CostCenter_Sync
    {
        public int? CostCenterID { get; set; }

        public int? FK_LocationID { get; set; }

        public string Name { get; set; }

        public string BillingReference { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
