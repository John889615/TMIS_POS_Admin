using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using POS_Api.ServiceInterfaces.BusinessCentral;
using POS_Common.Models.BusinessCentral;
using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.POS_CostCenters;
using POS_Common.Models.Debtors.POS_Locations;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.Inventory.POS_ProductCategories;
using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Inventory.POS_ProductTypes;
using POS_Common.Models.Inventory.POS_Units;
using POS_Common.Models.Menu.POS_Menus;
using POS_Common.Models.Stock.POS_DebtorProducts;
using POS_Common.Models.Stock.POS_PriceCodes;
using POS_Common.Models.Sync.POS_InvoiceHeaders;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TMIS_Common.Interfaces;
using TMIS_Common.Sql;

namespace POS_Api.Services.BusinessCentral
{
    public class BusinessCentral_Service : BusinessCentral_Custom_Service, IBusinessCentral_Service
    {
        #region Members

        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<BusinessCentral_Service> _logger;
        private readonly IBcTokenProvider _tokenProvider;
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public BusinessCentral_Service(
            IHttpClientFactory httpFactory,
            IConfiguration config,
            ILogger<BusinessCentral_Service> logger,
            IBcTokenProvider tokenProvider)
        {
            _httpFactory = httpFactory;
            _config = config;
            _logger = logger;
            _tokenProvider = tokenProvider;
        }
        #endregion

        #region Helper Methods

        private void LogSyncError(string methodName, Exception ex)
        {
            _logger.LogError(ex, "BC Sync failed in {Method}: {Message}", methodName, ex.Message);
        }

        private static DataTable BuildBulkInsertTable()
        {
            var dt = new DataTable();

            dt.Columns.Add("String1", typeof(string));
            dt.Columns.Add("String2", typeof(string));
            dt.Columns.Add("String3", typeof(string));
            dt.Columns.Add("String4", typeof(string));
            dt.Columns.Add("String5", typeof(string));

            dt.Columns.Add("Int1", typeof(int));
            dt.Columns.Add("Int2", typeof(int));
            dt.Columns.Add("Int3", typeof(int));
            dt.Columns.Add("Int4", typeof(int));
            dt.Columns.Add("Int5", typeof(int));

            dt.Columns.Add("Decimal1", typeof(decimal));
            dt.Columns.Add("Decimal2", typeof(decimal));
            dt.Columns.Add("Decimal3", typeof(decimal));
            dt.Columns.Add("Decimal4", typeof(decimal));
            dt.Columns.Add("Decimal5", typeof(decimal));

            dt.Columns.Add("Date1", typeof(DateTime));
            dt.Columns.Add("Date2", typeof(DateTime));
            dt.Columns.Add("Date3", typeof(DateTime));

            dt.Columns.Add("Bit1", typeof(bool));
            dt.Columns.Add("Bit2", typeof(bool));
            dt.Columns.Add("Bit3", typeof(bool));

            return dt;
        }

        private static SqlParameter AsBulkInsertParam(string name, DataTable dt)
        {
            return new SqlParameter(name, SqlDbType.Structured)
            {
                TypeName = "dbo.BulkInsert",
                Value = dt
            };
        }

        #endregion

        #region Methods

        #region Pull Requests

