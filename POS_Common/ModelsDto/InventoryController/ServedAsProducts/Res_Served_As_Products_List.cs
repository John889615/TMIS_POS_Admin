using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ServedAsProducts
{
    public class Res_Served_As_Products_List
    {
        #region Properties

        public int ServedAsProductID { get; set; }

        public int ServedAsID { get; set; }

        public string ServedAsType { get; set; }

        public string Name { get; set; }

        public bool? IsQuantified { get; set; }

        public decimal ? Quantity { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool? IsDefault { get; set; }
        #endregion
    }
}
