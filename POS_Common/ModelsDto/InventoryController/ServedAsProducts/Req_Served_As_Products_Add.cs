using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ServedAsProducts
{
    public class Req_Served_As_Products_Add
    {
        #region Properties

        public int ProductID { get; set; }

        public int ServedAsID { get; set; }
        
        public bool? IsQuantified { get; set; }

        public decimal? Quantity { get; set; }

        public bool? IsDefault { get; set; }
        #endregion
    }
}
