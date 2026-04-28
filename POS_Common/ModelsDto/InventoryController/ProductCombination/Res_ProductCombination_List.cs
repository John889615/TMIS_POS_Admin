using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductCombination
{
    public class Res_ProductCombination_List
    {
        public int? ProductCombinationID { get; set; }
        public int? FK_ProductItemID { get; set; }
        public string ProductItemName { get; set; }
        public bool? IsQuantified { get; set; }
        public decimal? Quantity { get; set; }
        public bool? IsOptional { get; set; }
        public bool? IsExtraCharge { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
