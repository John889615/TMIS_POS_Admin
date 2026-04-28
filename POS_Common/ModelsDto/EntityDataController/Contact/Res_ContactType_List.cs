using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController
{
    public class Res_ContactType_List
    {
        #region Properties

        public int? ContactTypeID { get; set; }

        public string Type { get; set; }

        public bool? IsPhoneNumberType { get; set; }

        public bool? IsEmailType { get; set; }
        #endregion
    }
}
