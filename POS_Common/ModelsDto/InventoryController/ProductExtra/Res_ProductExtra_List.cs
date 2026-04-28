using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductExtra
{
    public class Res_ProductExtra_List
    {
        public int? ProductExtraID { get; set; }
        public int? FK_ProductID { get; set; }
        public string ProductName { get; set; }
        public int? FK_ProductExtraCategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? FK_ProductExtraID { get; set; }
        public string ExtraName { get; set; }
        public bool? IsQuantified { get; set; }
        public decimal? Quantity { get; set; }
        public bool? IsExtraCharge { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
