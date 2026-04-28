using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.Settings
{
    public class Req_Settings_Update
    {
        #region Properties

        public int? SettingID { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string HeadOfficeNo { get; set; }

        public int? FK_CurrencyID { get; set; }

        public IFormFile ImageFile { get; set; }
        #endregion
    }
}
