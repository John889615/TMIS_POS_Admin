using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BcItemAvailabilityLocation
    {
        public string ItemNo { get; set; }
        public string LocationCode { get; set; }
        public decimal? OnHandQty { get; set; }
        public bool? HasStock { get; set; }
    }

}
