using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_Common.Requests.Users;
using Refit;

namespace POS_Common.Interfaces.Api_Endpoints
{
    public interface IDashboardApi
    {
        #region Endpoints

        [Get("/api/dashboard/ping")]
        Task<ApiResponse<string>> Ping();
        #endregion
    }
}
