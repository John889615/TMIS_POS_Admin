using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.ModelsDto.Authenticate
{
    public class Data
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }

    public class Res_Authenticate
    {
        public bool Success { get; set; }
        public List<object> Messages { get; set; }
        public Data Data { get; set; }
        public List<object> Errors { get; set; }
        public object ErrorCode { get; set; }
        public int StatusCode { get; set; }
        public object Meta { get; set; }


        #region Properties

        public int UserId { get; set; } // The unique ID of the user

        public string Username { get; set; } // The username of the authenticated user

        public string Email { get; set; } // The email address of the authenticated user

        public string AccessToken { get; set; } // The generated JWT access token

        public string RefreshToken { get; set; } // The generated refresh token

        public DateTime AccessTokenExpiration { get; set; } // Expiration date/time of the access token

        public DateTime RefreshTokenExpiration { get; set; } // Expiration date/time of the refresh token
        #endregion
    }
}
