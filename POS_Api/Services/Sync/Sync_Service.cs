using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Security.Claims;

using POS_Common.Models.Sync.POS_SlipPrinters;
using POS_Api.ServiceInterfaces.Sync;
using POS_Api.ServiceInterfaces.Cache;
using TMIS_Common.Interfaces;
using POS_Api.ServiceInterfaces.Logging;
using POS_Common.Models.EntityData.Users;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.Models.Debtors.POS_Locations;
using POS_Common.ModelsDto.SyncController;
using POS_Common.Models.Debtors.POS_CostCenters;
using POS_Common.Models.Inventory.POS_Units;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Stock.POS_DebtorProducts;
using POS_Common.Models.Stock.POS_CostCenterProducts;
using POS_Common.Models.EntityData.TH_BookingHeaders;
using POS_Common.Models.EntityData.Guests;
using POS_Common.Models.EntityData.TH_BookingGuests;
using POS_Common.Models.EntityData.POS_PaymentTypes;
using POS_Common.Models.Menu.POS_Menus;
using POS_Common.Models.Menu.POS_DebtorMenus;
using POS_Common.Models.Menu.POS_DebtorMenuItems;
using POS_Common.Models.Menu.POS_DebtorMenuItemProducts;
using POS_Common.Models.Stock.POS_PriceCodes;
using POS_Common.Models.Stock.POS_DebtorProductPrices;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.Menu.POS_DebtorMenuPrinters;
using POS_Common.Models.Menu.POS_DebtorMenuItemProductPrinters;
using POS_Common.Models.Inventory.POS_ProductCombinations;
using POS_Common.Models.Inventory.POS_ProductExtras;
using POS_Common.Models.Inventory.POS_ProductExtraCategories;
using POS_Common.Models.Inventory.POS_ProductPreparation;
using POS_Common.Models.Inventory.POS_ProductPreparationMethods;
using POS_Common.Models.Inventory.POS_ProductSubstitutions;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.Models.EntityData.POS_ImageCategories;
using POS_Common.Models.EntityData.POS_PaymentTypeIcons;
using POS_Common.ModelsDto.SyncController.FromServer;
using POS_Api.ServiceInterfaces.BusinessCentral;
using POS_Api.Services.BusinessCentral;
using POS_Common.Models.Sync.POS_InvoiceHeaders;
using POS_Common.Models.Sync.POS_InvoiceTabs;
using POS_Common.Models.Sync.POS_InvoiceLines;
using POS_Common.Models.Debtors.POS_LocationCurrencies;
using POS_Common.Models.EntityData.POS_Settings;
using POS_Common.Models.Inventory.POS_ServedAs;
using POS_Common.Models.Inventory.POS_ServedAsProducts;
using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.CurrencyExchangeRates;
using POS_Common.Models.EntityData.GlobalSettings;
using POS_Common.Models.Debtors.POS_CostCenterPrinters;
using Microsoft.Data.SqlClient;
using TMIS_Common.Sql;
using POS_Common.Models.EntityData.POS_SlipTypes;
using POS_Common.Models.Sync;
using POS_Common.Models.Sync.Custom.SelectSiteSyncStatus;
using POS_Common.Models.Sync.Custom.UpsertSiteSyncStatus;
using POS_Api.Helpers;
using POS_Api.Models.Email;
using POS_Api.ServiceInterfaces.Email;
using POS_Common.Models.Sync.Custom.SelectLocationRecipients;
using System.IO;
using POS_Api.Services.EntityData;

namespace POS_Api.Services.Sync
{
    public class Sync_Service : Sync_Custom_Service, ISync_Service
    {
        #region Members

        private readonly ILogging_Service _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        private readonly ICache_Service _cacheService;
        private readonly IBusinessCentral_Service _businessCentralService;
        private readonly IEmail_Service _emailService;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Sync_Service(IConfiguration configuration, ILogging_Service logger
            , IHttpContextAccessor httpContextAccessor, IUserContext userContext, ICache_Service cacheService, IBusinessCentral_Service businessCentralService
            , IEmail_Service emailService)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
            _cacheService = cacheService;
            _businessCentralService = businessCentralService;
            _emailService = emailService;

            Current_User_Management();
        }
        #endregion

        #region Helper Methods

        // Use IHttpContextAccessor to access HttpContext
        private string GetIpAddressFromRequest()
        {
            return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }

        // Use IHttpContextAccessor to access HttpContext
        private string GetUserAgentFromRequest()
        {
            return _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
        }

