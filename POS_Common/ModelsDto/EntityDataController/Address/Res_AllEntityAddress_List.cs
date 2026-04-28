using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Address
{
    public class Res_AllEntityAddress_List
    {
        public int? EntityAddressID { get; set; }

        public int? FK_AddressTypeID { get; set; }

        public string AddressType { get; set; }

        public bool? IsPrimary { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public int? AddressID { get; set; }

        public int? FK_CountryID { get; set; }

        public string Country { get; set; }

        public int? FK_ProvinceID { get; set; }

        public string Province { get; set; }

        public int? FK_AddressRegionID { get; set; }

        public string AddressRegion { get; set; }

        public string StreetAddress { get; set; }

        public string Locality { get; set; }

        public string PostalCode { get; set; }

        public string Landmark { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string Notes { get; set; }
    }
}
