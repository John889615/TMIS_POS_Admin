using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductExtra
{
    public class Req_ProductExtra_Update
    {
        public int ProductExtraID { get; set; }
        public int? FK_ProductID { get; set; }
        public int? FK_ProductExtraCategoryID { get; set; }
        public int? FK_ProductExtraID { get; set; }
        public bool? IsQuantified { get; set; }
        public decimal? Quantity { get; set; }
        public bool? IsExtraCharge { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