        public async Task<bool> SyncAllAsync()
        {
            try
            {
                using var sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1"));
                if (sqlConn.State != ConnectionState.Open) await sqlConn.OpenAsync();

                // ✅ FK-safe order
                if (!await SyncUnitsFromItems(sqlConn)) return false;
                if (!await SyncLocations(sqlConn)) return false;
                if (!await SyncProductCategories(sqlConn)) return false;
                if (!await SyncPriceCodes(sqlConn)) return false;

                // optional
                // if (!await SyncTaxTypes_BulkAsync(sqlConn)) return false;

                if (!await SyncProducts(sqlConn)) return false;
                if (!await SyncItemAvailabilityByLocation(sqlConn)) return false;
                if (!await SyncSalesPricing(sqlConn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BC SyncAllAsync bulk failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncUnitsFromItemsAsync()
        {
            try
            {
                using var sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1"));
                if (sqlConn.State != ConnectionState.Open) await sqlConn.OpenAsync();

                // ✅ FK-safe order
                if (!await SyncUnitsFromItems(sqlConn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BC SyncAllAsync bulk failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncLocationsAsync()
        {
            try
            {
                using var sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1"));
                if (sqlConn.State != ConnectionState.Open) await sqlConn.OpenAsync();

                // ✅ FK-safe order
                if (!await SyncLocations(sqlConn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BC SyncAllAsync bulk failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncProductCategoriesAsync()
        {
            try
            {
                using var sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1"));
                if (sqlConn.State != ConnectionState.Open) await sqlConn.OpenAsync();

                // ✅ FK-safe order
                if (!await SyncProductCategories(sqlConn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BC SyncAllAsync bulk failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncPriceCodesAsync()
        {
            try
            {
                using var sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1"));
                if (sqlConn.State != ConnectionState.Open) await sqlConn.OpenAsync();

                // ✅ FK-safe order
                if (!await SyncPriceCodes(sqlConn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BC SyncAllAsync bulk failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncProductsAsync()
        {
            try
            {
                using var sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1"));
                if (sqlConn.State != ConnectionState.Open) await sqlConn.OpenAsync();

                if (!await SyncUnitsFromItems(sqlConn)) return false;
                if (!await SyncProductCategories(sqlConn)) return false;
                if (!await SyncProducts(sqlConn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BC SyncAllAsync bulk failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncItemAvailabilityByLocationAsync()
        {
            try
            {
                using var sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1"));
                if (sqlConn.State != ConnectionState.Open) await sqlConn.OpenAsync();

                // ✅ FK-safe order
                //if (!await SyncUnitsFromItems(sqlConn)) return false;
                //if (!await SyncLocations(sqlConn)) return false;
                //if (!await SyncProductCategories(sqlConn)) return false;
                //if (!await SyncProducts(sqlConn)) return false;
                if (!await SyncItemAvailabilityByLocation(sqlConn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BC SyncAllAsync bulk failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncSalesPricingAsync()
        {
            try
            {
                using var sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1"));
                if (sqlConn.State != ConnectionState.Open) await sqlConn.OpenAsync();

                //if (!await SyncUnitsFromItems(sqlConn)) return false;
                //if (!await SyncLocations(sqlConn)) return false;
                //if (!await SyncProductCategories(sqlConn)) return false;
                //if (!await SyncPriceCodes(sqlConn)) return false;
                if (!await SyncProducts(sqlConn)) return false;
                if (!await SyncItemAvailabilityByLocation(sqlConn)) return false;
                if (!await SyncSalesPricing(sqlConn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BC SyncAllAsync bulk failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncUnitsFromItems(SqlConnection sqlConn)
        {
            try
            {
                var settings = new BusinessCentralSettings();
                _config.GetSection("BusinessCentral").Bind(settings);

                var token = await _tokenProvider.GetAccessTokenAsync();

                var itemsUrl =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/v2.0/companies({settings.CompanyId})/items";

                var client = _httpFactory.CreateClient("bc");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                using var itemRes = await client.GetAsync(itemsUrl);
                var itemJson = await itemRes.Content.ReadAsStringAsync();

                if (!itemRes.IsSuccessStatusCode)
                    throw new Exception($"BC GetItems failed: {itemRes.StatusCode} | {itemJson}");

                var itemRoot = JsonSerializer.Deserialize<BusinessCentralItemList>(
                    itemJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var items = itemRoot?.Value ?? new List<BusinessCentralItem>();

                var test = items.Where(x => x.Number == "100683").ToList();

                var distinctUnits = items
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(x.BaseUnitOfMeasureID) &&
                        !string.IsNullOrWhiteSpace(x.BaseUnitOfMeasureCode))
                    .Select(x => new
                    {
                        BC_ID = x.BaseUnitOfMeasureID!,
                        Unit = x.BaseUnitOfMeasureCode!
                    })
                    .Distinct()
                    .ToList();

                // ✅ Build TVP (dbo.BulkInsert)
                var tvp = BuildBulkInsertTable();

                foreach (var u in distinctUnits)
                {
                    var row = tvp.NewRow();

                    row["String1"] = u.BC_ID;     // BC_ID
                    row["String2"] = u.Unit;      // Unit
                    row["String3"] = u.Unit; // Symbol optional
                    row["Bit1"] = true;           // IsActive

                    // leave others DBNull
                    tvp.Rows.Add(row);
                }

                using var cmd = new SqlCommand("dbo.BulkUpsert_POS_Units", sqlConn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add(AsBulkInsertParam("@Rows", tvp));
                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SyncUnitsFromItemsAsync (Bulk) failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncLocations(SqlConnection sqlConn)
        {
            try
            {
                var settings = _config.GetSection("BusinessCentral").Get<BusinessCentralSettings>();
                var token = await _tokenProvider.GetAccessTokenAsync();

                var url =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                    $"/api/v2.0/companies({settings.CompanyId})/locations";

                var client = _httpFactory.CreateClient("bc");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var res = await client.GetAsync(url);
                var json = await res.Content.ReadAsStringAsync();

                if (!res.IsSuccessStatusCode)
                    throw new Exception($"BC locations failed: {(int)res.StatusCode} {res.ReasonPhrase} | {json}");

                var root = JsonSerializer.Deserialize<BusinessCentralODataResponse<BusinessCentralLocation>>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var tvp = BuildBulkInsertTable();

                foreach (var loc in root?.Value ?? new List<BusinessCentralLocation>())
                {
                    var row = tvp.NewRow();

                    row["String1"] = loc.Id ?? "";              // BC_ID
                    row["String2"] = loc.Code ?? "";            // ShortCode
                    row["String3"] = loc.DisplayName ?? "";     // Name

                    row["Int1"] = 140;                          // FK_CurrencyID
                    row["Int2"] = 1;                            // FK_CreatedUserID
                    row["Int3"] = 1;                            // FK_UpdatedUserID

                    row["Bit1"] = true;                         // IsActive

                    tvp.Rows.Add(row);
                }

                using var cmd = new SqlCommand("dbo.BulkUpsert_POS_Locations", sqlConn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add(AsBulkInsertParam("@Rows", tvp));
                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SyncLocationsAsync (Bulk) failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncProductCategories(SqlConnection sqlConn)
        {
            try
            {
                var settings = new BusinessCentralSettings();
                _config.GetSection("BusinessCentral").Bind(settings);

                var token = await _tokenProvider.GetAccessTokenAsync();

                var url =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/v2.0/companies({settings.CompanyId})/itemCategories";

                var client = _httpFactory.CreateClient("bc");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                using var res = await client.GetAsync(url);
                var json = await res.Content.ReadAsStringAsync();

                if (!res.IsSuccessStatusCode)
                    throw new Exception($"BC itemCategories failed: {res.StatusCode} | {json}");

                var root = JsonSerializer.Deserialize<BusinessCentralItemCategoryList>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var tvp = BuildBulkInsertTable();

                foreach (var cat in root?.Value ?? new List<BusinessCentralItemCategory>())
                {
                    var row = tvp.NewRow();

                    row["String1"] = cat.ID ?? "";            // BC_ID
                    row["String2"] = cat.DisplayName ?? "";   // CategoryName

                    row["Int1"] = 0;                          // ParentCategoryID (none unless you have hierarchy)
                    row["Bit1"] = true;                       // IsMaster
                    row["Bit2"] = true;                       // IsActive

                    tvp.Rows.Add(row);
                }

                using var cmd = new SqlCommand("dbo.BulkUpsert_POS_ProductCategories", sqlConn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add(AsBulkInsertParam("@Rows", tvp));
                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SyncProductCategoriesAsync (Bulk) failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncPriceCodes(SqlConnection sqlConn)
        {
            try
            {
                var settings = new BusinessCentralSettings();
                _config.GetSection("BusinessCentral").Bind(settings);

                var token = await _tokenProvider.GetAccessTokenAsync();

                var client = _httpFactory.CreateClient("bc");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var url =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                    $"/api/bluesafari/pricing/v1.0/companies({settings.CompanyId})/salesPrices7007";

                var distinctCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                while (!string.IsNullOrWhiteSpace(url))
                {
                    using var res = await client.GetAsync(url);
                    var json = await res.Content.ReadAsStringAsync();

                    if (!res.IsSuccessStatusCode)
                        throw new Exception($"BC salesPrices7007 failed: {res.StatusCode} | {json}");

                    using var doc = JsonDocument.Parse(json);

                    if (doc.RootElement.TryGetProperty("value", out var arr) && arr.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var row in arr.EnumerateArray())
                        {
                            var code = row.TryGetProperty("salesCode", out var sc) ? sc.GetString() : null;
                            if (!string.IsNullOrWhiteSpace(code))
                                distinctCodes.Add(code!);
                        }
                    }

                    url = doc.RootElement.TryGetProperty("@odata.nextLink", out var next)
                        ? next.GetString()
                        : null;
                }

                // Build TVP
                var tvp = BuildBulkInsertTable();

                foreach (var code in distinctCodes)
                {
                    var r = tvp.NewRow();
                    r["String1"] = code;    // PriceCode
                    r["String2"] = code;    // Description (same as code)
                    r["Bit1"] = true;       // IsActive
                    tvp.Rows.Add(r);
                }

                using var cmd = new SqlCommand("dbo.BulkUpsert_POS_PriceCodes", sqlConn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add(AsBulkInsertParam("@Rows", tvp));
                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SyncPriceCodesAsync (Bulk) failed: {Message}", ex.Message);
                return false;
            }
        }

        //public async Task<bool> SyncProducts(SqlConnection sqlConn)
        //{
        //    try
        //    {
        //        var settings = new BusinessCentralSettings();
        //        _config.GetSection("BusinessCentral").Bind(settings);

        //        var token = await _tokenProvider.GetAccessTokenAsync();

        //        var url =
        //            $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
        //            $"/api/v2.0/companies({settings.CompanyId})/items";

        //        var client = _httpFactory.CreateClient("bc");
        //        client.DefaultRequestHeaders.Authorization =
        //            new AuthenticationHeaderValue("Bearer", token);

        //        using var res = await client.GetAsync(url);
        //        var json = await res.Content.ReadAsStringAsync();

        //        if (!res.IsSuccessStatusCode)
        //            throw new Exception($"BC items failed: {res.StatusCode} | {json}");

        //        var root = JsonSerializer.Deserialize<BusinessCentralItemList>(
        //            json,
        //            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //        var items = root?.Value ?? new List<BusinessCentralItem>();

        //        // Lookups for FK mapping
        //        var units = await Inventory.Inventory_Custom_Service.POS_Units_Select_All(new Unit(), sqlConn);
        //        var categories = await Inventory.Inventory_Custom_Service.POS_ProductCategories_Select_All(new ProductCategory(), sqlConn);
        //        var types = await Inventory.Inventory_Custom_Service.POS_ProductTypes_Select_All(new ProductType(), sqlConn);

        //        // Default ProductType = first record (your request)
        //        var defaultTypeId = types?.FirstOrDefault()?.ProductTypeID ?? 0;

        //        // Build dictionaries
        //        var unitByCode = new Dictionary<string, Unit>(StringComparer.OrdinalIgnoreCase);
        //        foreach (var u in units)
        //        {
        //            if (!string.IsNullOrWhiteSpace(u.Unit) && !unitByCode.ContainsKey(u.Unit))
        //                unitByCode[u.Unit] = u;
        //        }

        //        var categoryByBcId = new Dictionary<string, ProductCategory>(StringComparer.OrdinalIgnoreCase);
        //        foreach (var c in categories)
        //        {
        //            if (!string.IsNullOrWhiteSpace(c.BC_ID) && !categoryByBcId.ContainsKey(c.BC_ID))
        //                categoryByBcId[c.BC_ID] = c;
        //        }

        //        var typeByName = new Dictionary<string, ProductType>(StringComparer.OrdinalIgnoreCase);
        //        foreach (var t in types)
        //        {
        //            if (!string.IsNullOrWhiteSpace(t.ProductType) && !typeByName.ContainsKey(t.ProductType))
        //                typeByName[t.ProductType] = t;
        //        }

        //        // Build TVP
        //        var tvp = BuildBulkInsertTable();

        //        foreach (var it in items)
        //        {
        //            // FK_UnitID is required in POS_Products
        //            if (!unitByCode.TryGetValue(it.BaseUnitOfMeasureCode ?? "", out var unit))
        //                continue;

        //            // FK_ProductTypeID is required in POS_Products
        //            int typeId;
        //            if (!string.IsNullOrWhiteSpace(it.Type) && typeByName.TryGetValue(it.Type, out var matchedType))
        //            {
        //                typeId = (int)matchedType.ProductTypeID;
        //            }
        //            else
        //            {
        //                // ✅ fallback to first ProductType record
        //                if (defaultTypeId <= 0)
        //                {
        //                    // no product types in DB, can't insert (FK required)
        //                    continue;
        //                }
        //                typeId = defaultTypeId;
        //            }

        //            // FK_ProductCategoryID can be NULL
        //            var catId = (!string.IsNullOrWhiteSpace(it.ItemCategoryID) &&
        //                         categoryByBcId.TryGetValue(it.ItemCategoryID, out var c))
        //                ? c.ProductCategoryID
        //                : 0;

        //            var row = tvp.NewRow();

        //            // Strings -> send NULL as DBNull.Value (not empty)
        //            row["String1"] = string.IsNullOrWhiteSpace(it.ID) ? (object)DBNull.Value : it.ID;                    // BC_ID
        //            row["String2"] = string.IsNullOrWhiteSpace(it.Number) ? (object)DBNull.Value : it.Number;            // ItemNo
        //            row["String3"] = string.IsNullOrWhiteSpace(it.DisplayName) ? (object)DBNull.Value : it.DisplayName;  // ProductName
        //            row["String4"] = string.IsNullOrWhiteSpace(it.DisplayName) ? (object)DBNull.Value : it.DisplayName;  // Description

        //            // Ints
        //            row["Int1"] = typeId;                      // FK_ProductTypeID
        //            row["Int2"] = unit.UnitID;             // FK_UnitID
        //            row["Int3"] = catId;                       // FK_ProductCategoryID (0 => NULL in SQL proc)
        //            row["Int4"] = unit.UnitID;             // FK_DefaultUnitID

        //            // Bits
        //            row["Bit1"] = string.Equals(it.Type, "Inventory", StringComparison.OrdinalIgnoreCase); // IsStockTracked
        //            row["Bit2"] = !it.Blocked;                 // IsActive

        //            tvp.Rows.Add(row);
        //        }

        //        using var cmd = new SqlCommand("dbo.BulkUpsert_POS_Products", sqlConn)
        //        {
        //            CommandType = CommandType.StoredProcedure,
        //            CommandTimeout = 300
        //        };

        //        cmd.Parameters.Add(AsBulkInsertParam("@Rows", tvp));
        //        await cmd.ExecuteNonQueryAsync();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "SyncProductsAsync (Bulk) failed: {Message}", ex.Message);
        //        return false;
        //    }
        //}

        public async Task<bool> SyncProducts(SqlConnection sqlConn)
        {
            try
            {
                var settings = new BusinessCentralSettings();
                _config.GetSection("BusinessCentral").Bind(settings);

                var token = await _tokenProvider.GetAccessTokenAsync();

                var url =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                    $"/api/v2.0/companies({settings.CompanyId})/items";

                var client = _httpFactory.CreateClient("bc");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                using var res = await client.GetAsync(url);
                var json = await res.Content.ReadAsStringAsync();

                if (!res.IsSuccessStatusCode)
                    throw new Exception($"BC items failed: {res.StatusCode} | {json}");

                var root = JsonSerializer.Deserialize<BusinessCentralItemList>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var items = root?.Value ?? new List<BusinessCentralItem>();

                //var tempProduct = JsonSerializer.Serialize(items.Where(x => x.Number == "100683").ToList());

                // Lookups for FK mapping
                var units = await Inventory.Inventory_Custom_Service.POS_Units_Select_All(new Unit(), sqlConn);
                var categories = await Inventory.Inventory_Custom_Service.POS_ProductCategories_Select_All(new ProductCategory(), sqlConn);
                var types = await Inventory.Inventory_Custom_Service.POS_ProductTypes_Select_All(new ProductType(), sqlConn);

                // Default ProductType = first record (your request)
                var defaultTypeId = types?.FirstOrDefault()?.ProductTypeID ?? 0;

                // Build dictionaries
                var unitByCode = new Dictionary<string, Unit>(StringComparer.OrdinalIgnoreCase);
                foreach (var u in units)
                {
                    if (!string.IsNullOrWhiteSpace(u.Unit) && !unitByCode.ContainsKey(u.Unit))
                        unitByCode[u.Unit] = u;
                }

                var categoryByBcId = new Dictionary<string, ProductCategory>(StringComparer.OrdinalIgnoreCase);
                foreach (var c in categories)
                {
                    if (!string.IsNullOrWhiteSpace(c.BC_ID) && !categoryByBcId.ContainsKey(c.BC_ID))
                        categoryByBcId[c.BC_ID] = c;
                }

                var typeByName = new Dictionary<string, ProductType>(StringComparer.OrdinalIgnoreCase);
                foreach (var t in types)
                {
                    if (!string.IsNullOrWhiteSpace(t.ProductType) && !typeByName.ContainsKey(t.ProductType))
                        typeByName[t.ProductType] = t;
                }

                // ✅ Two TVPs: one for products, one for service charges
                var tvpProducts = BuildBulkInsertTable();
                var tvpServiceCharges = BuildBulkInsertTable();

                static bool IsServiceCharge(BusinessCentralItem it)
                {
                    return !string.IsNullOrWhiteSpace(it?.DisplayName) &&
                           it.DisplayName.IndexOf("Service Charge", StringComparison.OrdinalIgnoreCase) >= 0;
                }

                // ✅ Trim only leading MENU##_
                static (string? trimmedName, string? originalName) SplitMenuPrefix(string? displayName)
                {
                    if (string.IsNullOrWhiteSpace(displayName))
                        return (null, null);

                    var s = displayName.Trim();

                    // Expect: MENU + digits + '_' at the very start, e.g. MENU11_MC_POKE BOWL ...
                    if (s.Length >= 6 && s.StartsWith("MENU", StringComparison.OrdinalIgnoreCase))
                    {
                        int i = 4; // after "MENU"
                        bool hasDigits = false;

                        while (i < s.Length && char.IsDigit(s[i]))
                        {
                            hasDigits = true;
                            i++;
                        }

                        if (hasDigits && i < s.Length && s[i] == '_')
                        {
                            var trimmed = s[(i + 1)..].Trim(); // everything after the underscore
                            if (!string.IsNullOrWhiteSpace(trimmed))
                                return (trimmed, s);
                        }
                    }

                    return (s, s);
                }

                foreach (var it in items)
                {
                    // FK_UnitID required
                    if (!unitByCode.TryGetValue(it.BaseUnitOfMeasureCode ?? "", out var unit))
                        continue;

                    // FK_ProductTypeID required
                    int typeId;
                    if (!string.IsNullOrWhiteSpace(it.Type) && typeByName.TryGetValue(it.Type, out var matchedType))
                    {
                        typeId = (int)matchedType.ProductTypeID;
                    }
                    else
                    {
                        if (defaultTypeId <= 0)
                            continue;

                        typeId = defaultTypeId;
                    }

                    // FK_ProductCategoryID can be NULL (0 => NULL in SQL proc)
                    var catId = (!string.IsNullOrWhiteSpace(it.ItemCategoryID) &&
                                 categoryByBcId.TryGetValue(it.ItemCategoryID, out var c))
                        ? c.ProductCategoryID
                        : 0;

                    // Pick the correct TVP target based on DisplayName
                    var targetTvp = IsServiceCharge(it) ? tvpServiceCharges : tvpProducts;

                    // ✅ Name trimmed, Description keeps original with MENU##
                    var (trimmedName, originalName) = SplitMenuPrefix(it.DisplayName);

                    var row = targetTvp.NewRow();

                    row["String1"] = string.IsNullOrWhiteSpace(it.ID) ? (object)DBNull.Value : it.ID;           // BC_ID
                    row["String2"] = string.IsNullOrWhiteSpace(it.Number) ? (object)DBNull.Value : it.Number;   // ItemNo

                    // ✅ Store trimmed as display/name
                    row["String3"] = string.IsNullOrWhiteSpace(trimmedName) ? (object)DBNull.Value : trimmedName;

                    // ✅ Keep menu number in description (original)
                    row["String4"] = string.IsNullOrWhiteSpace(originalName) ? (object)DBNull.Value : originalName;

                    row["Int1"] = typeId;            // FK_ProductTypeID
                    row["Int2"] = unit.UnitID;       // FK_UnitID
                    row["Int3"] = catId;             // FK_ProductCategoryID (0 => NULL in SQL proc)
                    row["Int4"] = unit.UnitID;       // FK_DefaultUnitID

                    row["Bit1"] = string.Equals(it.Type, "Inventory", StringComparison.OrdinalIgnoreCase); // IsStockTracked
                    row["Bit2"] = !it.Blocked;       // IsActive

                    targetTvp.Rows.Add(row);
                }

                // ✅ 1) Upsert products
                if (tvpProducts.Rows.Count > 0)
                {
                    using var cmdProducts = new SqlCommand("dbo.BulkUpsert_POS_Products", sqlConn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 300
                    };

                    cmdProducts.Parameters.Add(AsBulkInsertParam("@Rows", tvpProducts));
                    await cmdProducts.ExecuteNonQueryAsync();
                }

                // ✅ 2) Upsert service charges
                if (tvpServiceCharges.Rows.Count > 0)
                {
                    using var cmdCharges = new SqlCommand("dbo.BulkUpsert_POS_ServiceCharges", sqlConn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 300
                    };

                    cmdCharges.Parameters.Add(AsBulkInsertParam("@Rows", tvpServiceCharges));
                    await cmdCharges.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SyncProductsAsync (Bulk) failed: {Message}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SyncItemAvailabilityByLocation(SqlConnection sqlConn)
        {
            try
            {
                var settings = _config.GetSection("BusinessCentral").Get<BusinessCentralSettings>();
                var token = await _tokenProvider.GetAccessTokenAsync();

                var client = _httpFactory.CreateClient("bc");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                // ================================
                // 1) Fetch Items (Cost Prices)
                // ================================
                var itemsUrl =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                    $"/api/v2.0/companies({settings.CompanyId})/items?$select=number,unitCost";

                using var itemRes = await client.GetAsync(itemsUrl);
                var itemJson = await itemRes.Content.ReadAsStringAsync();

                if (!itemRes.IsSuccessStatusCode)
                    throw new Exception($"BC Items cost fetch failed: {itemRes.StatusCode} | {itemJson}");

                var itemRoot = JsonSerializer.Deserialize<BusinessCentralItemList>(
                    itemJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Build lookup: ItemNo -> UnitCost
                var costLookup = (itemRoot?.Value ?? new List<BusinessCentralItem>())
                    .Where(x => !string.IsNullOrWhiteSpace(x.Number))
                    .GroupBy(x => x.Number!.Trim())
                    .ToDictionary(
                        g => g.Key,
                        g => g.First().UnitCost ?? 0m
                    );

                #region Second Stock By Location Sync

                // ================================
                // 2) Fetch Sales Prices / Variants / Units
                // ================================
                var settings2 = new BusinessCentralSettings();
                _config.GetSection("BusinessCentral").Bind(settings2);

                var token2 = await _tokenProvider.GetAccessTokenAsync();

                var client2 = _httpFactory.CreateClient("bc");
                client2.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token2);

                var url2 =
                    $"{settings2.BaseUrl}/{settings2.TenantId}/{settings2.Environment}" +
                    $"/api/bluesafari/pricing/v1.0/companies({settings2.CompanyId})/salesPrices7007";

                using var res2 = await client2.GetAsync(url2);
                var json2 = await res2.Content.ReadAsStringAsync();

                if (!res2.IsSuccessStatusCode)
                    throw new Exception($"BC salesPrices7007 failed: {res2.StatusCode} | {json2}");

                var options2 = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false
                };

                var payload2 = JsonSerializer.Deserialize<BusinessCentralSalesPriceList>(json2, options2);

                if (payload2?.value == null || payload2.value.Count == 0)
                    return true;

                // ================================
                // 3) Build and Sync Units
                // ================================
                var distinctUnits = payload2.value
                    .Where(x => !string.IsNullOrWhiteSpace(x.unitOfMeasureCode))
                    .Select(x => new
                    {
                        BC_ID = x.unitOfMeasureCode.Trim(),
                        Unit = x.unitOfMeasureCode.Trim()
                    })
                    .Distinct()
                    .ToList();

                if (distinctUnits.Any())
                {
                    var tvpUnits = BuildBulkInsertTable();

                    foreach (var u in distinctUnits)
                    {
                        var row = tvpUnits.NewRow();

                        row["String1"] = null;   // BC_ID
                        row["String2"] = u.Unit;    // Unit
                        row["String3"] = u.Unit;    // Symbol
                        row["Bit1"] = true;         // IsActive

                        tvpUnits.Rows.Add(row);
                    }

                    using var cmdUnits = new SqlCommand("dbo.BulkUpsert_POS_Units", sqlConn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 300
                    };

                    cmdUnits.Parameters.Add(AsBulkInsertParam("@Rows", tvpUnits));
                    await cmdUnits.ExecuteNonQueryAsync();
                }

                // ================================
                // 4) Build Bulk TVP for POS_DebtorProducts
                // ================================
                var tvp2 = BuildBulkInsertTable();

                var distinctItems = payload2.value
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(x.itemNo) &&
                        !string.IsNullOrWhiteSpace(x.variantCode))
                    .Select(x => new
                    {
                        ItemNo = x.itemNo.Trim(),
                        VariantCode = x.variantCode.Trim(),
                        UnitOfMeasure = x.unitOfMeasureCode.Trim(),
                    })
                    .Distinct()
                    .ToList();

                foreach (var li in distinctItems)
                {
                    var costPrice = costLookup.TryGetValue(li.ItemNo, out var cost)
                        ? cost
                        : 0m;

                    var row = tvp2.NewRow();

                    row["String1"] = li.ItemNo;        // ItemNo
                    row["String2"] = li.VariantCode;   // LocationCode / VariantCode
                    row["String3"] = li.UnitOfMeasure;
                    row["Decimal1"] = 0m;              // QuantityOnHand
                    row["Bit1"] = true;                // IsAvailable
                    row["Decimal2"] = costPrice;       // CostPrice
                    row["Bit2"] = true;                // IsActive
                    row["Int1"] = 1;                   // CreatedUser
                    row["Int2"] = 1;                   // UpdatedUser

                    tvp2.Rows.Add(row);
                }

                // ================================
                // 5) Bulk Merge into POS_DebtorProducts
                // ================================
                if (tvp2.Rows.Count > 0)
                {
                    using var cmd2 = new SqlCommand("dbo.BulkUpsert_POS_DebtorProducts", sqlConn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 300
                    };

                    cmd2.Parameters.Add(AsBulkInsertParam("@Rows", tvp2));
                    await cmd2.ExecuteNonQueryAsync();
                }

                #endregion

                #region First Stock By Location Sync
                // ================================
                // 2) Fetch Stock By Location
                // ================================
                var stockUrl =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                    $"/api/bluesafari/inventory/v1.0/companies({settings.CompanyId})/itemStockByLocations";

                using var stockRes = await client.GetAsync(stockUrl);
                var stockJson = await stockRes.Content.ReadAsStringAsync();

                if (!stockRes.IsSuccessStatusCode)
                    throw new Exception($"BC StockByLocation failed: {stockRes.StatusCode} | {stockJson}");

                var stockRoot = JsonSerializer.Deserialize<BcODataResponse<BcItemAvailabilityLocation>>(
                    stockJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var test2 = stockRoot.Value.Where(x => x.ItemNo == "100683").ToList();  

                // ================================
                // 3) Build Bulk TVP
                // ================================
                var tvp = BuildBulkInsertTable();

                //foreach (var li in stockRoot?.Value ?? new List<BcItemAvailabilityLocation>())
                //{
                //    if (string.IsNullOrWhiteSpace(li.ItemNo) ||
                //        string.IsNullOrWhiteSpace(li.LocationCode))
                //        continue;

                //    var costPrice = costLookup.TryGetValue(li.ItemNo.Trim(), out var cost)
                //        ? cost
                //        : 0m;

                //    var row = tvp.NewRow();

                //    row["String1"] = li.ItemNo.Trim();       // ItemNo
                //    row["String2"] = li.LocationCode.Trim(); // LocationCode
                //    row["Decimal1"] = (decimal)(li.OnHandQty ?? 0); // QuantityOnHand
                //    row["Bit1"] = li.HasStock ?? false;      // IsAvailable
                //    row["Decimal2"] = costPrice;             // CostPrice
                //    row["Bit2"] = true;                      // IsActive
                //    row["Int1"] = 1;                         // CreatedUser
                //    row["Int2"] = 1;                         // UpdatedUser

                //    tvp.Rows.Add(row);
                //}

                //// ================================
                //// 4) Bulk Merge into POS_DebtorProducts
                //// ================================
                //using var cmd = new SqlCommand("dbo.BulkUpsert_POS_DebtorProducts", sqlConn)
                //{
                //    CommandType = CommandType.StoredProcedure,
                //    CommandTimeout = 300
                //};

                //cmd.Parameters.Add(AsBulkInsertParam("@Rows", tvp));
                //await cmd.ExecuteNonQueryAsync();
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "SyncItemAvailabilityByLocationAsync (Cost+Stock Bulk) failed: {Message}",
                    ex.Message);

                return false;
            }
        }

        public async Task<bool> SyncSalesPricing(SqlConnection sqlConn)
        {
            try
            {
                // 1) Settings + token
                var settings = new BusinessCentralSettings();
                _config.GetSection("BusinessCentral").Bind(settings);

                var token = await _tokenProvider.GetAccessTokenAsync();

                var client = _httpFactory.CreateClient("bc");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                // ---------------------------------------------------------------------
                // 2) First call: item UOMs
                // ---------------------------------------------------------------------
                var uomUrl =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                    $"/api/bluesafari/inventory/v1.0/companies({settings.CompanyId})/itemUoms";

                using var uomRes = await client.GetAsync(uomUrl);
                var uomJson = await uomRes.Content.ReadAsStringAsync();

                if (!uomRes.IsSuccessStatusCode)
                    throw new Exception($"BC itemUoms failed: {uomRes.StatusCode} | {uomJson}");

                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = false
                };

                var uomPayload = JsonSerializer.Deserialize<BusinessCentralItemUomList>(uomJson, jsonOptions);

                var salesUomLookup = (uomPayload?.value ?? new List<BusinessCentralItemUom>())
                    .Where(x => !string.IsNullOrWhiteSpace(x.itemNo))
                    .GroupBy(x => x.itemNo.Trim(), StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(
                        g => g.Key,
                        g => g.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.salesUnitOfMeasure))?.salesUnitOfMeasure?.Trim()
                             ?? g.First().salesUnitOfMeasure?.Trim()
                             ?? string.Empty,
                        StringComparer.OrdinalIgnoreCase
                    );

                // ---------------------------------------------------------------------
                // 3) Second call: sales prices
                // ---------------------------------------------------------------------
                var priceUrl =
                    $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                    $"/api/bluesafari/pricing/v1.0/companies({settings.CompanyId})/salesPrices7007";

                using var priceRes = await client.GetAsync(priceUrl);
                var priceJson = await priceRes.Content.ReadAsStringAsync();

                if (!priceRes.IsSuccessStatusCode)
                    throw new Exception($"BC salesPrices7007 failed: {priceRes.StatusCode} | {priceJson}");

                var pricePayload = JsonSerializer.Deserialize<BusinessCentralSalesPriceList>(priceJson, jsonOptions);

                if (pricePayload?.value == null || pricePayload.value.Count == 0)
                    return true;

                // ---------------------------------------------------------------------
                // 4) Build TVP
                // IMPORTANT:
                // You must have a column available in your TVP for SalesUnitOfMeasure.
                // Example below uses String5.
                // ---------------------------------------------------------------------
                var tvp = BuildBulkInsertTable();

                var distinctPrices = pricePayload.value
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(x.itemNo) &&
                        !string.IsNullOrWhiteSpace(x.salesCode) &&
                        x.unitPrice != null &&
                        !string.IsNullOrWhiteSpace(x.variantCode))
                    .Select(x => new
                    {
                        ItemNo = x.itemNo.Trim(),
                        SalesCode = x.salesCode.Trim(),
                        UnitPrice = x.unitPrice.Value,
                        PriceIncludesVat = x.priceIncludesVat == true,
                        LocationCode = x.variantCode.Trim(),
                        UnitOfMeasure = x.unitOfMeasureCode?.Trim(),
                        SalesUnitOfMeasure = salesUomLookup.TryGetValue(x.itemNo.Trim(), out var salesUom)
                            ? salesUom
                            : "EACH"
                    })
                    .Distinct()
                    .ToList();

                foreach (var row in distinctPrices)
                {
                    var r = tvp.NewRow();

                    r["String1"] = row.ItemNo;              // ItemNo
                    r["String2"] = row.SalesCode;          // PriceCode / SalesCode
                    r["Decimal1"] = row.UnitPrice;         // Price
                    r["Bit1"] = row.PriceIncludesVat;      // Includes VAT
                    r["Bit2"] = true;                      // IsActive
                    r["String3"] = row.LocationCode;       // Variant / Location
                    r["String4"] = row.UnitOfMeasure ?? ""; // BC price UOM
                    r["String5"] = row.SalesUnitOfMeasure ?? ""; // BC sales UOM

                    tvp.Rows.Add(r);
                }

                // ---------------------------------------------------------------------
                // 5) Bulk upsert
                // ---------------------------------------------------------------------
                using var cmd = new SqlCommand("dbo.BulkUpsert_POS_DebtorProductPrices_FromBC", sqlConn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 300
                };

                cmd.Parameters.Add(AsBulkInsertParam("@Rows", tvp));
                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SyncSalesPricingAsync failed: {Message}", ex.Message);
                return false;
            }
        }

        #endregion

        #region Pull Requests Old

        public async Task<bool> PingAsync()
        {
            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            var url = $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/v2.0/companies";
            var client = _httpFactory.CreateClient("bc");
            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                _logger.LogInformation("BC ping OK");
                return true;
            }

            var body = await res.Content.ReadAsStringAsync();
            _logger.LogWarning("BC ping failed: {Status} {Body}", res.StatusCode, body);
            return false;
        }

        public async Task<List<BusinessCentralDebtor>> GetDebtorsAsync()
        {
            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            var url = $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/v2.0/companies({settings.CompanyId})/customers";
            var token = await _tokenProvider.GetAccessTokenAsync();

            var client = _httpFactory.CreateClient("bc");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var res = await client.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                _logger.LogError("BC GetDebtors failed: {Status} {Body}", res.StatusCode, json);
                throw new Exception($"Failed to fetch debtors: {res.StatusCode}");
            }

            var root = JsonSerializer.Deserialize<BusinessCentralDebtorList>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            foreach (var debtor in root.Value)
            {
                var debtorInsert = await Debtors.Debtors_Custom_Service.Debtors_Sync_Insert(new Debtor()
                {
                    BC_ID = debtor.Id,
                    ShortCode = debtor.Number,
                    Name = debtor.DisplayName,
                    FK_MasterDebtorID = null,
                    IsMasterDebtor = false,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                }, _config.GetConnectionString(string.Format("ApplicationDb_1")));
            }

            return root?.Value ?? new List<BusinessCentralDebtor>();
        }

        public async Task<List<BusinessCentralCreditor>> GetCreditorsAsync()
        {
            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            var url = $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/v2.0/companies({settings.CompanyId})/vendors";
            var token = await _tokenProvider.GetAccessTokenAsync();

            var client = _httpFactory.CreateClient("bc");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var res = await client.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                _logger.LogError("BC GetCreditors failed: {Status} {Body}", res.StatusCode, json);
                throw new Exception($"Failed to fetch creditors: {res.StatusCode}");
            }

            var root = JsonSerializer.Deserialize<BusinessCentralCreditorList>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });



            foreach (var creditor in root.Value)
            {
                var creditorInsert = await Creditors.Creditors_Custom_Service.Creditors_Sync_Insert(new Creditor()
                {
                    BC_ID = creditor.Id,
                    ShortCode = creditor.Number,
                    Name = creditor.DisplayName,
                    FK_MasterCreditorID = null,
                    IsMasterCreditor = false,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                }, _config.GetConnectionString(string.Format("ApplicationDb_1")));
            }

            return root?.Value ?? new List<BusinessCentralCreditor>();
        }

        public async Task<bool> GetItemAvailabilityByLocationAsync()
        {
            var settings = _config.GetSection("BusinessCentral").Get<BusinessCentralSettings>();
            var token = await _tokenProvider.GetAccessTokenAsync();

            var url =
                $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                $"/api/bluesafari/inventory/v1.0/companies({settings.CompanyId})/itemStockByLocations";


            var client = _httpFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(settings.RequestTimeoutSeconds);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            using var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BcODataResponse<BcItemAvailabilityLocation>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var productResponse = await Inventory.Inventory_Custom_Service.POS_Products_Select_All(new Product()
            {

            }, _config.GetConnectionString("ApplicationDb_1"));

            var locationResponse = await Debtors.Debtors_Custom_Service.POS_Locations_Select_All(new Location()
            {

            }, _config.GetConnectionString("ApplicationDb_1"));

            var unitResponse = await Inventory.Inventory_Custom_Service.POS_Units_Select_All(new Unit()
            {

            }, _config.GetConnectionString("ApplicationDb_1"));

            foreach (var locationItem in result.Value)
            {
                var productLocationInsert = await Product_Location_Insert_Update(new DebtorProduct()
                {
                    FK_ProductID = productResponse.FirstOrDefault(t => t.ItemNo == locationItem.ItemNo)?.ProductID,
                    FK_LocationID = locationResponse.FirstOrDefault(t => t.ShortCode == locationItem.LocationCode)?.LocationID,
                    CostPrice = 0,
                    FK_SellUnitID = productResponse.FirstOrDefault(t => t.ItemNo == locationItem.ItemNo)?.FK_UnitID,
                    QuantityOnHand = (decimal)locationItem.OnHandQty,
                    IsAvailable = locationItem.HasStock
                }, _config.GetConnectionString("ApplicationDb_1"));
            }

            return true;
        }

        public async Task<bool> GetLocationsAsync()
        {
            var settings = _config
                .GetSection("BusinessCentral")
                .Get<BusinessCentralSettings>();

            var token = await _tokenProvider.GetAccessTokenAsync();

            var url =
                $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                $"/api/v2.0/companies({settings.CompanyId})/locations";

            using var client = _httpFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(
                    $"BC ListLocations failed: {(int)response.StatusCode} {response.ReasonPhrase} | {json}"
                );

            var result = JsonSerializer.Deserialize<
                BusinessCentralODataResponse<BusinessCentralLocation>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            foreach (var location in result.Value)
            {
                var locationInsert = await Location_Insert_Update(new Location()
                {
                    FK_CurrencyID = 140,
                    ShortCode = location.Code,
                    Name = location.DisplayName,
                    BC_ID = location.Id
                }, _config.GetConnectionString("ApplicationDb_1"));

                //var costCenterInsert = await POS_CostCenters_Insert_Update(new POS_CostCenter()
                //{
                //    FK_LocationID = locationInsert.LocationID,
                //    Name = location.DisplayName,
                //    BC_ID = location.Id
                //}, _config.GetConnectionString("ApplicationDb_1"));
            }

            return true;
        }

        public async Task<bool> GetProductsAsync()
        {

            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            var itemsUrl =
                $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/v2.0/companies({settings.CompanyId})/items";

            var token = await _tokenProvider.GetAccessTokenAsync();

            var client = _httpFactory.CreateClient("bc");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var itemRes = await client.GetAsync(itemsUrl);
            var itemJson = await itemRes.Content.ReadAsStringAsync();

            if (!itemRes.IsSuccessStatusCode)
            {
                _logger.LogError("BC GetItems failed: {Status} {Body}", itemRes.StatusCode, itemJson);
                throw new Exception($"Failed to fetch items: {itemRes.StatusCode}");
            }

            var itemRoot = JsonSerializer.Deserialize<BusinessCentralItemList>(
                itemJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var distinctTypes = itemRoot.Value
                .Select(x => x.Type)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .DistinctBy(x => x)
                .ToList();

            var distinctUnits = itemRoot.Value
                .Where(x =>
                    !string.IsNullOrWhiteSpace(x.BaseUnitOfMeasureID) &&
                    !string.IsNullOrWhiteSpace(x.BaseUnitOfMeasureCode))
                .Select(x => new
                {
                    x.BaseUnitOfMeasureID,
                    x.BaseUnitOfMeasureCode
                })
                .Distinct()
                .ToList();
            using (SqlConnection sqlConn = SqlClient.CreateInstance(_config.GetConnectionString("ApplicationDb_1")))
            {
                foreach (var units in distinctUnits)
                {
                    var unitInsert = await Unit_Insert_Update(new Unit()
                    {
                        Unit = units.BaseUnitOfMeasureCode,
                        BC_ID = units.BaseUnitOfMeasureID
                    }, sqlConn);
                }

                var typeResponse = await Inventory.Inventory_Custom_Service.POS_ProductTypes_Select_All(new ProductType()
                {

                }, sqlConn);

                var categoryResponse = await Inventory.Inventory_Custom_Service.POS_ProductCategories_Select_All(new ProductCategory()
                {

                }, sqlConn);

                var unitResponse = await Inventory.Inventory_Custom_Service.POS_Units_Select_All(new Unit()
                {

                }, sqlConn);

                foreach (var product in itemRoot.Value.Where(x => x.DisplayName.ToLower().Contains("amstel")))
                {
                    var isTracked = false;

                    if (product.Type == "Inventory")
                    {
                        isTracked = true;
                    }

                    var productInsert = await Product_Insert_Update(new Product()
                    {
                        ProductName = product.DisplayName,
                        FK_ProductTypeID = typeResponse.FirstOrDefault(t => t.ProductType == product.Type)?.ProductTypeID ?? 2,
                        IsStockTracked = isTracked,
                        FK_UnitID = unitResponse.FirstOrDefault(u => u.Unit == product.BaseUnitOfMeasureCode)?.UnitID ?? 5,
                        FK_ProductCategoryID = categoryResponse.FirstOrDefault(c => c.BC_ID == product.ItemCategoryID)?.ProductCategoryID ?? 3,
                        FK_DefaultUnitID = unitResponse.FirstOrDefault(u => u.Unit == product.BaseUnitOfMeasureCode)?.UnitID ?? 5,
                        IsActive = !product.Blocked,
                        BC_ID = product.ID,
                        ItemNo = product.Number,
                    }, sqlConn);
                }
            }

            return true;
        }

        public async Task<bool> GetSalesPricingAsync()
        {
            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            var token = await _tokenProvider.GetAccessTokenAsync();
            var client = _httpFactory.CreateClient("bc");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // ✅ Your custom API entity set name
            var url =
                $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/bluesafari/pricing/v1.0/companies({settings.CompanyId})/salesPrices7007";

            var results = new List<BusinessCentralSalesPriceRow>();

            while (!string.IsNullOrWhiteSpace(url))
            {
                var res = await client.GetAsync(url);
                var json = await res.Content.ReadAsStringAsync();

                if (!res.IsSuccessStatusCode)
                {
                    _logger.LogError("BC salesPrices7007 failed: {Status} {Body}", res.StatusCode, json);
                    throw new Exception($"Failed to fetch sales prices: {res.StatusCode}");
                }

                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("value", out var arr) && arr.ValueKind == JsonValueKind.Array)
                {
                    foreach (var row in arr.EnumerateArray())
                    {
                        // Strings
                        var systemId = row.TryGetProperty("systemId", out var sid) ? sid.GetString() : null;
                        var itemNo = row.TryGetProperty("itemNo", out var ino) ? ino.GetString() : null;
                        var salesType = row.TryGetProperty("salesType", out var st) ? st.GetString() : null;
                        var salesCode = row.TryGetProperty("salesCode", out var sc) ? sc.GetString() : null;
                        var currencyCode = row.TryGetProperty("currencyCode", out var cc) ? cc.GetString() : null;
                        var variantCode = row.TryGetProperty("variantCode", out var vc) ? vc.GetString() : null;
                        var unitOfMeasureCode = row.TryGetProperty("unitOfMeasureCode", out var uom) ? uom.GetString() : null;
                        var vatBusPostingGrPrice = row.TryGetProperty("vatBusPostingGrPrice", out var vbg) ? vbg.GetString() : null;

                        // Dates (BC might return "0001-01-01" as "no date")
                        DateTime? startingDate = null;
                        if (row.TryGetProperty("startingDate", out var sd) && sd.ValueKind == JsonValueKind.String)
                        {
                            if (DateTime.TryParse(sd.GetString(), out var dt))
                                startingDate = dt;
                        }

                        DateTime? endingDate = null;
                        if (row.TryGetProperty("endingDate", out var ed) && ed.ValueKind == JsonValueKind.String)
                        {
                            var s = ed.GetString();
                            if (DateTime.TryParse(s, out var dt) && dt.Year > 1) // treat 0001-01-01 as null
                                endingDate = dt;
                        }

                        // Numbers
                        decimal? unitPrice = null;
                        if (row.TryGetProperty("unitPrice", out var up) &&
                            (up.ValueKind == JsonValueKind.Number) &&
                            up.TryGetDecimal(out var upDec))
                        {
                            unitPrice = upDec;
                        }

                        decimal? minimumQuantity = null;
                        if (row.TryGetProperty("minimumQuantity", out var mq) &&
                            mq.ValueKind == JsonValueKind.Number)
                        {
                            // BC sometimes returns 1 or 1.0 depending on API
                            if (mq.TryGetDecimal(out var mqDec))
                                minimumQuantity = mqDec;
                        }

                        // Booleans
                        bool? priceIncludesVat = row.TryGetProperty("priceIncludesVat", out var piv) && piv.ValueKind == JsonValueKind.True
                            ? true
                            : row.TryGetProperty("priceIncludesVat", out piv) && piv.ValueKind == JsonValueKind.False
                                ? false
                                : (bool?)null;

                        bool? allowInvoiceDisc = row.TryGetProperty("allowInvoiceDisc", out var aid) && aid.ValueKind == JsonValueKind.True
                            ? true
                            : row.TryGetProperty("allowInvoiceDisc", out aid) && aid.ValueKind == JsonValueKind.False
                                ? false
                                : (bool?)null;

                        bool? allowLineDisc = row.TryGetProperty("allowLineDisc", out var ald) && ald.ValueKind == JsonValueKind.True
                            ? true
                            : row.TryGetProperty("allowLineDisc", out ald) && ald.ValueKind == JsonValueKind.False
                                ? false
                                : (bool?)null;

                        // Minimal guard (adjust if you want to keep blanks)
                        if (string.IsNullOrWhiteSpace(itemNo))
                            continue;

                        results.Add(new BusinessCentralSalesPriceRow
                        {
                            //SystemId = systemId,
                            //ItemNo = itemNo!,
                            ////SalesType = salesType,
                            //SalesCode = salesCode,
                            //StartingDate = startingDate,
                            //EndingDate = endingDate,
                            //CurrencyCode = currencyCode,
                            ////VariantCode = variantCode,
                            //UnitOfMeasureCode = unitOfMeasureCode,
                            //MinimumQuantity = minimumQuantity,
                            //UnitPrice = unitPrice,
                            //PriceIncludesVat = priceIncludesVat,
                            //AllowInvoiceDisc = allowInvoiceDisc,
                            //VatBusPostingGrPrice = vatBusPostingGrPrice,
                            //AllowLineDisc = allowLineDisc
                        });
                    }
                }

                // Paging
                url = doc.RootElement.TryGetProperty("@odata.nextLink", out var next)
                    ? next.GetString()
                    : null;
            }

            // ✅ Dedupe (tweak key if your API can return duplicates)
            var tempResults = results
                .GroupBy(x => new
                {
                    //x.SystemId,
                    //x.ItemNo,
                    ////x.SalesType,
                    //x.SalesCode,
                    //x.StartingDate,
                    //x.EndingDate,
                    //x.CurrencyCode,
                    ////x.VariantCode,
                    //x.UnitOfMeasureCode,
                    //x.MinimumQuantity,
                    //x.UnitPrice
                })
                .Select(g => g.First())
                //.OrderBy(x => x.ItemNo)
                //.ThenBy(x => x.SalesType)
                //.ThenBy(x => x.SalesCode)
                .ToList();

            //var distinctSalesCodes = tempResults.Select(x => x.SalesCode).ToList().DistinctBy(x => x).ToList();

            return true;
        }

        public async Task<bool> GetItemsWithCategoriesAsync()
        {
            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            // 1) URLs (same base pattern as your debtors call)
            var itemsUrl =
                $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/v2.0/companies({settings.CompanyId})/items";//?$select=id,number,displayName,itemCategoryCode,baseUnitOfMeasureCode";

            var categoriesUrl =
                $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}/api/v2.0/companies({settings.CompanyId})/itemCategories";//?$select=code,displayName";

            var token = await _tokenProvider.GetAccessTokenAsync();

            var client = _httpFactory.CreateClient("bc");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 2) Get Categories
            var catRes = await client.GetAsync(categoriesUrl);
            var catJson = await catRes.Content.ReadAsStringAsync();

            if (!catRes.IsSuccessStatusCode)
            {
                _logger.LogError("BC GetItemCategories failed: {Status} {Body}", catRes.StatusCode, catJson);
                throw new Exception($"Failed to fetch item categories: {catRes.StatusCode}");
            }

            var catRoot = JsonSerializer.Deserialize<BusinessCentralItemCategoryList>(
                catJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var categoryLookup = (catRoot?.Value ?? new List<BusinessCentralItemCategory>())
                .Where(x => !string.IsNullOrWhiteSpace(x.Code))
                .GroupBy(x => x.Code!)
                .ToDictionary(g => g.Key, g => g.First());

            // 3) Get Items
            var itemRes = await client.GetAsync(itemsUrl);
            var itemJson = await itemRes.Content.ReadAsStringAsync();

            if (!itemRes.IsSuccessStatusCode)
            {
                _logger.LogError("BC GetItems failed: {Status} {Body}", itemRes.StatusCode, itemJson);
                throw new Exception($"Failed to fetch items: {itemRes.StatusCode}");
            }

            var itemRoot = JsonSerializer.Deserialize<BusinessCentralItemList>(
                itemJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var items = itemRoot?.Value ?? new List<BusinessCentralItem>();

            // 4) Join items -> category name
            var result = new List<BusinessCentralItemWithCategory>();

            foreach (var item in items)
            {
                BusinessCentralItemCategory? cat = null;

                if (!string.IsNullOrWhiteSpace(item.ItemCategoryCode))
                {
                    categoryLookup.TryGetValue(item.ItemCategoryCode, out cat);
                }

                result.Add(new BusinessCentralItemWithCategory
                {
                    Id = item.ID,
                    Number = item.Number,
                    DisplayName = item.DisplayName,
                    ItemCategoryCode = item.ItemCategoryCode,
                    ItemCategoryName = cat?.DisplayName,
                    BaseUnitOfMeasureCode = item.BaseUnitOfMeasureCode
                });

                // If you want to sync into your DB here (like debtors), do it in this foreach.
                // Example placeholder:
                // await Items.Items_Custom_Service.Items_Sync_Insert(...);
            }

            return true;
        }

        #endregion

        #region Push Requests

        public async Task<string> CreateInvoiceAsync()
        {
            // ✅ You already have everything you need in this response
            // Expect each line to include: ItemNo, LocationBC_ID, Quantity + UnitPrice (decimal or string)
            var invoiceLines = await List_Invoice(new InvoiceHeader(), _config.GetConnectionString("ApplicationDb_1"));

            //if (invoiceLines == null || !invoiceLines.Any())
            //    throw new Exception("No invoice lines found.");

            var settings = _config.GetSection("BusinessCentral").Get<BusinessCentralSettings>()
                ?? throw new Exception("BusinessCentral settings missing.");

            var token = await _tokenProvider.GetAccessTokenAsync();

            var client = _httpFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(settings.RequestTimeoutSeconds);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var baseApi =
                $"{settings.BaseUrl}/{settings.TenantId}/{settings.Environment}" +
                $"/api/v2.0/companies({settings.CompanyId})";

            // 1) Create invoice header in BC
            var customerNumber = "C00016";
            var createInvoiceUrl = $"{baseApi}/salesInvoices";

            var invoicePayload = new
            {
                customerNumber = customerNumber,
                customerPurchaseOrderReference = "" /*invoiceLines.First().InvoiceNo*/,
                postingDate = DateTime.UtcNow.Date.ToString("yyyy-MM-dd")
            };

            using var createResponse = await client.PostAsync(
                createInvoiceUrl,
                new StringContent(JsonSerializer.Serialize(invoicePayload), Encoding.UTF8, "application/json"));

            var createBody = await createResponse.Content.ReadAsStringAsync();

            if (!createResponse.IsSuccessStatusCode)
                throw new Exception($"BC Create Invoice failed: {createBody}");

            var invoiceResult = JsonSerializer.Deserialize<JsonElement>(createBody);
            var invoiceId = invoiceResult.GetProperty("id").GetString();
            var invoiceNumber = invoiceResult.TryGetProperty("number", out var n) ? n.GetString() : "";

            if (string.IsNullOrWhiteSpace(invoiceId))
                throw new Exception("BC invoice id not returned.");

            // 2) Normalize + Group lines by (ItemNo, UnitPrice, Location) and sum Quantity
            // IMPORTANT: price-sensitive grouping as you requested
            //var grouped = invoiceLines
            //    .Select(x => new
            //    {
            //        ItemNo = (x.ItemNo ?? "").Trim(),
            //        LocationBC_ID = (x.LocationBC_ID ?? "").Trim(),

            //        Quantity = x.Quantity,
            //        UnitPrice = x.UnitPriceExcl
            //    })
            //    .Where(x => !string.IsNullOrWhiteSpace(x.ItemNo))
            //    .Where(x => !string.IsNullOrWhiteSpace(x.LocationBC_ID))
            //    .GroupBy(x => new { x.ItemNo, x.LocationBC_ID, x.UnitPrice })
            //    .Select(g => new
            //    {
            //        ItemNo = g.Key.ItemNo,
            //        LocationBC_ID = g.Key.LocationBC_ID,
            //        UnitPrice = g.Key.UnitPrice,
            //        Quantity = g.Sum(v => v.Quantity)
            //    })
            //    .ToList();

            // 3) Add lines to invoice in BC (SEQUENTIAL - avoids Line No collisions)
            var addLineUrl = $"{baseApi}/salesInvoices({invoiceId})/salesInvoiceLines";
            var exclPrice = 92.00;
            var scPrice = 8;

            //foreach (var line in grouped)
            //{
            var linePayload = new
            {
                lineType = "Item",
                lineObjectNumber = "100572",
                quantity = 1,
                unitPrice = exclPrice,
                locationId = "b2762000-c482-f011-b4ca-6045bd4260e5"
            };

            using var lineResponse = await client.PostAsync(
                addLineUrl,
                new StringContent(JsonSerializer.Serialize(linePayload), Encoding.UTF8, "application/json"));

            var lineBody = await lineResponse.Content.ReadAsStringAsync();

            if (!lineResponse.IsSuccessStatusCode)
            {
                // Helpful headers for BC troubleshooting
                var reqId = lineResponse.Headers.TryGetValues("request-id", out var r) ? r.FirstOrDefault() : null;
                var corr = lineResponse.Headers.TryGetValues("ms-correlation-x", out var c) ? c.FirstOrDefault() : null;

                //throw new Exception(
                //    $"BC Add Line failed for {line.ItemNo} @ {line.UnitPrice} ({line.LocationBC_ID}): {lineBody} | request-id={reqId} | ms-correlation-x={corr}");
            }
            //}
            var chargePayload = new
            {
                lineType = "Item",
                lineObjectNumber = "104789",
                quantity = 1,
                unitPrice = scPrice,
                locationId = "b2762000-c482-f011-b4ca-6045bd4260e5"
            };

            using var chargeLineResponse = await client.PostAsync(
                addLineUrl,
                new StringContent(JsonSerializer.Serialize(chargePayload), Encoding.UTF8, "application/json"));

            var chargeLineBody = await chargeLineResponse.Content.ReadAsStringAsync();

            if (!chargeLineResponse.IsSuccessStatusCode)
            {
                // Helpful headers for BC troubleshooting
                var reqId = chargeLineResponse.Headers.TryGetValues("request-id", out var r) ? r.FirstOrDefault() : null;
                var corr = chargeLineResponse.Headers.TryGetValues("ms-correlation-x", out var c) ? c.FirstOrDefault() : null;

                //throw new Exception(
                //    $"BC Add Line failed for {line.ItemNo} @ {line.UnitPrice} ({line.LocationBC_ID}): {lineBody} | request-id={reqId} | ms-correlation-x={corr}");
            }

            // 4) Post/Finalize invoice once
            //var postUrl = $"{baseApi}/salesInvoices({invoiceId})/Microsoft.NAV.post";

            //using var postResponse = await client.PostAsync(postUrl, null);
            //var postBody = await postResponse.Content.ReadAsStringAsync();

            //if (!postResponse.IsSuccessStatusCode)
            //    throw new Exception($"BC Post failed: {postBody}");

            // Return whatever you prefer. invoiceId is the BC GUID id; invoiceNumber is human-readable.
            return invoiceNumber!;
        }
        #endregion

        #endregion
    }
}
