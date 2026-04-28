using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralItemUom
    {
        public string systemId { get; set; }
        public string itemNo { get; set; }
        public string description { get; set; }
        public string baseUnitOfMeasure { get; set; }
        public string salesUnitOfMeasure { get; set; }
        public string purchaseUnitOfMeasure { get; set; }
        public decimal? inventory { get; set; }
        public bool? blocked { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? unitCost { get; set; }
    }
}
