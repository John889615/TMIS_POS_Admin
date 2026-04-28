using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralItemWithPrices
    {
        // Item fields (copied)
        public string ID { get; set; }
        public string Number { get; set; }
        public string DisplayName { get; set; }
        public string ItemCategoryID { get; set; }
        public string ItemCategoryCode { get; set; }
        public bool? Blocked { get; set; }
        public decimal? Inventory { get; set; }
        public decimal? UnitCost { get; set; }
        public string BaseUnitOfMeasureID { get; set; }
        public string BaseUnitOfMeasureCode { get; set; }
        public bool? PriceIncludesVat { get; set; }

        // Linked sales prices (0..4)
        public List<BusinessCentralSalesPriceRow> Prices { get; set; } = new List<BusinessCentralSalesPriceRow>();
    }
}
