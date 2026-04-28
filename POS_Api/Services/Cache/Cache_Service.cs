using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using POS_Api.ServiceInterfaces.Cache;
using POS_Api.ServiceInterfaces.Logging;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.Models.Cache;
using POS_Common.Models.Creditors.CreditorTypeMappings;
using POS_Common.Models.Creditors.CreditorTypes;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.POS_CostCenters;
using POS_Common.Models.Debtors.POS_Locations;
using POS_Common.Models.EntityData.AddressRegions;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.ContactTypes;
using POS_Common.Models.EntityData.Countries;
using POS_Common.Models.EntityData.CountryProvinces;
using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.DialingCodes;
using POS_Common.Models.EntityData.GlobalSettings;
using POS_Common.Models.EntityData.POS_PaymentTypeIcons;
using POS_Common.Models.EntityData.POS_SlipTypes;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.Inventory.POS_ProductExtraCategories;
using POS_Common.Models.Inventory.POS_ProductPreparationMethods;
using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Stock.POS_PriceCodes;
using POS_Common.ModelsDto.CreditorsController.Creditor;
using POS_Common.ModelsDto.CreditorsController.CreditorAddress;
using POS_Common.ModelsDto.CreditorsController.CreditorContact;
using POS_Common.ModelsDto.CreditorsController.CreditorType;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Interfaces;

namespace POS_Api.Services.Cache
{
    public class Cache_Service : ICache_Service
    {
        #region Members

        private readonly IConfiguration _configuration;

        private readonly ConcurrentDictionary<int, Cache_Lookup> _cache = new ConcurrentDictionary<int, Cache_Lookup>();
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Cache_Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods

        #region Caching

        public async Task<Cache_Lookup> GetCacheAsync(int tenantID)
        {
            Cache_Lookup tenantCache;

            if (!_cache.TryGetValue(tenantID, out tenantCache))
            {
                tenantCache = await LoadTenantCacheAsync(tenantID);

                _cache.AddOrUpdate(tenantID, tenantCache, (key, oldValue) => tenantCache);
            }

            return tenantCache;
        }

        public async Task RefreshAsync(int tenantID)
        {
            Cache_Lookup tenantCache = await LoadTenantCacheAsync(tenantID);

            _cache.AddOrUpdate(tenantID, tenantCache, (key, oldValue) => tenantCache);
        }

        private async Task<Cache_Lookup> LoadTenantCacheAsync(int tenantID)
        {
            Cache_Lookup cache = new Cache_Lookup();

            cache.Countries = await EntityData.EntityData_Custom_Service.Countries_Select_All(new Country()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.Provinces = await EntityData.EntityData_Custom_Service.CountryProvinces_Select_All(new CountryProvince()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.AddressRegions = await EntityData.EntityData_Custom_Service.AddressRegions_Select_All(new AddressRegion()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.DialingCodes = await EntityData.EntityData_Custom_Service.DialingCodes_Select_All(new DialingCode()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.AddressTypes = await EntityData.EntityData_Custom_Service.AddressTypes_Select_All(new AddressType()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.CostCenter = await Debtors.Debtors_Custom_Service.POS_CostCenters_Select_All(new CostCenter()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.Debtor = await Debtors.Debtors_Custom_Service.Debtors_Select_All(new Debtor()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.User = await EntityData.EntityData_Custom_Service.Users_Select_All(new User()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.ContactTypes = await EntityData.EntityData_Custom_Service.ContactTypes_Select_All(new ContactType()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.Location = await Debtors.Debtors_Custom_Service.POS_Locations_Select_All(new Location()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.Tax = await EntityData.EntityData_Custom_Service.POS_TaxTypes_Select_All(new TaxType()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.Currencies = await EntityData.EntityData_Custom_Service.Currencies_Select_All(new Currency()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.Products = await Inventory.Inventory_Custom_Service.POS_Products_Select_All(new Product()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.ProductCategories = await Inventory.Inventory_Custom_Service.POS_ProductExtraCategories_Select_All(new ProductExtraCategory()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.ProductPreparationMethods = await Inventory.Inventory_Custom_Service.POS_ProductPreparationMethods_Select_All(new ProductPreparationMethod()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.PaymentTypeIcon = await EntityData.EntityData_Custom_Service.POS_PaymentTypeIcons_Select_All(new PaymentTypeIcon()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.PriceCodes = await Stock.Stock_Custom_Service.POS_PriceCodes_Select_All(new PriceCodes()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.GlobalSettings = await EntityData.EntityData_Custom_Service.GlobalSettings_Select_All(new GlobalSettings()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));

            cache.SlipTypes = await EntityData.EntityData_Custom_Service.POS_SlipTypes_Select_All(new SlipType()
                , _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", tenantID.ToString())));
            return cache;
        }
        #endregion

        #endregion
    }
}
