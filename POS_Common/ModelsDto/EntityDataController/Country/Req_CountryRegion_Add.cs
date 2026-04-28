using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Country
{
    public class Req_CountryRegion_Add
    {
        #region Properties

        public string Region { get; set; }

        public int? FK_ContinentID { get; set; }
        #endregion
    }
}
