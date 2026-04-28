using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.DebtorsController.DebtorAddress
{
    public class Req_DebtorAddress_Add
    {
        #region Address

        public int? FK_CountryID { get; set; }

        public int? FK_ProvinceID { get; set; }

        public int? FK_AddressRegionID { get; set; }

        public string StreetAddress { get; set; }

        public string Locality { get; set; }

        public string PostalCode { get; set; }

        public string Landmark { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string Notes { get; set; }
        #endregion

        #region Entity Address

        public int? FK_DebtorID { get; set; }

        public int? FK_AddressTypeID { get; set; }

        public bool? IsPrimary { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
        #endregion
    }
}
