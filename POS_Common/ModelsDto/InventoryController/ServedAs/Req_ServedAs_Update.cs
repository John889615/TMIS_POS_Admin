using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ServedAs
{
    public class Req_ServedAs_Update
    {
        #region Properties

        public int? ServedAsID { get; set; }

        public string ServedAsType { get; set; }

        public string Name { get; set; }
        #endregion
    }
}
