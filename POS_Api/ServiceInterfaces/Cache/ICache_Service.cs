using POS_Common.Models;
using POS_Common.Models.Cache;
using POS_Common.Models.EntityData.AddressRegions;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.Countries;
using POS_Common.Models.EntityData.CountryProvinces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.Cache
{
    public interface ICache_Service
    {
        Task<Cache_Lookup> GetCacheAsync(int tenantID);
        
        Task RefreshAsync(int tenantID);
    }
}
