using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Country
{
    public class Req_CountrySubRegion_Add
    {
        #region Properties

        public string Subregion { get; set; }

        public int? FK_CountryRegionID { get; set; }
        #endregion
    }
}
