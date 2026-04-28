using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController
{
    public class Res_Timezone_List
    {
        public int TimeZoneID { get; set; }

        public string TimeZone { get; set; }

        public string UTCOffset { get; set; }

        public bool ObservesDST { get; set; }
    }
}
