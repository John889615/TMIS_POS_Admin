using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Inventory.POS_Products
{
   public class Product : Product_Base
    {
        #region Additional Properties
        
        public string ProductType { get; set; }

        public bool? IsInventory { get; set; }

        public bool? IsManufactured { get; set; }

        public bool? IsService { get; set; }

        public bool? IsComposite { get; set; }

        public string Unit { get; set; }

        public string Symbol { get; set; }

        public string ProductCategory { get; set; }

        public string DefaultUnit { get; set; }

        public string DefaultSymbol { get; set; }

        public string ImageUrl { get; set; }
        #endregion
    }
}