        public async void Current_User_Management()
        {
            try
            {
                var creditorResponse = await Base_Service.Current_User_Management(new User()
                {
                    UserID = _userContext.UserID,
                    Firstname = _userContext.Firstname,
                    Lastname = _userContext.Lastname,
                    Username = _userContext.Username
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region To Server Receive Helpers

        private object ToDb(object value)
        {
            return value ?? DBNull.Value;
        }

        private DataTable BuildBulkInsertToServerTable()
        {
            var table = new DataTable();

            table.Columns.Add("Int1", typeof(int));
            table.Columns.Add("Int2", typeof(int));
            table.Columns.Add("Int3", typeof(int));
            table.Columns.Add("Int4", typeof(int));
            table.Columns.Add("Int5", typeof(int));
            table.Columns.Add("Int6", typeof(int));
            table.Columns.Add("Int7", typeof(int));
            table.Columns.Add("Int8", typeof(int));
            table.Columns.Add("Int9", typeof(int));
            table.Columns.Add("Int10", typeof(int));

            table.Columns.Add("Guid1", typeof(Guid));
            table.Columns.Add("Guid2", typeof(Guid));
            table.Columns.Add("Guid3", typeof(Guid));
            table.Columns.Add("Guid4", typeof(Guid));
            table.Columns.Add("Guid5", typeof(Guid));
            table.Columns.Add("Guid6", typeof(Guid));
            table.Columns.Add("Guid7", typeof(Guid));
            table.Columns.Add("Guid8", typeof(Guid));
            table.Columns.Add("Guid9", typeof(Guid));
            table.Columns.Add("Guid10", typeof(Guid));

            table.Columns.Add("String1", typeof(string));
            table.Columns.Add("String2", typeof(string));
            table.Columns.Add("String3", typeof(string));
            table.Columns.Add("String4", typeof(string));
            table.Columns.Add("String5", typeof(string));
            table.Columns.Add("String6", typeof(string));
            table.Columns.Add("String7", typeof(string));
            table.Columns.Add("String8", typeof(string));
            table.Columns.Add("String9", typeof(string));
            table.Columns.Add("String10", typeof(string));
            table.Columns.Add("String11", typeof(string));
            table.Columns.Add("String12", typeof(string));
            table.Columns.Add("String13", typeof(string));
            table.Columns.Add("String14", typeof(string));
            table.Columns.Add("String15", typeof(string));
            table.Columns.Add("String16", typeof(string));

            table.Columns.Add("Decimal1", typeof(decimal));
            table.Columns.Add("Decimal2", typeof(decimal));
            table.Columns.Add("Decimal3", typeof(decimal));
            table.Columns.Add("Decimal4", typeof(decimal));
            table.Columns.Add("Decimal5", typeof(decimal));
            table.Columns.Add("Decimal6", typeof(decimal));
            table.Columns.Add("Decimal7", typeof(decimal));
            table.Columns.Add("Decimal8", typeof(decimal));
            table.Columns.Add("Decimal9", typeof(decimal));
            table.Columns.Add("Decimal10", typeof(decimal));

            table.Columns.Add("Date1", typeof(DateTime));
            table.Columns.Add("Date2", typeof(DateTime));
            table.Columns.Add("Date3", typeof(DateTime));
            table.Columns.Add("Date4", typeof(DateTime));
            table.Columns.Add("Date5", typeof(DateTime));
            table.Columns.Add("Date6", typeof(DateTime));
            table.Columns.Add("Date7", typeof(DateTime));
            table.Columns.Add("Date8", typeof(DateTime));
            table.Columns.Add("Date9", typeof(DateTime));
            table.Columns.Add("Date10", typeof(DateTime));

            table.Columns.Add("Bool1", typeof(bool));
            table.Columns.Add("Bool2", typeof(bool));
            table.Columns.Add("Bool3", typeof(bool));
            table.Columns.Add("Bool4", typeof(bool));
            table.Columns.Add("Bool5", typeof(bool));

            return table;
        }

        private string GetAppDbConnectionString()
        {
            return _configuration.GetConnectionString("ApplicationDb_1");
        }

        private async Task ExecuteBulkUpsertToServerAsync(string procedureName, DataTable table, SqlConnection sqlConn)
        {
            using var cmd = new SqlCommand(procedureName, sqlConn);
            cmd.CommandType = CommandType.StoredProcedure;

            var param = cmd.Parameters.AddWithValue("@Rows", table);
            param.SqlDbType = SqlDbType.Structured;
            param.TypeName = "dbo.BulkInsertToServer";

            await cmd.ExecuteNonQueryAsync();
        }

        private async Task<ApiResponse<bool>> ExecuteSingleItemBulkSyncAsync(
            DataTable tvp,
            string procedureName,
            string logName)
        {
            try
            {
                _logger.LogService($"Starting {logName} sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                await ExecuteBulkUpsertToServerAsync(procedureName, tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService($"Exception during {logName} Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        #endregion

        #region Methods

        #region From Server

        public async Task<ApiResponse<List<Res_Location_Sync>>> List_Locations()
        {
            try
            {
                _logger.LogService("Starting Location List");

                var syncResponse = await Debtors.Debtors_Custom_Service.POS_Locations_Select_All(new Location()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Location_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        var currencies = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Currencies;
                        var currencySymbol = sync.FK_CurrencyID != null
                               ? currencies.FirstOrDefault(x => x.CurrencyID == sync.FK_CurrencyID).Currency
                               : null;
                        var currency = CurrencySymbol.GetSymbol(currencySymbol);

                        response.Add(new Res_Location_Sync()
                        {
                            LocationID = sync.LocationID,
                            ShortCode = sync.ShortCode,
                            Name = sync.Name,
                            Currency = currency,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_Location_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_CostCenter_Sync>>> List_Cost_Centers()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Debtors.Debtors_Custom_Service.POS_CostCenters_Select_All(new CostCenter()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CostCenter_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_CostCenter_Sync()
                        {
                            CostCenterID = sync.CostCenterID,
                            FK_LocationID = sync.FK_LocationID,
                            Name = sync.Name,
                            BillingReference = sync.BillingReference,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_CostCenter_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_SlipPrinter_Sync>>> List_Slip_Printers()
        {
            try
            {
                _logger.LogService("Starting Slip Printer List");

                var syncResponse = await POS_SlipPrinters_Select_All(new SlipPrinter()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_SlipPrinter_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_SlipPrinter_Sync()
                        {
                            SlipPrinterID = sync.SlipPrinterID,
                            FK_LocationID = sync.FK_LocationID,
                            FK_CostCenterID = sync.CostCenterID,
                            Name = sync.Name,
                            Model = sync.Model,
                            IpAddress = sync.IpAddress,
                            Port = sync.Port,
                            IsDefault = sync.IsDefault,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during slip printer list", ex);
                return ApiResponse.Fail<List<Res_SlipPrinter_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Unit_Sync>>> List_Units()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory.Inventory_Custom_Service.POS_Units_Select_All(new Unit()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Unit_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_Unit_Sync()
                        {
                            UnitID = sync.UnitID,
                            Unit = sync.Unit,
                            Symbol = sync.Symbol,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_Unit_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_TaxType_Sync>>> List_Tax_Types()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.POS_TaxTypes_Select_All(new TaxType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_TaxType_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_TaxType_Sync()
                        {
                            TaxTypeID = sync.TaxTypeID,
                            TaxName = sync.TaxName,
                            TaxPercentage = sync.TaxPercentage,
                            ValidFrom = sync.ValidFrom,
                            ValidTo = sync.ValidTo,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_TaxType_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Product_Sync>>> List_Products()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory.Inventory_Custom_Service.POS_Products_Select_All(new Product()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Product_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_Product_Sync()
                        {
                            ProductID = sync.ProductID,
                            ProductName = sync.ProductName,
                            Description = sync.Description,
                            IsStockTracked = sync.IsStockTracked,
                            FK_UnitID = sync.FK_UnitID,
                            FK_DefaultUnitID = sync.FK_DefaultUnitID,
                            //FK_DefaultTaxTypeID = sync.FK_DefaultTaxTypeID,
                            SKU = sync.SKU,
                            Barcode = sync.Barcode,
                            QrCode = sync.QrCode,
                            IsActive = sync.IsActive,
                            DateAdded = sync.DateAdded,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_Product_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_LocationProduct_Sync>>> List_Location_Products()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Stock.Stock_Custom_Service.POS_DebtorProducts_Select_All(new DebtorProduct()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_LocationProduct_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_LocationProduct_Sync()
                        {
                            LocationProductID = sync.DebtorProductID,
                            FK_ProductID = sync.FK_ProductID,
                            FK_LocationID = sync.FK_LocationID,
                            //FK_TaxTypeID = sync.FK_TaxTypeID,
                            //Value = sync.Value,
                            //Vat = sync.Vat,
                            CostPrice = sync.CostPrice,
                            FK_SellUnitID = sync.FK_SellUnitID,
                            QuantityOnHand = sync.QuantityOnHand,
                            IsAvailable = sync.IsAvailable,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_LocationProduct_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_DebtorProductPrices_Sync>>> List_Debtor_Product_Prices()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Stock.Stock_Custom_Service.POS_DebtorProductPrices_Select_All(new DebtorProductPrice()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_DebtorProductPrices_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_DebtorProductPrices_Sync()
                        {
                            DebtorProductPriceID = sync.DebtorProductPriceID,
                            FK_DebtorProductID = sync.FK_DebtorProductID,
                            FK_PriceCodeID = sync.FK_PriceCodeID,
                            ItemPrice = sync.ItemPrice,
                            Inclusive = sync.Inclusive,
                            Vat = sync.Vat,
                            FK_TaxID = sync.FK_TaxID,
                            StartDate = sync.StartDate,
                            EndDate = sync.EndDate,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated,
                            FK_DefaultUnitID = sync.FK_DefaultUnitID
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_DebtorProductPrices_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_PriceCode_Sync>>> List_Price_Codes()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Stock.Stock_Custom_Service.POS_PriceCodes_Select_All(new PriceCodes()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_PriceCode_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_PriceCode_Sync()
                        {
                            PriceCodeID = sync.PriceCodeID,
                            PriceCode = sync.PriceCode,
                            Description = sync.Description,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_PriceCode_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_CostCenterProduct_Sync>>> List_Cost_Center_Products()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Stock.Stock_Custom_Service.POS_CostCenterProducts_Select_All(new CostCenterProduct()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CostCenterProduct_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_CostCenterProduct_Sync()
                        {
                            CostCenterProductID = sync.CostCenterProductID,
                            FK_ProductID = sync.FK_ProductID,
                            FK_CostCenterID = sync.FK_CostCenterID,
                            FK_TaxTypeID = sync.FK_TaxTypeID,
                            Value = sync.Value,
                            Vat = sync.Vat,
                            ItemPrice = sync.ItemPrice,
                            FK_SellUnitID = sync.FK_SellUnitID,
                            QuantityOnHand = sync.QuantityOnHand,
                            IsAvailable = sync.IsAvailable,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_CostCenterProduct_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_BookingHeader_Sync>>> List_Booking_Headers()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.TH_BookingHeaders_Select_All(new BookingHeader()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_BookingHeader_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_BookingHeader_Sync()
                        {
                            BookingHeaderID = sync.BookingHeaderID,
                            PartyName = sync.PartyName,
                            BookingReference = sync.BookingReference,
                            TravelStart = sync.TravelStart,
                            TravelEnd = sync.TravelEnd,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_BookingHeader_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Guest_Sync>>> List_Guests()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.Guests_Select_All(new Guest()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Guest_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_Guest_Sync()
                        {
                            GuestID = sync.GuestID,
                            Title = sync.Title,
                            FirstName = sync.FirstName,
                            MiddleName = sync.MiddleName,
                            LastName = sync.LastName,
                            DateOfBirth = sync.DateOfBirth,
                            Gender = sync.Gender,
                            Nationality = sync.Nationality,
                            PreferredLanguage = sync.PreferredLanguage,
                            SpecialRequests = sync.SpecialRequests,
                            LoyaltyNumber = sync.LoyaltyNumber,
                            Notes = sync.Notes,
                            CreatedDate = sync.DateCreated,
                            UpdatedDate = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_Guest_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_BookingGuest_Sync>>> List_Booking_Guests()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.TH_BookingGuests_Select_All(new BookingGuest()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_BookingGuest_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_BookingGuest_Sync()
                        {
                            BookingGuestID = sync.BookingGuestID,
                            FK_BookingHeaderID = sync.FK_BookingHeaderID,
                            FK_GuestID = sync.FK_GuestID,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_BookingGuest_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_PaymentType_Sync>>> List_Payment_Types()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.POS_PaymentTypes_Select_All(new PaymentType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_PaymentType_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_PaymentType_Sync()
                        {
                            PaymentTypeID = sync.PaymentTypeID,
                            Name = sync.Name,
                            IsActive = sync.IsActive,
                            IsPrimary = sync.IsPrimary,
                            IsSecondary = sync.IsSecondary,
                            SettlePayment = sync.SettlePayment,
                            RequireAdditionalInfo = sync.RequireAdditionalInfo,
                            RequireElevation = sync.RequireElevation,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated,
                            FK_PaymentTypeIconID = sync.FK_PaymentTypeIcon
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_PaymentType_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Menu_Sync>>> List_Menus()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Menu.Menu_Custom_Service.POS_DebtorMenus_Select_All(new DebtorMenu()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Menu_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_Menu_Sync()
                        {
                            POS_DebtorMenuID = sync.DebtorMenuID,
                            FK_CostCenterID = sync.FK_CostCenterID,
                            FK_MenuID = sync.FK_MenuID,
                            MenuName = sync.MenuName,
                            ValidFrom = sync.ValidFrom,
                            ValidTo = sync.ValidTo,
                            FK_LocationID = sync.FK_LocationID,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_Menu_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_MenuPrinter_Sync>>> List_Menu_Printers()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Menu.Menu_Custom_Service.POS_DebtorMenuPrinters_Select_All(new DebtorMenuPrinter()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_MenuPrinter_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_MenuPrinter_Sync()
                        {
                            POS_DebtorMenuPrinterID = sync.DebtorMenuPrinterID,
                            FK_DebtorMenuID = sync.FK_DebtorMenuID,
                            FK_PrinterID = sync.FK_PrinterID,
                            FK_OrderSlipTypeID = sync.FK_OrderSlipTypeID,
                            FK_CreatedUserID = sync.FK_CreatedUserID,
                            FK_UpdatedUserID = sync.FK_UpdatedUserID,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_MenuPrinter_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_MenuItem_Sync>>> List_Menu_Items()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Menu.Menu_Custom_Service.POS_DebtorMenuItems_Select_All(new DebtorMenuItem()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_MenuItem_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_MenuItem_Sync()
                        {
                            POS_DebtorMenuItemID = sync.DebtorMenuItemID,
                            FK_DebtorMenuID = sync.FK_DebtorMenuID,
                            Item = sync.Item,
                            Description = sync.Description,
                            FK_MenuItemID = sync.FK_MenuItemID,
                            FK_ReferenceInsertID = sync.FK_ReferenceInsertID,
                            FK_CreatedUserID = sync.FK_CreatedUserID,
                            FK_UpdatedUserID = sync.FK_UpdatedUserID,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_MenuItem_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_MenuItemProduct_Sync>>> List_Menu_Item_Products()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Menu.Menu_Custom_Service.POS_DebtorMenuItemProducts_Select_All(new DebtorMenuItemProduct()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_MenuItemProduct_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_MenuItemProduct_Sync()
                        {
                            POS_MenuItemProductID = sync.MenuItemProductID,
                            FK_DebtorMenuItemID = sync.FK_DebtorMenuItemID,
                            FK_ProductID = sync.FK_ProductID,
                            FK_CreatedUserID = sync.FK_CreatedUserID,
                            FK_UpdatedUserID = sync.FK_UpdatedUserID,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated,
                            DisplayOrder = sync.DisplayOrder
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_MenuItemProduct_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_MenuItemProductPrinter_Sync>>> List_Menu_Item_Product_Printers()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Menu.Menu_Custom_Service.POS_DebtorMenuItemProductPrinters_Select_All(new DebtorMenuItemProductPrinter()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_MenuItemProductPrinter_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_MenuItemProductPrinter_Sync()
                        {
                            POS_DebtorMenuItemProductPrinterID = sync.DebtorMenuItemProductPrinterID,
                            FK_MenuItemProductID = sync.FK_MenuItemProductID,
                            FK_PrinterID = sync.FK_PrinterID,
                            FK_CreatedUserID = sync.FK_CreatedUserID,
                            FK_UpdatedUserID = sync.FK_UpdatedUserID,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_MenuItemProductPrinter_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ProductCombination_Sync>>> List_Product_Combinations()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory.Inventory_Custom_Service.POS_ProductCombinations_Select_All(new ProductCombination()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductCombination_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_ProductCombination_Sync()
                        {
                            ProductCombinationID = sync.ProductCombinationID,
                            FK_ProductID = sync.FK_ProductID,
                            FK_ProductItemID = sync.FK_ProductItemID,
                            IsQuantified = sync.IsQuantified,
                            Quantity = sync.Quantity,
                            IsOptional = sync.IsOptional,
                            IsExtraCharge = sync.IsExtraCharge,
                            DisplayOrder = sync.DisplayOrder,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ProductCombination_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ProductExtra_Sync>>> List_Product_Extras()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory.Inventory_Custom_Service.POS_ProductExtras_Select_All(new ProductExtra()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductExtra_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_ProductExtra_Sync()
                        {
                            ProductExtraID = sync.ProductExtraID,
                            FK_ProductID = sync.FK_ProductID,
                            FK_ProductExtraCategoryID = sync.FK_ProductExtraCategoryID,
                            FK_ProductExtraID = sync.FK_ProductExtraID,
                            IsQuantified = sync.IsQuantified,
                            Quantity = sync.Quantity,
                            IsExtraCharge = sync.IsExtraCharge,
                            DisplayOrder = sync.DisplayOrder,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ProductExtra_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ProductExtraCategory_Sync>>> List_Product_Extra_Categories()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory.Inventory_Custom_Service.POS_ProductExtraCategories_Select_All(new ProductExtraCategory()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductExtraCategory_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_ProductExtraCategory_Sync()
                        {
                            ProductExtraCategoryID = sync.ProductExtraCategoryID,
                            Category = sync.Category,
                            DisplayOrder = sync.DisplayOrder,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ProductExtraCategory_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ProductPreparation_Sync>>> List_Product_Preparation()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory.Inventory_Custom_Service.POS_ProductPreparation_Select_All(new ProductPreparation()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductPreparation_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_ProductPreparation_Sync()
                        {
                            ProductPreparationID = sync.ProductPreparationID,
                            FK_ProductID = sync.FK_ProductID,
                            FK_ProductPreparationMethodID = sync.FK_ProductPreparationMethodID,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ProductPreparation_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ProductPreparationMethod_Sync>>> List_Product_Preparation_Methods()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory.Inventory_Custom_Service.POS_ProductPreparationMethods_Select_All(new ProductPreparationMethod()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductPreparationMethod_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_ProductPreparationMethod_Sync()
                        {
                            ProductPreparationMethodID = sync.ProductPreparationMethodID,
                            ShortCode = sync.ShortCode,
                            Method = sync.Method,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ProductPreparationMethod_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ProductSubstitution_Sync>>> List_Product_Substitutions()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory.Inventory_Custom_Service.POS_ProductSubstitutions_Select_All(new ProductSubstitution()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductSubstitution_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_ProductSubstitution_Sync()
                        {
                            ProductSubstitutionID = sync.ProductSubstitutionID,
                            FK_ProductID = sync.FK_ProductID,
                            FK_ProductSubstitutionID = sync.FK_ProductSubstitutionID,
                            IsQuantified = sync.IsQuantified,
                            Quantity = sync.Quantity,
                            IsExtraCharge = sync.IsExtraCharge,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ProductSubstitution_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Image_Sync>>> List_Images()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.POS_Images_Select_All(new Image()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Image_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_Image_Sync()
                        {
                            ImageID = sync.ImageID,
                            FK_ImageCategoryID = sync.FK_ImageCategoryID,
                            FK_ItemID = sync.FK_ItemID,
                            FileSystemPath = sync.FileSystemPath,
                            RelativePath = sync.RelativePath,
                            ImageName = sync.ImageName,
                            FileExtension = sync.FileExtension,
                            ImageUrl = sync.ImageUrl,
                            LocalUrl = sync.LocalUrl,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_Image_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ImageCategory_Sync>>> List_Image_Categories()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.POS_ImageCategories_Select_All(new ImageCategory()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ImageCategory_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        response.Add(new Res_ImageCategory_Sync()
                        {
                            ImageCategoryID = sync.ImageCategoryID,
                            Category = sync.Category,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ImageCategory_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_PaymentTypeIcon_Sync>>> List_Payment_Type_Icons()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.POS_PaymentTypeIcons_Select_All(new PaymentTypeIcon()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_PaymentTypeIcon_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        response.Add(new Res_PaymentTypeIcon_Sync()
                        {
                            PaymentTypeIconID = sync.PaymentTypeIconID,
                            IconPath = sync.IconPath,
                            Category = sync.Category,
                            DateCreated = sync.DateCreated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_PaymentTypeIcon_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Settings_Sync>>> List_Settings()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.POS_Settings_Select_All(new Settings()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Settings_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_Settings_Sync()
                        {
                            SettingID = sync.SettingID,
                            CompanyName = sync.CompanyName,
                            Email = sync.Email,
                            HeadOfficeNo = sync.HeadOfficeNo,
                            FK_CurrencyID = sync.FK_CurrencyID,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_Settings_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ServedAs_Sync>>> List_Served_As()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory_Service.POS_ServedAs_Select_All(new ServedAs()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ServedAs_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        response.Add(new Res_ServedAs_Sync()
                        {
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated,
                            FK_CreatedUserID = sync.FK_CreatedUserID,
                            FK_UpdatedUserID = sync.FK_UpdatedUserID,
                            Name = sync.Name,
                            ServedAsID = sync.ServedAsID,
                            ServedAsType = sync.ServedAsType
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ServedAs_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_ServedAsProducts_Sync>>> List_Served_As_Products()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Inventory_Service.POS_ServedAsProducts_Select_All(new ServedAsProduct()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ServedAsProducts_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        response.Add(new Res_ServedAsProducts_Sync()
                        {
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated,
                            FK_CreatedUserID = sync.FK_CreatedUserID,
                            FK_UpdatedUserID = sync.FK_UpdatedUserID,
                            FK_ProductID = (int)sync.FK_ProductID,
                            FK_ServedAsID = (int)sync.FK_ServedAsID,
                            IsQuantified = (bool)sync.IsQuantified,
                            Quantity = (decimal)sync.Quantity,
                            ServedAsProductID = (int)sync.ServedAsProductID,
                            IsDefault = (bool)sync.IsDefault
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_ServedAsProducts_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_Currency_Sync>>> List_Currencies()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData_Service.Currencies_Select_All(new Currency()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Currency_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        response.Add(new Res_Currency_Sync()
                        {
                            Currency = sync.Currency,
                            CurrencyID = sync.CurrencyID,
                            ISO2Code = sync.ISO2Code,
                            Name = sync.Name,
                            Symbol = sync.Symbol
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during currencies list", ex);
                return ApiResponse.Fail<List<Res_Currency_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_LocationCurrency_Sync>>> List_Location_Currencies()
        {
            try
            {
                _logger.LogService("Starting Location List");

                var syncResponse = await Debtors.Debtors_Custom_Service.POS_LocationCurrencies_Select_All(new LocationCurrencies()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_LocationCurrency_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_LocationCurrency_Sync()
                        {
                            LocationCurrencyID = sync.LocationCurrencyID,
                            FK_LocationID = sync.FK_LocationID,
                            FK_CurrencyID = sync.FK_CurrencyID,
                            IsActive = sync.IsActive,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_LocationCurrency_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_CurrencyExchangeRate_Sync>>> List_Currency_Exchange_Rates()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData_Service.CurrencyExchangeRates_Select_All(new CurrencyExchangeRate()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CurrencyExchangeRate_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        response.Add(new Res_CurrencyExchangeRate_Sync()
                        {
                            ConversionMethod = sync.ConversionMethod,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated,
                            CurrencyExchangeRateID = sync.CurrencyExchangeRateID,
                            EffectiveDate = sync.EffectiveDate,
                            ExchangeRate = sync.ExchangeRate,
                            FK_FromCurrencyID = sync.FK_FromCurrencyID,
                            FK_ToCurrencyID = sync.FK_ToCurrencyID,
                            Notes = sync.Notes
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during currency exchange rates list", ex);
                return ApiResponse.Fail<List<Res_CurrencyExchangeRate_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_GlobalSettings_Sync>>> List_Global_Settings()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData.EntityData_Custom_Service.GlobalSettings_Select_All(new GlobalSettings()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_GlobalSettings_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {

                        response.Add(new Res_GlobalSettings_Sync()
                        {
                            GlobalSettingID = sync.GlobalSettingID,
                            Key = sync.Key,
                            Value = sync.Value,
                            Environment = sync.Environment,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during location list", ex);
                return ApiResponse.Fail<List<Res_GlobalSettings_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_CostCenterPrinter_Sync>>> List_Cost_Center_Printers()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await Debtors.Debtors_Custom_Service.POS_CostCenterPrinters_Select_All(new CostCenterPrinter()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CostCenterPrinter_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        response.Add(new Res_CostCenterPrinter_Sync()
                        {
                            CostCenterPrinterID = sync.CostCenterPrinterID,
                            FK_CostCenterID = sync.FK_CostCenterID,
                            FK_PrinterID = sync.FK_PrinterID,
                            FK_InvoiceSlipTypeID = sync.FK_InvoiceSlipTypeID,
                            FK_TabSlipTypeID = sync.FK_TabSlipTypeID,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during cost center printers list", ex);
                return ApiResponse.Fail<List<Res_CostCenterPrinter_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_SlipTypes_Sync>>> List_Slip_Types()
        {
            try
            {
                _logger.LogService("Starting Sync List");

                var syncResponse = await EntityData_Service.POS_SlipTypes_Select_All(new SlipType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_SlipTypes_Sync>();

                if (syncResponse != null && syncResponse.Any())
                {
                    foreach (var sync in syncResponse)
                    {
                        response.Add(new Res_SlipTypes_Sync()
                        {
                            SlipTypeID = sync.SlipTypeID,
                            Description = sync.Description,
                            SlipCode = sync.SlipCode,
                            SlipType = sync.SlipType,
                            DateCreated = sync.DateCreated,
                            DateUpdated = sync.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during slip types list", ex);
                return ApiResponse.Fail<List<Res_SlipTypes_Sync>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<ImageBytesResult>> Get_Image_Bytes(int imageId, CancellationToken cancellationToken = default)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()));

                var image = await EntityData.EntityData_Base_Service.POS_Images_Select_Single(new Image()
                {
                    ImageID = imageId
                }, connectionString);

                if (image == null)
                {
                    _logger.LogService($"Get_Image_Bytes: image row not found for imageId={imageId}");
                    return ApiResponse.Fail<ImageBytesResult>(AppErrorCode.NotFound,
                        new List<string> { $"Image {imageId} not found" }, 404);
                }

                // FileSystemPath stores the templated root only (e.g. "{Path}\{TenantID}"); the actual file
                // lives at <expanded root>\<RelativePath>\<ImageName>. Expand via GlobalSettings.Image_Admin_Server_Path
                // (filtered by Environment) before any File.* call.
                var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                    .Where(x => x.Environment == _configuration["Environment"]).ToList();
                var adminImagePath = globalSettings.FirstOrDefault(x => x.Key == "Image_Admin_Server_Path")?.Value ?? "";
                var resolvedRoot = (image.FileSystemPath ?? "")
                    .Replace("{Path}", adminImagePath)
                    .Replace("{TenantID}", _userContext.TenantID.ToString());
                var resolvedPath = Path.Combine(resolvedRoot, image.RelativePath ?? "", image.ImageName ?? "");

                if (string.IsNullOrWhiteSpace(image.FileSystemPath) || !File.Exists(resolvedPath))
                {
                    _logger.LogService($"Get_Image_Bytes: file missing on disk for imageId={imageId}, path={resolvedPath}");
                    return ApiResponse.Fail<ImageBytesResult>(AppErrorCode.NotFound,
                        new List<string> { $"Image {imageId} file missing on disk" }, 404);
                }

                var bytes = await File.ReadAllBytesAsync(resolvedPath, cancellationToken);
                var contentType = MapContentType(image.FileExtension);

                var lastModified = image.DateUpdated
                                   ?? image.DateCreated
                                   ?? File.GetLastWriteTimeUtc(resolvedPath);

                // Ensure DateTimeKind.Utc so "R" format produces correct "GMT" timezone marker.
                lastModified = lastModified.Kind switch
                {
                    DateTimeKind.Utc => lastModified,
                    DateTimeKind.Local => lastModified.ToUniversalTime(),
                    _ => DateTime.SpecifyKind(lastModified, DateTimeKind.Utc),
                };

                return ApiResponse.Success(new ImageBytesResult
                {
                    Bytes = bytes,
                    ContentType = contentType,
                    LastModified = lastModified,
                });
            }
            catch (Exception ex)
            {
                _logger.LogService("Get_Image_Bytes failed", ex);
                return ApiResponse.Fail<ImageBytesResult>(AppErrorCode.ServerError,
                    new List<string> { ex.Message }, 500);
            }
        }

        private static string MapContentType(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension)) return "application/octet-stream";
            var ext = fileExtension.Trim().ToLowerInvariant();
            if (!ext.StartsWith(".")) ext = "." + ext;
            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",
                ".tif" or ".tiff" => "image/tiff",
                _ => "application/octet-stream",
            };
        }
        #endregion

        #region To Server

        public async Task<ApiResponse<bool>> List_Booking_Headers(List<BookingHeader_Sync> items)
        {
            try
            {
                _logger.LogService("Starting BookingHeaders sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<BookingHeader_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Int1"] = ToDb(item.BookingHeaderID);
                    row["String1"] = ToDb(item.PartyName);
                    row["String2"] = ToDb(item.BookingReference);
                    row["Date1"] = ToDb(item.TravelStart);
                    row["Date2"] = ToDb(item.TravelEnd);
                    row["Date3"] = ToDb(item.DateCreated);
                    row["Date4"] = ToDb(item.DateUpdated);
                    row["Bool1"] = ToDb(item.IsStaffBooking);
                    row["String3"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_BookingHeaders", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during BookingHeaders Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Guests(List<Guest_Sync> items)
        {
            try
            {
                _logger.LogService("Starting Guests sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<Guest_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Int1"] = ToDb(item.GuestID);
                    row["String1"] = ToDb(item.Title);
                    row["String2"] = ToDb(item.FirstName);
                    row["String3"] = ToDb(item.MiddleName);
                    row["String4"] = ToDb(item.LastName);
                    row["Date1"] = ToDb(item.DateOfBirth);
                    row["String5"] = ToDb(item.Gender);
                    row["String6"] = ToDb(item.Nationality);
                    row["String7"] = ToDb(item.PreferredLanguage);
                    row["String8"] = ToDb(item.SpecialRequests);
                    row["String9"] = ToDb(item.LoyaltyNumber);
                    row["String10"] = ToDb(item.Notes);
                    row["String11"] = ToDb(item.SyncStatus);
                    row["Date2"] = ToDb(item.DateCreated);
                    row["Date3"] = ToDb(item.DateUpdated);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_Guests", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Guests Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Booking_Guests(List<BookingGuest_Sync> items)
        {
            try
            {
                _logger.LogService("Starting BookingGuests sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<BookingGuest_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Int1"] = ToDb(item.BookingGuestID);
                    row["Int2"] = ToDb(item.FK_BookingHeaderID);
                    row["Int3"] = ToDb(item.FK_GuestID);
                    row["Date1"] = ToDb(item.DateCreated);
                    row["Date2"] = ToDb(item.DateUpdated);
                    row["String1"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_BookingGuests", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during BookingGuests Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Accounts(List<Account_Sync> items)
        {
            try
            {
                _logger.LogService("Starting Accounts sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<Account_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.AccountID);
                    row["String1"] = ToDb(item.Name);
                    row["Int1"] = ToDb(item.FK_BookingHeaderID);
                    row["Bool1"] = ToDb(item.IsClosed);
                    row["Int2"] = ToDb(item.FK_ResponsibleID);
                    row["Date1"] = ToDb(item.DateCreated);
                    row["Date2"] = ToDb(item.DateUpdated);
                    row["String2"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_Accounts", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Accounts Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Account_Guests(List<AccountGuest_Sync> items)
        {
            try
            {
                _logger.LogService("Starting AccountGuests sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<AccountGuest_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.AccountGuestID);
                    row["Guid2"] = ToDb(item.FK_AccountID);
                    row["Int1"] = ToDb(item.FK_GuestID);
                    row["Bool1"] = ToDb(item.IsResponsible);
                    row["Date1"] = ToDb(item.DateCreated);
                    row["Date2"] = ToDb(item.DateUpdated);
                    row["String1"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_AccountGuests", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during AccountGuests Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Arrivals(List<Arrival_Sync> items)
        {
            try
            {
                _logger.LogService("Starting Arrivals sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<Arrival_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.ArrivalID);
                    row["Int1"] = ToDb(item.FK_GuestID);
                    row["String2"] = ToDb(item.CheckedInBy);
                    row["Date1"] = ToDb(item.CheckInDate);
                    row["String3"] = ToDb(item.CheckedOutBy);
                    row["Date2"] = ToDb(item.CheckOutDate);
                    row["String4"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_Arrivals", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Arrivals Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_CashUp_Headers(List<CashUpHeader_Sync> items)
        {
            try
            {
                _logger.LogService("Starting CashUpHeaders sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<CashUpHeader_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.CashUpHeaderID);
                    row["Int1"] = ToDb(item.FK_CostCenterID);
                    row["Int2"] = ToDb(item.FK_CurrencyID);
                    row["Date1"] = ToDb(item.CashUpDate);
                    row["String2"] = ToDb(item.CashedUpBy);
                    row["Decimal1"] = ToDb(item.TotalSystemAmount);
                    row["Decimal2"] = ToDb(item.TotalCountedAmount);
                    row["Decimal3"] = ToDb(item.TotalVariance);
                    row["String1"] = ToDb(item.Notes);
                    row["Bool1"] = ToDb(item.IsFinalised);
                    row["Date2"] = ToDb(item.DateCreated);
                    row["Date3"] = ToDb(item.DateUpdated);
                    row["String3"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_CashUpHeaders", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during CashUpHeaders Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_CashUp_Lines(List<CashUpLine_Sync> items)
        {
            try
            {
                _logger.LogService("Starting CashUpLines sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<CashUpLine_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.CashUpPaymentTypeID);
                    row["Guid2"] = ToDb(item.FK_CashUpID);
                    row["Int1"] = ToDb(item.FK_PaymentTypeID);
                    row["Decimal1"] = ToDb(item.SystemAmount);
                    row["Decimal2"] = ToDb(item.CountedAmount);
                    row["Decimal3"] = ToDb(item.VarianceAmount);
                    row["String1"] = ToDb(item.Notes);
                    row["Date1"] = ToDb(item.DateCreated);
                    row["Date2"] = ToDb(item.DateUpdated);
                    row["String2"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_CashUpLines", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during CashUpLines Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Tabs(List<Tab_Sync> items)
        {
            try
            {
                _logger.LogService("Starting Tabs sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<Tab_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.TabID);
                    row["Int1"] = ToDb(item.FK_LocationID);
                    row["String4"] = ToDb(item.CreatedBy);
                    row["Guid2"] = ToDb(item.FK_AccountID);
                    row["Int3"] = ToDb(item.FK_CostCenterID);
                    row["Int4"] = ToDb(item.FK_PaymentTypeID);
                    row["String1"] = ToDb(item.TabName);
                    row["Int5"] = ToDb(item.TableName);
                    row["Int6"] = ToDb(item.NoOfGuests);
                    row["Decimal1"] = ToDb(item.Gratuity);
                    row["Int7"] = ToDb(item.GratuityPerc);
                    row["Decimal2"] = ToDb(item.Discount);
                    row["Int8"] = ToDb(item.DiscountPerc);
                    row["Bool1"] = ToDb(item.IsVoided);
                    row["String2"] = ToDb(item.VoidNote);
                    row["Bool2"] = ToDb(item.IsPaid);
                    row["Decimal3"] = ToDb(item.AmountPaid);
                    row["Decimal4"] = ToDb(item.AmountDue);
                    row["Decimal5"] = ToDb(item.VatTotal);
                    row["Date1"] = ToDb(item.PaymentDate);
                    row["Date2"] = ToDb(item.ClosedDate);
                    row["String3"] = ToDb(item.AdditionalInfo);
                    row["Date3"] = ToDb(item.DateCreated);
                    row["Date4"] = ToDb(item.DateUpdated);
                    row["Int10"] = ToDb(item.FK_CurrencyID);
                    row["Decimal6"] = ToDb(item.CurrentExchangeRate);
                    row["String5"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_Tabs", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Tabs Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Tab_Lines(List<TabLine_Sync> items)
        {
            try
            {
                _logger.LogService("Starting TabLines sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<TabLine_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.TabLineID);
                    row["Guid2"] = ToDb(item.FK_TabID);
                    row["String6"] = ToDb(item.CreatedBy);
                    row["Int2"] = ToDb(item.FK_ProductID);
                    row["Int3"] = ToDb(item.FK_PriceCodeID);
                    row["Guid3"] = ToDb(item.FK_PointerID);
                    row["Decimal1"] = ToDb(item.UnitCostExcl);
                    row["Decimal2"] = ToDb(item.Vat);
                    row["Decimal3"] = ToDb(item.UnitCostIncl);
                    row["String1"] = ToDb(item.Product);
                    row["Decimal4"] = ToDb(item.Quantity);
                    row["Decimal5"] = ToDb(item.Discount);
                    row["Int4"] = ToDb(item.DiscountPerc);
                    row["Bool1"] = ToDb(item.IsVoided);
                    row["String2"] = ToDb(item.Notes);
                    row["String3"] = ToDb(item.AutoNotes);
                    row["Date1"] = ToDb(item.DateCreated);
                    row["Date2"] = ToDb(item.DateUpdated);
                    row["String4"] = ToDb(item.ServedAs);
                    row["Bool2"] = ToDb(item.ServedAsQuantified);
                    row["Decimal6"] = ToDb(item.ServedAsQuantity);
                    row["Int5"] = ToDb(item.FK_MenuID);
                    row["String5"] = ToDb(item.MenuName);
                    row["String7"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_TabLines", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during TabLines Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_TabLine_Combinations(List<TabLineCombination_Sync> items)
        {
            try
            {
                _logger.LogService("Starting TabLineCombinations sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<TabLineCombination_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.TabLineCombinationID);
                    row["Guid2"] = ToDb(item.FK_TabLineID);
                    row["Int1"] = ToDb(item.FK_ProductCombinationID);
                    row["String1"] = ToDb(item.Product);
                    row["Bool1"] = ToDb(item.Hold);
                    row["String2"] = ToDb(item.Notes);
                    row["String3"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_TabLineCombinations", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during TabLineCombinations Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_TabLine_Extras(List<TabLineExtra_Sync> items)
        {
            try
            {
                _logger.LogService("Starting TabLineExtras sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<TabLineExtra_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.TabLineExtraID);
                    row["Guid2"] = ToDb(item.FK_TabLineID);
                    row["Int1"] = ToDb(item.FK_ProductID);
                    row["String1"] = ToDb(item.Product);
                    row["String2"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_TabLineExtras", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during TabLineExtras Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_TabLine_Guests(List<TabLineGuest_Sync> items)
        {
            try
            {
                _logger.LogService("Starting TabLineGuests sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<TabLineGuest_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.TabLineGuestID);
                    row["Guid2"] = ToDb(item.FK_TabLineID);
                    row["Int1"] = ToDb(item.FK_GuestID);
                    row["String1"] = ToDb(item.Note);
                    row["Date1"] = ToDb(item.DateUpdated);
                    row["String2"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_TabLineGuests", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during TabLineGuests Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_TabLine_Preparation_Methods(List<TabLinePreparationMethod_Sync> items)
        {
            try
            {
                _logger.LogService("Starting TabLinePreparationMethods sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<TabLinePreparationMethod_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.TabLinePreparationMethodID);
                    row["Guid2"] = ToDb(item.FK_TabLineCombinationID);
                    row["Int1"] = ToDb(item.FK_PreparationMethodID);
                    row["String1"] = ToDb(item.PreparationMethodName);
                    row["String2"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_TabLinePreparationMethods", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during TabLinePreparationMethods Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Tabline_Substitutes(List<TablineSubstitute_Sync> items)
        {
            try
            {
                _logger.LogService("Starting TablineSubstitutes sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<TablineSubstitute_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.TablineSubstituteID);
                    row["Guid2"] = ToDb(item.FK_ParentTabLineID);
                    row["Guid3"] = ToDb(item.FK_SubstituionTabLineID);
                    row["Guid4"] = ToDb(item.FK_ParentTabLineCombinationID);
                    row["String1"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_TablineSubstitutes", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during TablineSubstitutes Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Invoice_Headers(List<InvoiceHeader_Sync> items)
        {
            try
            {
                _logger.LogService("Starting InvoiceHeaders sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<InvoiceHeader_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.InvoiceHeaderID);
                    row["Guid2"] = ToDb(item.FK_AccountID);
                    row["Int1"] = ToDb(item.FK_LocationID);
                    row["String1"] = ToDb(item.InvoiceNo);
                    row["String2"] = ToDb(item.PartyName);
                    row["String3"] = ToDb(item.BookingReference);
                    row["Decimal1"] = ToDb(item.DiscountTotal);
                    row["Decimal2"] = ToDb(item.GratuityTotal);
                    row["Decimal3"] = ToDb(item.ExclTotal);
                    row["Decimal4"] = ToDb(item.VatTotal);
                    row["Decimal5"] = ToDb(item.InclTotal);
                    row["Bool1"] = ToDb(item.IsDiscarded);
                    row["Date1"] = ToDb(item.DateCreated);
                    row["Date2"] = ToDb(item.DatePaid);
                    row["String4"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_InvoiceHeaders", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during InvoiceHeaders Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Invoice_Tabs(List<InvoiceTab_Sync> items)
        {
            try
            {
                _logger.LogService("Starting InvoiceTabs sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<InvoiceTab_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.InvoiceTabID);
                    row["Guid2"] = ToDb(item.FK_InvoiceHeaderID);
                    row["Guid3"] = ToDb(item.FK_TabID);
                    row["Decimal1"] = ToDb(item.TabGratuity);
                    row["Decimal2"] = ToDb(item.TabDiscount);
                    row["Decimal3"] = ToDb(item.TabTotalExcl);
                    row["Decimal4"] = ToDb(item.TabTotalVat);
                    row["Decimal5"] = ToDb(item.TabTotalIncl);
                    row["Date1"] = ToDb(item.TabDateOpened);
                    row["Date2"] = ToDb(item.TabDateClosed);
                    row["String1"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_InvoiceTabs", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during InvoiceTabs Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Invoice_Lines(List<InvoiceLine_Sync> items)
        {
            try
            {
                _logger.LogService("Starting InvoiceLines sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<InvoiceLine_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.InvoiceLineID);
                    row["Guid2"] = ToDb(item.FK_InvoiceTabID);
                    row["String1"] = ToDb(item.Product);
                    row["Decimal1"] = ToDb(item.Quantity);
                    row["Decimal2"] = ToDb(item.LineDiscount);
                    row["Decimal3"] = ToDb(item.LineTotalExcl);
                    row["Decimal4"] = ToDb(item.LineTotalVat);
                    row["Decimal5"] = ToDb(item.LineTotalIncl);
                    row["String2"] = ToDb(item.Guests);
                    row["Int1"] = ToDb(item.FK_ProductID);
                    row["String3"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_InvoiceLines", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during InvoiceLines Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Invoice_Payments(List<InvoicePayment_Sync> items)
        {
            try
            {
                _logger.LogService("Starting InvoicePayments sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<InvoicePayment_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.InvoicePaymentID);
                    row["Guid2"] = ToDb(item.FK_InvoiceID);
                    row["Int1"] = ToDb(item.FK_PaymentTypeID);
                    row["Int2"] = ToDb(item.FK_FromCurrencyID);
                    row["Int3"] = ToDb(item.FK_ToCurrencyID);
                    row["String1"] = ToDb(item.FromCurrency);
                    row["String2"] = ToDb(item.ToCurrency);
                    row["Decimal1"] = ToDb(item.FromTotal);
                    row["Decimal2"] = ToDb(item.ToTotal);
                    row["Decimal3"] = ToDb(item.FromAmountPaid);
                    row["Decimal4"] = ToDb(item.ToAmountPaid);
                    row["Decimal5"] = ToDb(item.ExchangeRate);
                    row["Date1"] = ToDb(item.ExchangeDate);
                    row["Date2"] = ToDb(item.DatePaid);
                    row["String3"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_InvoicePayments", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during InvoicePayments Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<bool>> List_Void_Logs(List<VoidLog_Sync> items)
        {
            try
            {
                _logger.LogService("Starting VoidLogs sync Add");

                using var sqlConn = SqlClient.CreateInstance(GetAppDbConnectionString());
                await sqlConn.OpenAsync();

                var tvp = BuildBulkInsertToServerTable();

                foreach (var item in items ?? new List<VoidLog_Sync>())
                {
                    var row = tvp.NewRow();
                    row["Guid1"] = ToDb(item.VoidLogID);
                    row["Guid2"] = ToDb(item.FK_TabID);
                    row["Guid3"] = ToDb(item.FK_TabLineID);
                    row["String2"] = ToDb(item.VoidedBy);
                    row["String1"] = ToDb(item.Note);
                    row["Date1"] = ToDb(item.DateCreated);
                    row["Date2"] = ToDb(item.DateUpdated);
                    row["String3"] = ToDb(item.SyncStatus);
                    tvp.Rows.Add(row);
                }

                await ExecuteBulkUpsertToServerAsync("dbo.BulkUpsertToServer_VoidLogs", tvp, sqlConn);

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during VoidLogs Sync Add", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        #endregion

        #region Admin

        public async Task<ApiResponse<bool>> Notify_Result(Req_Notify_Result request)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()));

                // 1. Read current state for (SiteId, TypeName)
                var existing = await Sync_Custom_SP_Service.SelectSiteSyncStatus(
                    new Req_SelectSiteSyncStatus { SiteId = request.SiteId, TypeName = request.TypeName },
                    connectionString);
                var current = existing?.FirstOrDefault();

                var state = new SiteSyncStatusState
                {
                    ConsecutiveFailures = current?.ConsecutiveFailures ?? 0,
                    LastSuccessAt       = current?.LastSuccessAt,
                    LastFailureAt       = current?.LastFailureAt,
                    LastErrorMessage    = current?.LastErrorMessage,
                    AlertSentAt         = current?.AlertSentAt,
                };

                // 2. Apply state machine (threshold=3 from spec)
                var (next, shouldEmail) = SiteSyncStatusStateMachine.Apply(
                    state, request.Status, request.ErrorMessage, request.ObservedAt, threshold: 3);

                // 3. Persist via Upsert wrapper
                await Sync_Custom_SP_Service.UpsertSiteSyncStatus(
                    new Req_UpsertSiteSyncStatus
                    {
                        SiteId              = request.SiteId,
                        TypeName            = request.TypeName,
                        LastSuccessAt       = next.LastSuccessAt,
                        LastFailureAt       = next.LastFailureAt,
                        ConsecutiveFailures = next.ConsecutiveFailures,
                        LastErrorMessage    = next.LastErrorMessage,
                        LastReportedAt      = request.ObservedAt,
                        AlertSentAt         = next.AlertSentAt,
                    },
                    connectionString);

                if (shouldEmail)
                {
                    var recipients = await Sync_Custom_SP_Service.SelectLocationRecipients(
                        new Req_SelectLocationRecipients { SiteId = request.SiteId },
                        connectionString);
                    var recipient = recipients?.FirstOrDefault();
                    if (recipient != null && (!string.IsNullOrWhiteSpace(recipient.ContactEmail) || !string.IsNullOrWhiteSpace(recipient.SupportEmail)))
                    {
                        await _emailService.Send_Sync_Failure_Email(new SyncFailureEmail
                        {
                            SiteId = request.SiteId,
                            SiteName = recipient.SiteName,
                            TypeName = request.TypeName,
                            ErrorMessage = request.ErrorMessage,
                            ConsecutiveFailures = next.ConsecutiveFailures,
                            LastSuccessAt = next.LastSuccessAt,
                            To = new[] { recipient.ContactEmail, recipient.SupportEmail }
                                 .Where(s => !string.IsNullOrWhiteSpace(s)).ToList(),
                        });
                    }
                }

                return ApiResponse.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogService("Notify_Result failed", ex);
                return ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        #endregion

        #endregion
    }
}