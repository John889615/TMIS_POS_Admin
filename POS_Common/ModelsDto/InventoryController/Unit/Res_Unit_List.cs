using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.Unit
{
    public class Res_Unit_List
    {
        #region Properties

        public int? POS_UnitID { get; set; }

        public string Unit { get; set; }

        public string Symbol { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
