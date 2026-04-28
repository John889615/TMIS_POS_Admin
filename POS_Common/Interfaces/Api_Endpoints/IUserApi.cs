using POS_Common.Requests.Users;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Interfaces.Api_Endpoints
{
    public interface IUsersApi
    {
        #region Endpoints

        [Post("/api/users/login")]
        Task<ApiResponse<string>> LoginAsync(Login_Request request);

        [Post("/api/users/ping")]
        Task<ApiResponse<string>> Ping(Login_Request request);
        #endregion
    }
}
