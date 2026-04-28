using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductSubstitution
{
    public class Res_ProductSubstitution_List
    {
        public int? ProductSubstitutionID { get; set; }
        public int? FK_ProductID { get; set; }
        public string ProductName { get; set; }
        public int? FK_ProductSubstitutionID { get; set; }
        public string ProductSubstitute { get; set; }
        public bool? IsQuantified { get; set; }
        public decimal? Quantity { get; set; }
        public bool? IsExtraCharge { get; set; }
    }
}
