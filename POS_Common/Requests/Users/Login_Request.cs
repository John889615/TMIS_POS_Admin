using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Requests.Users
{
    public class Login_Request
    {
        #region Properties

        public string Username { get; set; }

        public string Password { get; set; }
        #endregion
    }
}
