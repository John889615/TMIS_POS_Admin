using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController
{
    public class Res_DialingCode_List
    {
        #region Properties

        public int? DialingCodeID { get; set; }

        public string DialingCode { get; set; }

        public string ISO2Code { get; set; }
        #endregion
    }
}
