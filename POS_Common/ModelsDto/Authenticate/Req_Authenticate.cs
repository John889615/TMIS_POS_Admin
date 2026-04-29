using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.Authenticate
{
    public class Req_Authenticate
    {
        #region Properties

        public string Username { get; set; }

        public string Password { get; set; }

        public string? StaffCode { get; set; }

        public int? Pin { get; set; }
        #endregion
    }
}
