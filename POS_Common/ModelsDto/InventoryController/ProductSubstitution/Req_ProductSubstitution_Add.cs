using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductSubstitution
{
    public class Req_ProductSubstitution_Add
    {
        public int? FK_ProductID { get; set; }
        public int? FK_ProductSubstitutionID { get; set; }
        public bool? IsQuantified { get; set; }
        public decimal? Quantity { get; set; }
        public bool? IsExtraCharge { get; set; }
    }
}
