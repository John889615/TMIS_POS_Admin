using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Interfaces.Api_Endpoints
{
    public interface IInventory
    {
        #region Endpoints

        [Get("/api/inventory/ping")]
        Task<ApiResponse<string>> Ping();
        #endregion
    }
}
