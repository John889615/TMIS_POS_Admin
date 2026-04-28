using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController
{
    public class Res_CountryProvince_List
    {
        #region Properties

        public int? CountryProvinceID { get; set; }

        public string ProvinceName { get; set; }

        public string ISO2Code { get; set; }

        public int? FK_CountryID { get; set; }
        #endregion
    }
}
