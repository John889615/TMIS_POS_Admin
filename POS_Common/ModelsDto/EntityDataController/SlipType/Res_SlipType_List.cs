using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.SlipType
{
    public class Res_SlipType_List
    {
        #region Properties

        public int? SlipTypeID { get; set; }

        public string SlipType { get; set; }

        public string SlipCode { get; set; }

        public string Description { get; set; }
        #endregion
    }
}
