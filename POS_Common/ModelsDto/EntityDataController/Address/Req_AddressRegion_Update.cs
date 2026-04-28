using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Address
{
    public class Req_AddressRegion_Update
    {
        #region Properties

        public int? AddressRegionID { get; set; }

        public string RegionName { get; set; }

        public string Description { get; set; }

        public int? FK_CountryID { get; set; }

        public int? FK_ProvinceID { get; set; }
        #endregion
    }
}
