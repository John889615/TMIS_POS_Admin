using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ServedAs
{
    public class Req_ServedAs_Add
    {
        #region Properties

        public string ServedAsType { get; set; }

        public string Name { get; set; }
        #endregion
    }
}
