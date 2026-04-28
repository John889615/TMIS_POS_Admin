using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_Menu_Sync
    {
        public int? POS_DebtorMenuID { get; set; }

        public int? FK_CostCenterID { get; set; }

        public int? FK_MenuID { get; set; }

        public string MenuName { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int? FK_LocationID { get; set; }
    }
}
