using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralSalesPriceList
    {
        public List<BusinessCentralSalesPriceRow> value { get; set; } = new List<BusinessCentralSalesPriceRow>();
    }

    public class BusinessCentralSalesPriceRow
    {
        public string systemId { get; set; }

        public string itemNo { get; set; }

        public string salesCode { get; set; }

        public string currencyCode { get; set; }

        public string unitOfMeasureCode { get; set; }

        public decimal? minimumQuantity { get; set; }

        public decimal? unitPrice { get; set; }

        public bool? priceIncludesVat { get; set; }

        public bool? allowInvoiceDisc { get; set; }

        public string vatBusPostingGrPrice { get; set; }

        public bool? allowLineDisc { get; set; }

        public string variantCode { get; set; }
    }
}
