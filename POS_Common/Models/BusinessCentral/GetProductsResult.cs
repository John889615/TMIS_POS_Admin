using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class GetProductsResult
    {
        public List<BusinessCentralItem> Items { get; set; } = new List<BusinessCentralItem>();
        public List<BusinessCentralItemCategory> Categories { get; set; } = new List<BusinessCentralItemCategory>();
        public List<BusinessCentralSalesPriceRow> SalesPrices { get; set; } = new List<BusinessCentralSalesPriceRow>();

        // ✅ Added combined list
        public List<BusinessCentralItemWithPrices> ItemsWithPrices { get; set; } = new List<BusinessCentralItemWithPrices>();
    }
}
