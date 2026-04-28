using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.EntityDataController.DialingCode
{
    public class Req_DialingCode_Update
    {
        #region Properties

        public int? DialingCodeID { get; set; }

        public string DialingCode { get; set; }

        public string ISO2Code { get; set; }
        #endregion
    }
}
