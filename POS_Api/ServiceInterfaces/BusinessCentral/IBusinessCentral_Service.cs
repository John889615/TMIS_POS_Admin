using POS_Common.Models.BusinessCentral;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.ServiceInterfaces.BusinessCentral
{
    public interface IBusinessCentral_Service
    {
        Task<bool> PingAsync();
        Task<List<BusinessCentralDebtor>> GetDebtorsAsync();
        Task<List<BusinessCentralCreditor>> GetCreditorsAsync();
        Task<string> CreateInvoiceAsync();
        Task<bool> SyncAllAsync();
        Task<bool> SyncUnitsFromItemsAsync();
        Task<bool> SyncLocationsAsync();
        Task<bool> SyncProductCategoriesAsync();
        Task<bool> SyncPriceCodesAsync();
        Task<bool> SyncProductsAsync();
        Task<bool> SyncItemAvailabilityByLocationAsync();
        Task<bool> SyncSalesPricingAsync();
    }
}
