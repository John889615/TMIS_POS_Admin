using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.InventoryController.ProductExtraCategories
{
    public class Req_ProductExtraCategory_Add
    {
        public string Category { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
