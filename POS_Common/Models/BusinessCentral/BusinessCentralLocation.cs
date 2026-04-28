using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralLocation
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string CountryRegionCode { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }

}
