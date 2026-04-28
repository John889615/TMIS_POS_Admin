using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;
using POS_Api.ServiceInterfaces.Inventory;
using Microsoft.AspNetCore.Http;
using POS_Api.ServiceInterfaces.Logging;
using POS_Common.Enums;
using POS_Common.Models;
using System.Data;
using System.Security.Claims;
using TMIS_Common.Models;

using POS_Api.ServiceInterfaces.Finances;

namespace POS_Api.Services.Finances
{
    public class Finances_Service : Finances_Base_Service, IFinances_Service
    {
        #region properties
       #endregion
   }
}

