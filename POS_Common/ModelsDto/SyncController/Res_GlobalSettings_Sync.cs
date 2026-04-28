using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.SyncController
{
    public class Res_GlobalSettings_Sync
    {
        #region Properties

        public int? GlobalSettingID { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Environment { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
        #endregion
    }
}
