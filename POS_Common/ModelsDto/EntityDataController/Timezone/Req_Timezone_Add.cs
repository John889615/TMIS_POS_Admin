using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Timezone
{
    public class Req_Timezone_Add
    {
        #region Properties

        public string TimeZone { get; set; }

        public string UTCOffset { get; set; }

        public bool? ObservesDST { get; set; }
        #endregion
    }
}
