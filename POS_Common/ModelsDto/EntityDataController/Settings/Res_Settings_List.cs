using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Settings
{
    public class Res_Settings_List
    {
        #region Properties

        public int? SettingID { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string HeadOfficeNo { get; set; }

        public int? FK_CurrencyID { get; set; }

        public string Currency { get; set; }
        #endregion
    }
}
