using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Country
{
    public class Req_Country_Update
    {
        #region Properties

        public int? CountryID { get; set; }

        public string CountryName { get; set; }

        public string NativeName { get; set; }

        public string OfficialName { get; set; }

        public string ISO2Code { get; set; }

        public string ISO3Code { get; set; }

        public string PrimaryLanguageCode { get; set; }

        public short? NumericCode { get; set; }

        public int? FK_DialingCodeID { get; set; }

        public int? FK_CurrencyID { get; set; }

        public int? FK_CountryRegionID { get; set; }

        public int? FK_CountrySubregionID { get; set; }

        public int? FK_TimeZoneID { get; set; }
        #endregion
    }
}
