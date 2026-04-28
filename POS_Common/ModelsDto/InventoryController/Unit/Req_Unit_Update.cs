using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.Unit
{
    public class Req_Unit_Update
    {
        #region Properties

        public int? POS_UnitID { get; set; }

        public string Unit { get; set; }

        public string Symbol { get; set; }
        #endregion
    }
}
