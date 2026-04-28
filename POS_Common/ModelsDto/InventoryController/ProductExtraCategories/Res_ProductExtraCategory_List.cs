using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductExtraCategories
{
    public class Res_ProductExtraCategory_List
    {
        public int? ProductExtraCategoryID { get; set; }
        public string Category { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
