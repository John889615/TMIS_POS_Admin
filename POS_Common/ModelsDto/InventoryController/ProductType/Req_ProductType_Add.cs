using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductType
{
    public class Req_ProductType_Add
    {
        #region Properties

        public string ProductType { get; set; }

        public bool? IsInventory { get; set; }

        public bool? IsManufactured { get; set; }

        public bool? IsService { get; set; }

        public bool? IsComposite { get; set; }
        #endregion
    }
}
