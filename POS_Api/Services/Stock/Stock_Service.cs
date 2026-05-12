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
using POS_Api.ServiceInterfaces.Stock;
using POS_Api.Services.Stock;
using POS_Common.ModelsDto.StockController.PurchaseOrder;
using POS_Common.Models.Debtors.Debtors;
using POS_Common.ModelsDto.DebtorsController;
using POS_Common.Models.Stock.POS_PurchaseOrders;
using POS_Common.Models.Debtors.DebtorTypeMappings;
using POS_Common.Models.Stock.POS_PurchaseOrderLines;
using Microsoft.Data.SqlClient;
using System.Transactions;
using TMIS_Common.Sql;
using POS_Common.ModelsDto.StockController.PurchaseOrderLine;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrder;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrderLines;
using POS_Common.ModelsDto.StockController.StockRequest;
using POS_Common.Models.Stock.POS_StockRequests;
using Microsoft.VisualBasic;
using POS_Common.ModelsDto.StockController.StockRequestLine;
using POS_Common.Models.Stock.POS_StockRequestLines;
using POS_Common.Models.EntityData.Statuses;
using TMIS_Common.Interfaces;
using POS_Common.Models.EntityData.Users;
using POS_Common.ModelsDto.StockController.StockTransfer;
using POS_Common.Models.Stock.POS_StockTransfers;
using POS_Common.ModelsDto.StockController.DebtorProduct;
using POS_Common.Models.Stock.POS_DebtorProducts;
using POS_Common.Models.Stock.POS_DebtorProductPriceHistory;
using POS_Common.ModelsDto.StockController.CostCenterProduct;
using POS_Common.Models.Stock.POS_CostCenterProducts;
using POS_Common.ModelsDto.StockController.SupplierProduct;
using POS_Api.ServiceInterfaces.Cache;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.Stock.POS_CostCenterProductPriceHistory;
using POS_Common.ModelsDto.StockController.PriceCode;
using POS_Common.Models.Stock.POS_PriceCodes;
using POS_Common.ModelsDto.StockController.DebtorProductPrice;
using POS_Common.Models.Stock.POS_DebtorProductPrices;
using POS_Api.ServiceInterfaces.Email;
using POS_Api.Models.Email;
using POS_Common.Models.Stock.POS_StockRequestReviewers;
using StockRequestLine = POS_Common.Models.Stock.POS_StockRequestLines.StockRequestLine;

namespace POS_Api.Services
{
    public class Stock_Service : Stock_Custom_Service, IStock_Service
    {
        #region Members

        private readonly ILogging_Service _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        private readonly ICache_Service _cacheService;
        private readonly IEmail_Service _emailService;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Stock_Service(IConfiguration configuration, ILogging_Service logger
            , IHttpContextAccessor httpContextAccessor, IUserContext userContext, ICache_Service cacheService
            , IEmail_Service emailService)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
            _cacheService = cacheService;
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

        #region Methods

        #region Price Codes

        public async Task<ApiResponse<List<Res_PriceCode_List>>> List_Price_Codes()
        {
            try
            {
                _logger.LogService("Starting Price Codes List");

                var priceCodeResponse = await POS_PriceCodes_Select_All(new PriceCodes()
                {
                    
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_PriceCode_List>();

                if (priceCodeResponse != null && priceCodeResponse.Any())
                {
                    foreach (var priceCode in priceCodeResponse)
                    {

                        response.Add(new Res_PriceCode_List()
                        {
                            POS_PriceCodeID = priceCode.PriceCodeID,
                            PriceCode = priceCode.PriceCode,
                            Description = priceCode.Description,
                            IsActive = priceCode.IsActive
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_PriceCode_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Price_Code(Req_PriceCode_Add request)
        {
            try
            {
                _logger.LogService("Starting Price Codes Add", request);

                //var tax = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Tax;
                //var taxRate = tax.FirstOrDefault(x => x.POS_TaxTypeID == request.FK_TaxTypeID)?.TaxPercentage;
                //var vat = taxRate != null ? (request.Value * taxRate) / 100 : null;

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var priceCodeInsert = await POS_PriceCodes_Insert(new PriceCodes()
                    {
                        PriceCode = request.PriceCode,
                        Description = request.Description,
                        IsActive = request.IsActive,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Price Code add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Price_Code(Req_PriceCode_Update request)
        {
            try
            {
                _logger.LogService("Starting Price Codes Update", request);

                //var tax = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Tax;
                //var taxRate = tax.FirstOrDefault(x => x.POS_TaxTypeID == request.FK_TaxTypeID)?.TaxPercentage;
                //var vat = taxRate != null ? (request.Value * taxRate) / 100 : null;

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var priceCodeResponse = await POS_PriceCodes_Select_Single(new PriceCodes()
                    {
                        PriceCodeID = request.POS_PriceCodeID
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                    var debtorProductUpdate = await POS_PriceCodes_Update(new PriceCodes()
                    {
                        PriceCodeID = request.POS_PriceCodeID,
                        PriceCode = request.PriceCode,
                        Description = request.Description,
                        IsActive = request.IsActive,
                        DateUpdated = DateTime.Now,
                        DateCreated = priceCodeResponse.DateCreated
                    }, sqlConn); 
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Purchase Order add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Debtor Products

        public async Task<ApiResponse<List<Res_DebtorProduct_List>>> List_Debtor_Products(Req_DebtorProduct_List request)
        {
            try
            {
                _logger.LogService("Starting Debtor Products List");

                var debtorProductResponse = await DebtorProducts_Select_All_DebtorProducts(new DebtorProduct()
                {
                    FK_LocationID = request.DebtorID,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_DebtorProduct_List>();

                if (debtorProductResponse != null && debtorProductResponse.Any())
                {
                    foreach (var debtorProduct in debtorProductResponse)
                    {

                        response.Add(new Res_DebtorProduct_List()
                        {
                            POS_DebtorProductID = debtorProduct.DebtorProductID,
                            FK_ProductID = debtorProduct.FK_ProductID,
                            ProductName = debtorProduct.ProductName,
                            FK_DebtorID = debtorProduct.FK_LocationID,
                            Debtor = debtorProduct.Debtor,
                            FK_SellUnitID = debtorProduct.FK_SellUnitID,
                            Symbol = debtorProduct.Symbol,
                            Unit = debtorProduct.Unit,
                            QuantityOnHand = debtorProduct.QuantityOnHand,
                            IsAvailable = debtorProduct.IsAvailable,
                            IsActive = debtorProduct.IsActive,
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_DebtorProduct_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Debtor_Product(Req_DebtorProduct_Add request)
        {
            try
            {
                _logger.LogService("Starting Debtor Product Add", request);

                //var tax = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Tax;
                //var taxRate = tax.FirstOrDefault(x => x.POS_TaxTypeID == request.FK_TaxTypeID)?.TaxPercentage;
                //var vat = taxRate != null ? (request.Value * taxRate) / 100 : null;

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorProductInsert = await POS_DebtorProducts_Insert(new DebtorProduct()
                    {
                        FK_ProductID = request.FK_ProductID,
                        FK_LocationID = request.FK_DebtorID,
                        FK_SellUnitID = request.FK_SellUnitID,
                        CostPrice = request.CostPrice,
                        QuantityOnHand = request.QuantityOnHand,
                        IsAvailable = request.IsAvailable,
                        IsActive = request.IsActive,
                        FK_CreatedUserID = _userContext.UserID,
                        FK_UpdatedUserID = _userContext.UserID,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, sqlConn);

                    //var debtorProductPriceHistory = await POS_DebtorProductPriceHistory_Insert(new DebtorProductPriceHistory()
                    //{
                    //    FK_DebtorProductID = debtorProductInsert.POS_DebtorProductID,
                    //    Value = request.Value,
                    //    //Vat = debtorProductInsert.Vat,
                    //    ItemPrice = request.ItemPrice,
                    //    ValidFrom = DateTime.Now,
                    //    ValidTo = null,
                    //    FK_CreatedUserID = _userContext.UserID,
                    //    FK_UpdatedUserID = _userContext.UserID,
                    //    DateCreated = DateTime.Now,
                    //    DateUpdated = DateTime.Now
                    //}, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Debtor Products add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Debtor_Product(Req_DebtorProduct_Update request)
        {
            try
            {
                _logger.LogService("Starting Debtor Product Update", request);

                var debtorProductResponse = await POS_DebtorProducts_Select_Single(new DebtorProduct()
                {
                    DebtorProductID = request.POS_DebtorProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (debtorProductResponse == null)
                {
                    _logger.LogService("Purchase Order not found", request.POS_DebtorProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderNotFound, new List<string> { "Purchase Order not found." }, 400);
                }

                var debtorProductPriceHistoryResponse = await POS_DebtorProductPriceHistory_Select_FK_DebtorProductID(new DebtorProductPriceHistory()
                {
                    FK_DebtorProductID = request.POS_DebtorProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                //var tax = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Tax;
                //var taxRate = tax.FirstOrDefault(x => x.POS_TaxTypeID == request.FK_TaxTypeID)?.TaxPercentage;
                //var vat = taxRate != null ? (request.Value * taxRate) / 100 : null;

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorProductUpdate = await POS_DebtorProducts_Update(new DebtorProduct()
                    {
                        DebtorProductID = request.POS_DebtorProductID,
                        FK_ProductID = request.FK_ProductID ?? debtorProductResponse.FK_ProductID,
                        FK_LocationID = request.FK_DebtorID ?? debtorProductResponse.FK_LocationID,
                        FK_SellUnitID = request.FK_SellUnitID ?? debtorProductResponse.FK_SellUnitID,
                        QuantityOnHand = request.QuantityOnHand ?? debtorProductResponse.QuantityOnHand,
                        IsAvailable = request.IsAvailable ?? debtorProductResponse.IsAvailable,
                        IsActive = request.IsActive ?? debtorProductResponse.IsActive,
                        FK_UpdatedUserID = _userContext.UserID,
                        DateUpdated = DateTime.Now,
                        FK_CreatedUserID = debtorProductResponse.FK_CreatedUserID,
                        DateCreated = debtorProductResponse.DateCreated
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Purchase Order add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Debtor Product Prices

        public async Task<ApiResponse<List<Res_DebtorProductPrice_List>>> List_Debtor_Product_Prices(Req_DebtorProductPrice_List request)
        {
            try
            {
                _logger.LogService("Starting Debtor Products List");

                var debtorProductResponse = await DebtorProductPrices_Select_All_DebtorProducts(new DebtorProductPrice()
                {
                    FK_DebtorProductID = request.DebtorProductID,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var priceCode = _cacheService.GetCacheAsync(_userContext.TenantID).Result.PriceCodes;

                var response = new List<Res_DebtorProductPrice_List>();

                if (debtorProductResponse != null && debtorProductResponse.Any())
                {
                    foreach (var debtorProduct in debtorProductResponse)
                    {
                        response.Add(new Res_DebtorProductPrice_List()
                        {
                            POS_DebtorProductPriceID = debtorProduct.DebtorProductPriceID,
                            FK_DebtorProductID = debtorProduct.FK_DebtorProductID,
                            FK_PriceCodeID = debtorProduct.FK_PriceCodeID,
                            PriceCode = priceCode.FirstOrDefault(x => x.PriceCodeID == debtorProduct.FK_PriceCodeID)?.PriceCode,
                            FK_TaxID = debtorProduct.FK_TaxID,
                            ItemPrice = debtorProduct.ItemPrice,
                            Inclusive = debtorProduct.Inclusive,
                            Vat = debtorProduct.Vat,
                            StartDate = debtorProduct.StartDate,
                            EndDate = debtorProduct.EndDate,
                            IsActive = debtorProduct.IsActive
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_DebtorProductPrice_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Debtor_Product_Price(Req_DebtorProductPrice_Add request)
        {
            try
            {
                _logger.LogService("Starting Debtor Product Add", request);

                //var tax = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Tax;
                //var taxRate = tax.FirstOrDefault(x => x.POS_TaxTypeID == request.FK_TaxTypeID)?.TaxPercentage;
                //var vat = taxRate != null ? (request.Value * taxRate) / 100 : null;

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorProductInsert = await POS_DebtorProductPrices_Insert(new DebtorProductPrice()
                    {
                        FK_DebtorProductID = request.FK_DebtorProductID,
                        FK_PriceCodeID = request.FK_PriceCodeID,
                        FK_TaxID = request.FK_TaxID,
                        ItemPrice = request.ItemPrice,
                        Inclusive = request.Inclusive,
                        Vat = request.Vat,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        IsActive = request.IsActive,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, sqlConn);

                    //var debtorProductPriceHistory = await POS_DebtorProductPriceHistory_Insert(new DebtorProductPriceHistory()
                    //{
                    //    FK_DebtorProductID = debtorProductInsert.POS_DebtorProductPriceID,
                    //    Value = request.Value,
                    //    //Vat = debtorProductInsert.Vat,
                    //    ItemPrice = request.ItemPrice,
                    //    ValidFrom = DateTime.Now,
                    //    ValidTo = null,
                    //    FK_CreatedUserID = _userContext.UserID,
                    //    FK_UpdatedUserID = _userContext.UserID,
                    //    DateCreated = DateTime.Now,
                    //    DateUpdated = DateTime.Now
                    //}, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Debtor Products add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Debtor_Product_Price(Req_DebtorProductPrice_Update request)
        {
            try
            {
                _logger.LogService("Starting Debtor Product Update", request);

                var debtorProductResponse = await POS_DebtorProducts_Select_Single(new DebtorProduct()
                {
                    //POS_DebtorProductID = request.POS_DebtorProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (debtorProductResponse == null)
                {
                    //_logger.LogService("Purchase Order not found", request.POS_DebtorProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderNotFound, new List<string> { "Purchase Order not found." }, 400);
                }

                var debtorProductPriceHistoryResponse = await POS_DebtorProductPriceHistory_Select_FK_DebtorProductID(new DebtorProductPriceHistory()
                {
                    //FK_DebtorProductID = request.POS_DebtorProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                //var tax = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Tax;
                //var taxRate = tax.FirstOrDefault(x => x.POS_TaxTypeID == request.FK_TaxTypeID)?.TaxPercentage;
                //var vat = taxRate != null ? (request.Value * taxRate) / 100 : null;

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorProductUpdate = await POS_DebtorProducts_Update(new DebtorProduct()
                    {
                        //POS_DebtorProductID = request.POS_DebtorProductID,
                        //FK_ProductID = request.FK_ProductID ?? debtorProductResponse.FK_ProductID,
                        //FK_LocationID = request.FK_DebtorID ?? debtorProductResponse.FK_LocationID,
                        //FK_SellUnitID = request.FK_SellUnitID ?? debtorProductResponse.FK_SellUnitID,
                        //QuantityOnHand = request.QuantityOnHand ?? debtorProductResponse.QuantityOnHand,
                        //IsAvailable = request.IsAvailable ?? debtorProductResponse.IsAvailable,
                        //IsActive = request.IsActive ?? debtorProductResponse.IsActive,
                        //FK_UpdatedUserID = _userContext.UserID,
                        //DateUpdated = DateTime.Now,
                        //FK_CreatedUserID = debtorProductResponse.FK_CreatedUserID,
                        //DateCreated = debtorProductResponse.DateCreated
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Purchase Order add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Cost Center Products

        public async Task<ApiResponse<List<Res_CostCenterProduct_List>>> List_Cost_Center_Products(Req_CostCenterProduct_List request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Products List");

                var costCenterProductResponse = await CostCenterProducts_Select_All_CostCenterProducts(new CostCenterProduct()
                {
                    FK_CostCenterID = request.CostCenterId,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_CostCenterProduct_List>();

                if (costCenterProductResponse != null && costCenterProductResponse.Any())
                {
                    foreach (var costCenterProduct in costCenterProductResponse)
                    {

                        response.Add(new Res_CostCenterProduct_List()
                        {
                            POS_CostCenterProductID = costCenterProduct.CostCenterProductID,
                            FK_ProductID = costCenterProduct.FK_ProductID,
                            ProductName = costCenterProduct.ProductName,
                            FK_CostCenterID = costCenterProduct.FK_CostCenterID,
                            CostCenter = costCenterProduct.CostCenter,
                            FK_TaxTypeID = costCenterProduct.FK_TaxTypeID,
                            Rate = costCenterProduct.Rate,
                            Value = costCenterProduct.Value,
                            Vat = costCenterProduct.Vat,
                            ItemPrice = costCenterProduct.ItemPrice,
                            FK_SellUnitID = costCenterProduct.FK_SellUnitID,
                            Symbol = costCenterProduct.Symbol,
                            Unit = costCenterProduct.Unit,
                            QuantityOnHand = costCenterProduct.QuantityOnHand,
                            IsAvailable = costCenterProduct.IsAvailable,
                            IsActive = costCenterProduct.IsActive,
                            CreatedBy = costCenterProduct.CreatedBy,
                            UpdatedBy = costCenterProduct.UpdatedBy,
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_CostCenterProduct_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Cost_Center_Product(Req_CostCenterProduct_Add request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Product Add", request);

                var tax = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Tax;
                var taxRate = tax.FirstOrDefault(x => x.TaxTypeID == request.FK_TaxTypeID)?.TaxPercentage;
                var vat = taxRate != null ? (request.Value * taxRate) / 100 : null;

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorProductInsert = await POS_CostCenterProducts_Insert(new CostCenterProduct()
                    {
                        FK_ProductID = request.FK_ProductID,
                        FK_CostCenterID = request.FK_CostCenterID,
                        FK_TaxTypeID = request.FK_TaxTypeID,
                        Value = request.Value,
                        Vat = vat,
                        ItemPrice = request.ItemPrice,
                        FK_SellUnitID = request.FK_SellUnitID,
                        QuantityOnHand = request.QuantityOnHand,
                        IsAvailable = request.IsAvailable,
                        IsActive = request.IsActive,
                        FK_CreatedUserID = _userContext.UserID,
                        FK_UpdatedUserID = _userContext.UserID,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, sqlConn);

                    var debtorProductPriceHistory = await POS_CostCenterProductPriceHistory_Insert(new CostCenterProductPriceHistory()
                    {
                        FK_CostCenterProductID = debtorProductInsert.CostCenterProductID,
                        Value = debtorProductInsert.Value,
                        Vat = debtorProductInsert.Vat,
                        ItemPrice = debtorProductInsert.ItemPrice,
                        ValidFrom = DateTime.Now,
                        ValidTo = null,
                        FK_CreatedUserID = _userContext.UserID,
                        FK_UpdatedUserID = _userContext.UserID,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Cost Center Products add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Cost_Center_Product(Req_CostCenterProduct_Update request)
        {
            try
            {
                _logger.LogService("Starting Cost Center Product Update", request);

                var costCenterProductResponse = await POS_CostCenterProducts_Select_Single(new CostCenterProduct()
                {
                    CostCenterProductID = request.POS_CostCenterProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (costCenterProductResponse == null)
                {
                    _logger.LogService("Purchase Order not found", request.POS_CostCenterProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderNotFound, new List<string> { "Purchase Order not found." }, 400);
                }

                var costCenterProductPriceHistoryResponse = await CostCenterProductPriceHistory_Select_FK_CostCenterProductID(new CostCenterProductPriceHistory()
                {
                    FK_CostCenterProductID = request.POS_CostCenterProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var costCenterProductUpdate = await POS_CostCenterProducts_Update(new CostCenterProduct()
                    {
                        CostCenterProductID = request.POS_CostCenterProductID,
                        FK_ProductID = request.FK_ProductID ?? costCenterProductResponse.FK_ProductID,
                        FK_CostCenterID = request.FK_CostCenterID ?? costCenterProductResponse.FK_CostCenterID,
                        FK_TaxTypeID = request.FK_TaxTypeID ?? costCenterProductResponse.FK_TaxTypeID,
                        Value = request.Value ?? costCenterProductResponse.Value,
                        Vat = request.Vat ?? costCenterProductResponse.Vat,
                        ItemPrice = request.ItemPrice ?? costCenterProductResponse.ItemPrice,
                        FK_SellUnitID = request.FK_SellUnitID ?? costCenterProductResponse.FK_SellUnitID,
                        QuantityOnHand = request.QuantityOnHand ?? costCenterProductResponse.QuantityOnHand,
                        IsAvailable = request.IsAvailable ?? costCenterProductResponse.IsAvailable,
                        IsActive = request.IsActive ?? costCenterProductResponse.IsActive,
                        FK_UpdatedUserID = _userContext.UserID,
                        DateUpdated = DateTime.Now,
                        FK_CreatedUserID = costCenterProductResponse.FK_CreatedUserID,
                        DateCreated = costCenterProductResponse.DateCreated
                    }, sqlConn);

                    if (costCenterProductPriceHistoryResponse != null)
                    {
                        var costCenterProductHistoryUpdate = await POS_CostCenterProductPriceHistory_Update(new CostCenterProductPriceHistory()
                        {
                            CostcenterProductPriceHistoryID = costCenterProductPriceHistoryResponse.CostcenterProductPriceHistoryID,
                            FK_CostCenterProductID = costCenterProductPriceHistoryResponse.FK_CostCenterProductID,
                            Value = costCenterProductPriceHistoryResponse.Value,
                            Vat = costCenterProductPriceHistoryResponse.Vat,
                            ItemPrice = costCenterProductPriceHistoryResponse.ItemPrice,
                            ValidFrom = costCenterProductPriceHistoryResponse.ValidFrom,
                            ValidTo = DateTime.Now,
                            FK_UpdatedUserID = _userContext.UserID,
                            DateUpdated = DateTime.Now,
                            FK_CreatedUserID = costCenterProductPriceHistoryResponse.FK_CreatedUserID,
                            DateCreated = costCenterProductPriceHistoryResponse.DateCreated
                        }, sqlConn);

                        var costCenterProductPriceHistory = await POS_CostCenterProductPriceHistory_Insert(new CostCenterProductPriceHistory()
                        {
                            FK_CostCenterProductID = costCenterProductUpdate.CostCenterProductID,
                            Value = costCenterProductUpdate.Value,
                            Vat = costCenterProductUpdate.Vat,
                            ItemPrice = costCenterProductUpdate.ItemPrice,
                            ValidFrom = DateTime.Now,
                            ValidTo = null,
                            FK_CreatedUserID = _userContext.UserID,
                            FK_UpdatedUserID = _userContext.UserID,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }

                    else
                    {
                        var costCenterProductPriceHistory = await POS_CostCenterProductPriceHistory_Insert(new CostCenterProductPriceHistory()
                        {
                            FK_CostCenterProductID = costCenterProductUpdate.CostCenterProductID,
                            Value = costCenterProductUpdate.Value,
                            Vat = costCenterProductUpdate.Vat,
                            ItemPrice = costCenterProductUpdate.ItemPrice,
                            ValidFrom = DateTime.Now,
                            ValidTo = null,
                            FK_CreatedUserID = _userContext.UserID,
                            FK_UpdatedUserID = _userContext.UserID,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Purchase Order add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Supplier Products

        public async Task<ApiResponse<List<Res_SupplierProduct_List>>> List_Supplier_Products(Req_SupplierProduct_List request)
        {
            try
            {
                _logger.LogService("Starting Debtor Products List");

                var debtorProductResponse = await DebtorProducts_Select_All_DebtorProducts(new DebtorProduct()
                {
                    //FK_DebtorID = request.DebtorID,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_SupplierProduct_List>();

                if (debtorProductResponse != null && debtorProductResponse.Any())
                {
                    foreach (var debtorProduct in debtorProductResponse)
                    {

                        response.Add(new Res_SupplierProduct_List()
                        {
                            //POS_DebtorProductID = debtorProduct.POS_DebtorProductID,
                            //FK_ProductID = debtorProduct.FK_ProductID,
                            //ProductName = debtorProduct.ProductName,
                            //FK_DebtorID = debtorProduct.FK_DebtorID,
                            //Debtor = debtorProduct.Debtor,
                            //FK_TaxTypeID = debtorProduct.FK_TaxTypeID,
                            //Rate = debtorProduct.Rate,
                            //Value = debtorProduct.Value,
                            //Vat = debtorProduct.Vat,
                            //ItemPrice = debtorProduct.ItemPrice,
                            //FK_SellUnitID = debtorProduct.FK_SellUnitID,
                            //Symbol = debtorProduct.Symbol,
                            //Unit = debtorProduct.Unit,
                            //QuantityOnHand = debtorProduct.QuantityOnHand,
                            //IsAvailable = debtorProduct.IsAvailable,
                            //IsActive = debtorProduct.IsActive,
                            //CreatedBy = debtorProduct.CreatedBy,
                            //UpdatedBy = debtorProduct.UpdatedBy,
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during address list", ex);
                return ApiResponse.Fail<List<Res_SupplierProduct_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Supplier_Product(Req_SupplierProduct_Add request)
        {
            try
            {
                _logger.LogService("Starting Debtor Product Add", request);

                //var purchaseOrderResponse = await PurchaseOrder_Select_Single_Number(new POS_PurchaseOrder()
                //{
                //   //OrderNumber = request.OrderNumber
                //}, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                //if (purchaseOrderResponse != null)
                //{
                //    _logger.LogService("Purchase Order already exists", request.FK_DebtorID);
                //    return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderExists, new List<string> { "Purchase Order already exists." }, 400);
                //}

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorProductInsert = await POS_DebtorProducts_Insert(new DebtorProduct()
                    {
                        //FK_ProductID = request.FK_ProductID,
                        //FK_DebtorID = request.FK_DebtorID,
                        //FK_TaxTypeID = request.FK_TaxTypeID,
                        //Value = request.Value,
                        //Vat = request.Vat,
                        //ItemPrice = request.ItemPrice,
                        //FK_SellUnitID = request.FK_SellUnitID,
                        //QuantityOnHand = request.QuantityOnHand,
                        //IsAvailable = request.IsAvailable,
                        //IsActive = request.IsActive,
                        //FK_CreatedUserID = _userContext.UserID,
                        //FK_UpdatedUserID = _userContext.UserID,
                        //DateCreated = DateTime.Now,
                        //DateUpdated = DateTime.Now
                    }, sqlConn);

                    var debtorProductPriceHistory = await POS_DebtorProductPriceHistory_Insert(new DebtorProductPriceHistory()
                    {
                        //FK_DebtorProductID = debtorProductInsert.POS_DebtorProductID,
                        //Value = request.Value,
                        //Vat = request.Vat,
                        //ItemPrice = request.ItemPrice,
                        //ValidFrom = DateTime.Now,
                        //ValidTo = null,
                        //FK_CreatedUserID = _userContext.UserID,
                        //FK_UpdatedUserID = _userContext.UserID,
                        //DateCreated = DateTime.Now,
                        //DateUpdated = DateTime.Now
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Debtor Products add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Supplier_Product(Req_SupplierProduct_Update request)
        {
            try
            {
                _logger.LogService("Starting Debtor Product Update", request);

                var debtorProductResponse = await POS_DebtorProducts_Select_Single(new DebtorProduct()
                {
                    //POS_DebtorProductID = request.POS_DebtorProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (debtorProductResponse == null)
                {
                    //_logger.LogService("Purchase Order not found", request.POS_DebtorProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderNotFound, new List<string> { "Purchase Order not found." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorProduct = await POS_DebtorProducts_Update(new DebtorProduct()
                    {
                        //POS_DebtorProductID = request.POS_DebtorProductID,
                        //FK_ProductID = request.FK_ProductID ?? debtorProductResponse.FK_ProductID,
                        //FK_DebtorID = request.FK_DebtorID ?? debtorProductResponse.FK_DebtorID,
                        //FK_TaxTypeID = request.FK_TaxTypeID ?? debtorProductResponse.FK_TaxTypeID,
                        //Value = request.Value ?? debtorProductResponse.Value,
                        //Vat = request.Vat ?? debtorProductResponse.Vat,
                        //ItemPrice = request.ItemPrice ?? debtorProductResponse.ItemPrice,
                        //FK_SellUnitID = request.FK_SellUnitID ?? debtorProductResponse.FK_SellUnitID,
                        //QuantityOnHand = request.QuantityOnHand ?? debtorProductResponse.QuantityOnHand,
                        //IsAvailable = request.IsAvailable ?? debtorProductResponse.IsAvailable,
                        //IsActive = request.IsActive ?? debtorProductResponse.IsActive,
                        //FK_UpdatedUserID = _userContext.UserID,
                        //DateUpdated = DateTime.Now,
                        //FK_CreatedUserID = debtorProductResponse.FK_CreatedUserID,
                        //DateCreated = debtorProductResponse.DateCreated
                    }, sqlConn);

                    //var debtorProduct = await POS_DebtorProducts_Update(new POS_DebtorProduct()
                    //{
                    //    POS_DebtorProductID = request.POS_DebtorProductID,
                    //    FK_ProductID = request.FK_ProductID ?? debtorProductResponse.FK_ProductID,
                    //    FK_DebtorID = request.FK_DebtorID ?? debtorProductResponse.FK_DebtorID,
                    //    FK_TaxTypeID = request.FK_TaxTypeID ?? debtorProductResponse.FK_TaxTypeID,
                    //    Value = request.Value ?? debtorProductResponse.Value,
                    //    Vat = request.Vat ?? debtorProductResponse.Vat,
                    //    ItemPrice = request.ItemPrice ?? debtorProductResponse.ItemPrice,
                    //    FK_SellUnitID = request.FK_SellUnitID ?? debtorProductResponse.FK_SellUnitID,
                    //    QuantityOnHand = request.QuantityOnHand ?? debtorProductResponse.QuantityOnHand,
                    //    IsAvailable = request.IsAvailable ?? debtorProductResponse.IsAvailable,
                    //    IsActive = request.IsActive ?? debtorProductResponse.IsActive,
                    //    FK_UpdatedUserID = _userContext.UserID,
                    //    DateUpdated = DateTime.Now,
                    //    FK_CreatedUserID = debtorProductResponse.FK_CreatedUserID,
                    //    DateCreated = debtorProductResponse.DateCreated
                    //}, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Purchase Order add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #endregion


        //#region Purchase Orders

        //public async Task<ApiResponse<List<Res_PurchaseOrder_List>>> List_Purchase_Orders(Req_PurchaseOrder_List request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Purchase Order List");

        //        var purchaseOrderResponse = await PurchaseOrders_Select_All_PurchaseOrders(new POS_PurchaseOrder()
        //        {
        //            FK_DebtorID = request.DebtorID,
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


        //        var response = new List<Res_PurchaseOrder_List>();

        //        if (purchaseOrderResponse != null && purchaseOrderResponse.Any())
        //        {
        //            foreach (var purchaseOrder in purchaseOrderResponse)
        //            {

        //                response.Add(new Res_PurchaseOrder_List()
        //                {
        //                    POS_PurchaseOrderID = purchaseOrder.POS_PurchaseOrderID,
        //                    OrderNumber = purchaseOrder.OrderNumber,
        //                    SupplierID = purchaseOrder.FK_SupplierID,
        //                    SupplierName = purchaseOrder.SupplierName,
        //                    DebtorID = purchaseOrder.FK_DebtorID,
        //                    DebtorName = purchaseOrder.DebtorName,
        //                    CostCenterID = purchaseOrder.FK_CostCenterID,
        //                    CostCenterName = purchaseOrder.CostCenterName,
        //                    OrderStatusID = purchaseOrder.FK_OrderStatusID,
        //                    OrderStatus = purchaseOrder.OrderStatus,
        //                    CreatedBy = purchaseOrder.CreatedBy,
        //                    Notes = purchaseOrder.Notes,
        //                    ManagerNotes = purchaseOrder.ManagerNotes
        //                });
        //            }
        //        }

        //        return ApiResponse.Success(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during address list", ex);
        //        return ApiResponse.Fail<List<Res_PurchaseOrder_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}

        //public async Task<ApiResponse<object>> Add_Purchase_Order(Req_PurchaseOrder_Add request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Purchase Order Add", request);

        //        var purchaseOrderResponse = await PurchaseOrder_Select_Single_Number(new POS_PurchaseOrder()
        //        {
        //            OrderNumber = request.OrderNumber
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

        //        if (purchaseOrderResponse != null)
        //        {
        //            _logger.LogService("Purchase Order already exists", request.OrderNumber);
        //            return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderExists, new List<string> { "Purchase Order already exists." }, 400);
        //        }

        //        using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
        //        {
        //            await sqlConn.OpenAsync();

        //            int statusId = request.IsSubmitted == true ? 1 : 5;

        //            var purchaseOrderInsert = await POS_PurchaseOrders_Insert(new POS_PurchaseOrder()
        //            {
        //                OrderNumber = request.OrderNumber,
        //                FK_SupplierID = request.FK_SupplierID,
        //                FK_DebtorID = request.FK_DebtorID,
        //                FK_CostCenterID = request.FK_CostCenterID,
        //                FK_OrderStatusID = statusId,
        //                FK_UserID = 1,
        //                Notes = request.Notes,
        //                DateOrdered = DateTime.Now,
        //                DateUpdated = DateTime.Now
        //            }, sqlConn);
        //        }

        //        return ApiResponse.Success(new object());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during Purchase Order add", ex);
        //        return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}

        //public async Task<ApiResponse<object>> Update_Purchase_Order(Req_PurchaseOrder_Update request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Purchase Order Update", request);

        //        var purchaseOrderResponse = await POS_PurchaseOrders_Select_Single(new POS_PurchaseOrder()
        //        {
        //            POS_PurchaseOrderID = request.POS_PurchaseOrderID
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

        //        if (purchaseOrderResponse == null)
        //        {
        //            _logger.LogService("Purchase Order not found", request.POS_PurchaseOrderID);
        //            return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderNotFound, new List<string> { "Purchase Order not found." }, 400);
        //        }

        //        using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
        //        {
        //            await sqlConn.OpenAsync();

        //            int statusId = request.IsSubmitted == true ? 1 : 5;

        //            var purchaseOrderInsert = await POS_PurchaseOrders_Update(new POS_PurchaseOrder()
        //            {
        //                POS_PurchaseOrderID = request.POS_PurchaseOrderID,
        //                OrderNumber = request.OrderNumber ?? purchaseOrderResponse.OrderNumber,
        //                FK_SupplierID = request.FK_SupplierID ?? purchaseOrderResponse.FK_SupplierID,
        //                FK_DebtorID = request.FK_DebtorID ?? purchaseOrderResponse.FK_DebtorID,
        //                FK_CostCenterID = request.FK_CostCenterID ?? purchaseOrderResponse.FK_CostCenterID,
        //                FK_OrderStatusID = statusId,
        //                FK_UserID = 1,
        //                Notes = request.Notes ?? purchaseOrderResponse.Notes,
        //                DateOrdered = DateTime.Now,
        //                DateUpdated = DateTime.Now
        //            }, sqlConn);
        //        }

        //        return ApiResponse.Success(new object());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during Purchase Order add", ex);
        //        return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}
        //#endregion

        //#region Purchase Order Lines

        //public async Task<ApiResponse<List<Res_PurchaseOrderLine_List>>> List_Purchase_Order_Lines(Req_PurchaseOrderLine_List request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Purchase Order Line List");

        //        var purchaseOrderLineResponse = await PurchaseOrderLines_Select_All_PurchaseOrderLines(new POS_PurchaseOrderLine()
        //        {
        //            FK_PurchaseOrderID = request.PurchaseOrderID,
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


        //        var response = new List<Res_PurchaseOrderLine_List>();

        //        if (purchaseOrderLineResponse != null && purchaseOrderLineResponse.Any())
        //        {
        //            foreach (var purchaseOrderLine in purchaseOrderLineResponse)
        //            {
        //                response.Add(new Res_PurchaseOrderLine_List()
        //                {
        //                    POS_PurchaseOrderLineID = purchaseOrderLine.POS_PurchaseOrderLineID,
        //                    PurchaseOrderID = purchaseOrderLine.FK_PurchaseOrderID,
        //                    ProductID = purchaseOrderLine.FK_ProductID,
        //                    ProductName = purchaseOrderLine.ProductName,
        //                    Quantity = purchaseOrderLine.Quantity,
        //                    UnitCostExcl = purchaseOrderLine.UnitCostExcl,
        //                    UnitCostIncl = purchaseOrderLine.UnitCostIncl,
        //                    TaxTypeID = purchaseOrderLine.FK_TaxTypeID,
        //                    TaxRate = purchaseOrderLine.TaxRate,
        //                    TotalCostExcl = purchaseOrderLine.TotalCostExcl,
        //                    TotalCostIncl = purchaseOrderLine.TotalCostIncl,
        //                    Notes = purchaseOrderLine.Notes,
        //                    IsDeclined = purchaseOrderLine.IsDeclined,
        //                    MangerNotes = purchaseOrderLine.ManagerNotes
        //                });
        //            }
        //        }

        //        return ApiResponse.Success(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during Purchase Order Line list", ex);
        //        return ApiResponse.Fail<List<Res_PurchaseOrderLine_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}

        //public async Task<ApiResponse<object>> Add_Purchase_Order_Line(Req_PurchaseOrderLine_Add request)
        //{
        //    try
        //    {
        //        foreach (var line in request.PurchaseOrderLines)
        //        {
        //            _logger.LogService("Starting Purchase Order List Add", request);

        //            using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
        //            {
        //                await sqlConn.OpenAsync();

        //                var purchaseOrderResponse = await PurchaseOrder_Select_Single_ID(new POS_PurchaseOrder()
        //                {
        //                    POS_PurchaseOrderID = line.FK_PurchaseOrderID,
        //                    FK_ProductID = line.FK_ProductID
        //                }, sqlConn);

        //                var unitCost = purchaseOrderResponse?.UnitCost ?? 0;
        //                var taxRate = ((purchaseOrderResponse?.TaxRate) ?? 0) / 100;
        //                var unitCostExcl = unitCost;
        //                var unitCostIncl = unitCost * taxRate + unitCostExcl;
        //                var totalCostIncl = unitCostIncl * line.Quantity;
        //                var totalCostExcl = unitCost * line.Quantity;

        //                if (line.POS_PurchaseOrderLineID == null)
        //                {
        //                    var purchaseOrderInsert = await POS_PurchaseOrderLines_Insert(new POS_PurchaseOrderLine()
        //                    {
        //                        FK_PurchaseOrderID = line.FK_PurchaseOrderID,
        //                        FK_ProductID = line.FK_ProductID,
        //                        Quantity = line.Quantity,
        //                        UnitCostIncl = unitCostIncl,
        //                        UnitCostExcl = unitCostExcl,
        //                        FK_TaxTypeID = purchaseOrderResponse?.POS_TaxTypeID,
        //                        TaxRate = purchaseOrderResponse?.TaxRate ?? 0,
        //                        TotalCostIncl = totalCostIncl,
        //                        TotalCostExcl = totalCostExcl,
        //                        Notes = line.Notes,
        //                        IsDeclined = false
        //                    }, sqlConn);
        //                }

        //                else
        //                {
        //                    var purchaseOrderInsert = await POS_PurchaseOrderLines_Update(new POS_PurchaseOrderLine()
        //                    {
        //                        POS_PurchaseOrderLineID = line.POS_PurchaseOrderLineID,
        //                        FK_PurchaseOrderID = line.FK_PurchaseOrderID,
        //                        FK_ProductID = line.FK_ProductID,
        //                        Quantity = line.Quantity,
        //                        UnitCostIncl = unitCostIncl,
        //                        UnitCostExcl = unitCostExcl,
        //                        FK_TaxTypeID = purchaseOrderResponse?.POS_TaxTypeID,
        //                        TaxRate = purchaseOrderResponse?.TaxRate ?? 0,
        //                        TotalCostIncl = totalCostIncl,
        //                        TotalCostExcl = totalCostExcl,
        //                        Notes = line.Notes,
        //                        IsDeclined = false
        //                    }, sqlConn);
        //                }
        //            }
        //        }

        //        return ApiResponse.Success(new object());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during Purchase Order add", ex);
        //        return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
        //    }
        //}
        //#endregion

        //#region Purchase Order Manager Review

        //public async Task<ApiResponse<List<Res_SubmittedPurchaseOrder_List>>> List_Submitted_Purchase_Orders()
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Submitted Purchase Order List");

        //        var purchaseOrderResponse = await PurchaseOrders_Select_All_SubmittedPurchaseOrders(new POS_PurchaseOrder()
        //        {

        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


        //        var response = new List<Res_SubmittedPurchaseOrder_List>();

        //        if (purchaseOrderResponse != null && purchaseOrderResponse.Any())
        //        {
        //            foreach (var purchaseOrder in purchaseOrderResponse)
        //            {

        //                response.Add(new Res_SubmittedPurchaseOrder_List()
        //                {
        //                    POS_PurchaseOrderID = purchaseOrder.POS_PurchaseOrderID,
        //                    OrderNumber = purchaseOrder.OrderNumber,
        //                    SupplierID = purchaseOrder.FK_SupplierID,
        //                    SupplierName = purchaseOrder.SupplierName,
        //                    DebtorID = purchaseOrder.FK_DebtorID,
        //                    DebtorName = purchaseOrder.DebtorName,
        //                    CostCenterID = purchaseOrder.FK_CostCenterID,
        //                    CostCenterName = purchaseOrder.CostCenterName,
        //                    OrderStatusID = purchaseOrder.FK_OrderStatusID,
        //                    OrderStatus = purchaseOrder.OrderStatus,
        //                    CreatedBy = purchaseOrder.CreatedBy,
        //                    Notes = purchaseOrder.Notes,
        //                    ManagerNotes = purchaseOrder.ManagerNotes,
        //                });
        //            }
        //        }

        //        return ApiResponse.Success(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during address list", ex);
        //        return ApiResponse.Fail<List<Res_SubmittedPurchaseOrder_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}

        //public async Task<ApiResponse<object>> Update_Purchase_Order_Status(Req_PurchaseOrderStatus_Update request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Purchase Order Update", request);

        //        var purchaseOrderResponse = await POS_PurchaseOrders_Select_Single(new POS_PurchaseOrder()
        //        {
        //            POS_PurchaseOrderID = request.POS_PurchaseOrderID
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

        //        if (purchaseOrderResponse == null)
        //        {
        //            _logger.LogService("Purchase Order not found", request.POS_PurchaseOrderID);
        //            return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderNotFound, new List<string> { "Purchase Order not found." }, 400);
        //        }

        //        using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
        //        {
        //            await sqlConn.OpenAsync();

        //            int statusId = request.IsDeclined == true ? 6 : 2;

        //            var purchaseOrderInsert = await POS_PurchaseOrders_Update(new POS_PurchaseOrder()
        //            {
        //                POS_PurchaseOrderID = request.POS_PurchaseOrderID,
        //                OrderNumber = purchaseOrderResponse.OrderNumber,
        //                FK_SupplierID = purchaseOrderResponse.FK_SupplierID,
        //                FK_DebtorID = purchaseOrderResponse.FK_DebtorID,
        //                FK_CostCenterID = purchaseOrderResponse.FK_CostCenterID,
        //                FK_OrderStatusID = statusId,
        //                CreatedBy = "Test",
        //                ManagerNotes = request.ManagerNotes ?? purchaseOrderResponse.ManagerNotes,
        //                Notes = purchaseOrderResponse.Notes,
        //                FK_UserID = 1,
        //                DateOrdered = purchaseOrderResponse.DateOrdered,
        //                DateUpdated = DateTime.Now
        //            }, sqlConn);
        //        }

        //        return ApiResponse.Success(new object());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during Purchase Order Status Update", ex);
        //        return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}
        //#endregion

        //#region Purchase Order Lines Manager Review

        //public async Task<ApiResponse<List<Res_SubmittedPurchaseOrderLines_List>>> List_Submitted_Purchase_Order_Lines(Req_SubmittedPurchaseOrderLines_List request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Submitted Purchase Order Lines List");

        //        var purchaseOrderLineResponse = await PurchaseOrderLines_Select_All_SubmittedPurchaseOrderLines(new POS_PurchaseOrderLine()
        //        {
        //            FK_PurchaseOrderID = request.POS_PurchaseOrderID
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


        //        var response = new List<Res_SubmittedPurchaseOrderLines_List>();

        //        if (purchaseOrderLineResponse != null && purchaseOrderLineResponse.Any())
        //        {
        //            foreach (var purchaseOrderLine in purchaseOrderLineResponse)
        //            {

        //                response.Add(new Res_SubmittedPurchaseOrderLines_List()
        //                {
        //                    POS_PurchaseOrderLineID = purchaseOrderLine.POS_PurchaseOrderLineID,
        //                    PurchaseOrderID = purchaseOrderLine.FK_PurchaseOrderID,
        //                    ProductID = purchaseOrderLine.FK_ProductID,
        //                    ProductName = purchaseOrderLine.ProductName,
        //                    Quantity = purchaseOrderLine.Quantity,
        //                    UnitCostExcl = purchaseOrderLine.UnitCostExcl,
        //                    UnitCostIncl = purchaseOrderLine.UnitCostIncl,
        //                    TaxRate = purchaseOrderLine.TaxRate,
        //                    TotalCostExcl = purchaseOrderLine.TotalCostExcl,
        //                    TotalCostIncl = purchaseOrderLine.TotalCostIncl,
        //                    StockOnHand = purchaseOrderLine.StockOnHand,
        //                    Notes = purchaseOrderLine.Notes,
        //                    IsDeclined = purchaseOrderLine.IsDeclined,
        //                    MangerNotes = purchaseOrderLine.ManagerNotes
        //                });
        //            }
        //        }

        //        return ApiResponse.Success(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during address list", ex);
        //        return ApiResponse.Fail<List<Res_SubmittedPurchaseOrderLines_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}

        //public async Task<ApiResponse<object>> Update_Purchase_Order_Line_Status(Req_PurchaseOrderLineStatus_Update request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Purchase Order Line Update", request);

        //        var purchaseOrderLineResponse = await POS_PurchaseOrderLines_Select_Single(new POS_PurchaseOrderLine()
        //        {
        //            POS_PurchaseOrderLineID = request.POS_PurchaseOrderLineID
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

        //        if (purchaseOrderLineResponse == null)
        //        {
        //            _logger.LogService("Purchase Order Line not found", request.POS_PurchaseOrderLineID);
        //            return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderLineNotFound, new List<string> { "Purchase Order Line not found." }, 400);
        //        }

        //        using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
        //        {
        //            await sqlConn.OpenAsync();

        //            if (request.IsDeclined == true)
        //            {
        //                var purchaseOrderResponse = await POS_PurchaseOrders_Select_Single(new POS_PurchaseOrder()
        //                {
        //                    POS_PurchaseOrderID = request.FK_PurchaseOrderID
        //                }, sqlConn);

        //                var purchaseOrderInsert = await POS_PurchaseOrders_Update(new POS_PurchaseOrder()
        //                {
        //                    POS_PurchaseOrderID = purchaseOrderResponse.POS_PurchaseOrderID,
        //                    OrderNumber = purchaseOrderResponse.OrderNumber,
        //                    FK_SupplierID = purchaseOrderResponse.FK_SupplierID,
        //                    FK_DebtorID = purchaseOrderResponse.FK_DebtorID,
        //                    FK_CostCenterID = purchaseOrderResponse.FK_CostCenterID,
        //                    FK_OrderStatusID = 7,
        //                    ManagerNotes = purchaseOrderResponse.ManagerNotes,
        //                    Notes = purchaseOrderResponse.Notes,
        //                    FK_UserID = 1,
        //                    DateOrdered = purchaseOrderResponse.DateOrdered,
        //                    DateUpdated = DateTime.Now
        //                }, sqlConn);
        //            }

        //            var purchaseOrderLineUpdate = await POS_PurchaseOrderLines_Update(new POS_PurchaseOrderLine()
        //            {
        //                POS_PurchaseOrderLineID = request.POS_PurchaseOrderLineID,
        //                FK_PurchaseOrderID = request.FK_PurchaseOrderID,
        //                FK_ProductID = purchaseOrderLineResponse.FK_ProductID,
        //                Quantity = purchaseOrderLineResponse.Quantity,
        //                UnitCostIncl = purchaseOrderLineResponse.UnitCostIncl,
        //                UnitCostExcl = purchaseOrderLineResponse.UnitCostExcl,
        //                FK_TaxTypeID = purchaseOrderLineResponse.FK_TaxTypeID,
        //                TaxRate = purchaseOrderLineResponse.TaxRate,
        //                TotalCostIncl = purchaseOrderLineResponse.TotalCostIncl,
        //                TotalCostExcl = purchaseOrderLineResponse.TotalCostExcl,
        //                Notes = purchaseOrderLineResponse.Notes,
        //                ManagerNotes = request.ManagerNotes ?? purchaseOrderLineResponse.ManagerNotes,
        //                IsDeclined = request.IsDeclined ?? purchaseOrderLineResponse.IsDeclined
        //            }, sqlConn);
        //        }

        //        return ApiResponse.Success(new object());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during Purchase Order Line Update", ex);
        //        return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}
        //#endregion

        #region Stock Requests

        public async Task<ApiResponse<List<Res_StockRequest_List>>> List_Stock_Requests(Req_StockRequest_List request)
        {
            try
            {
                _logger.LogService("Starting Stock Request List", request);

                var stockRequestResponse = await StockRequests_Select_All_StockRequests(new StockRequest()
                {
                    FK_ToDebtorID    = request.ToDebtorID,
                    FK_FromDebtorID  = request.FromDebtorID,
                    FK_OrderStatusID = request.OrderStatusID
                }, GetConnectionString());

                var response = new List<Res_StockRequest_List>();

                if (stockRequestResponse != null && stockRequestResponse.Any())
                {
                    foreach (var sr in stockRequestResponse)
                    {
                        response.Add(new Res_StockRequest_List()
                        {
                            POS_StockRequestID  = sr.StockRequestID,
                            RefNumber           = sr.RefNumber,
                            FK_FromDebtorID     = sr.FK_FromDebtorID,
                            FromDebtorName      = sr.FromDebtorName,
                            FK_ToDebtorID       = sr.FK_ToDebtorID,
                            ToDebtorName        = sr.ToDebtorName,
                            FK_OrderStatusID    = sr.FK_OrderStatusID,
                            OrderStatus         = sr.OrderStatus,
                            CreatedBy           = sr.CreatedBy,
                            ManagerNotes        = sr.ManagerNotes,
                            Notes               = sr.Notes,
                            DateOrdered         = sr.DateOrdered,
                            DateUpdated         = sr.DateUpdated,
                            FK_ApprovedByUserID = sr.FK_ApprovedByUserID,
                            DateApproved        = sr.DateApproved
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Stock Request list", ex);
                return ApiResponse.Fail<List<Res_StockRequest_List>>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Stock_Request(Req_StockRequest_Add request)
        {
            try
            {
                _logger.LogService("Starting Stock Request Add", request);

                if (request.Lines == null || request.Lines.Count == 0)
                    return ApiResponse.Fail<object>(AppErrorCode.StockRequestEmpty, new List<string> { "At least one line is required." }, 400);

                var connectionString = GetConnectionString();

                int newId;
                string refNumber;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();

                    refNumber = await POS_Api.Services.DocumentSequence.DocumentSequence_Service.Get_Next_Reference(
                        POS_Api.Services.DocumentSequence.DocumentSequence_Service.DocumentTypes.StockRequest,
                        sqlConn);

                    if (string.IsNullOrEmpty(refNumber))
                    {
                        _logger.LogService("Failed to mint Stock Request reference number", request);
                        return ApiResponse.Fail<object>(AppErrorCode.DatabaseError, new List<string> { "Failed to generate Stock Request reference number." }, 500);
                    }

                    var inserted = await POS_StockRequests_Insert(new StockRequest()
                    {
                        RefNumber        = refNumber,
                        FK_FromDebtorID  = request.FK_FromDebtorID,
                        FK_ToDebtorID    = request.FK_ToDebtorID,
                        FK_OrderStatusID = (int)StockRequestStatus.Draft,
                        FK_UserID        = _userContext.UserID,
                        Notes            = request.Notes,
                        DateOrdered      = DateTime.Now,
                        DateUpdated      = DateTime.Now
                    }, sqlConn);

                    if (inserted?.StockRequestID == null)
                    {
                        _logger.LogService("Failed to insert Stock Request header", request);
                        return ApiResponse.Fail<object>(AppErrorCode.DatabaseError, new List<string> { "Failed to create Stock Request." }, 500);
                    }

                    newId = inserted.StockRequestID.Value;

                    foreach (var line in request.Lines)
                    {
                        var product = await POS_Api.Services.Inventory.Inventory_Base_Service.POS_Products_Select_Single(
                            new POS_Common.Models.Inventory.POS_Products.Product { ProductID = line.FK_ProductID },
                            sqlConn);

                        if (product == null)
                        {
                            _logger.LogService("Product not found for Stock Request line", line.FK_ProductID);
                            return ApiResponse.Fail<object>(AppErrorCode.ProductNotFound, new List<string> { $"Product {line.FK_ProductID} not found." }, 400);
                        }

                        var insertedLine = await POS_StockRequestLines_Insert(new StockRequestLine()
                        {
                            FK_StockRequestID = newId,
                            FK_ProductID      = line.FK_ProductID,
                            FK_UnitID         = product.FK_UnitID,
                            Quantity          = line.Quantity,
                            Notes             = line.Notes,
                            IsDeclined        = false
                        }, sqlConn);

                        if (insertedLine?.StockRequestLineID == null)
                        {
                            _logger.LogService("Failed to insert Stock Request line", line);
                            return ApiResponse.Fail<object>(AppErrorCode.DatabaseError, new List<string> { "Failed to create Stock Request lines." }, 500);
                        }
                    }

                    scope.Complete();
                }

                return ApiResponse.Success<object>(new { POS_StockRequestID = newId, RefNumber = refNumber });
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Stock Request Add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Stock_Request(Req_StockRequest_Update request)
        {
            try
            {
                _logger.LogService("Starting Stock Request Update", request);

                var connectionString = GetConnectionString();

                var existing = await POS_StockRequests_Select_Single(new StockRequest()
                {
                    StockRequestID = request.POS_StockRequestID
                }, connectionString);

                if (existing == null)
                {
                    _logger.LogService("Stock Request not found", request.POS_StockRequestID);
                    return ApiResponse.Fail<object>(AppErrorCode.StockRequestNotFound, new List<string> { "Stock Request not found." }, 400);
                }

                if (existing.FK_OrderStatusID != (int)StockRequestStatus.Draft)
                {
                    _logger.LogService("Stock Request not in Draft", existing.FK_OrderStatusID);
                    return ApiResponse.Fail<object>(AppErrorCode.StockRequestNotInDraft, new List<string> { "Only Draft requests can be edited." }, 400);
                }

                if (request.Lines == null || request.Lines.Count == 0)
                    return ApiResponse.Fail<object>(AppErrorCode.StockRequestEmpty, new List<string> { "At least one line is required." }, 400);

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();

                    await POS_StockRequests_Update(new StockRequest()
                    {
                        StockRequestID      = existing.StockRequestID,
                        RefNumber           = request.RefNumber       ?? existing.RefNumber,
                        FK_FromDebtorID     = request.FK_FromDebtorID ?? existing.FK_FromDebtorID,
                        FK_ToDebtorID       = request.FK_ToDebtorID   ?? existing.FK_ToDebtorID,
                        FK_OrderStatusID    = existing.FK_OrderStatusID,
                        FK_UserID           = existing.FK_UserID,
                        ManagerNotes        = existing.ManagerNotes,
                        Notes               = request.Notes ?? existing.Notes,
                        DateOrdered         = existing.DateOrdered,
                        DateUpdated         = DateTime.Now,
                        FK_ApprovedByUserID = existing.FK_ApprovedByUserID,
                        DateApproved        = existing.DateApproved
                    }, sqlConn);

                    await StockRequestLines_Delete_By_Stock_Request(existing.StockRequestID.Value, sqlConn);

                    foreach (var line in request.Lines)
                    {
                        var product = await POS_Api.Services.Inventory.Inventory_Base_Service.POS_Products_Select_Single(
                            new POS_Common.Models.Inventory.POS_Products.Product { ProductID = line.FK_ProductID },
                            sqlConn);

                        if (product == null)
                        {
                            _logger.LogService("Product not found for Stock Request line during update", line.FK_ProductID);
                            return ApiResponse.Fail<object>(AppErrorCode.ProductNotFound, new List<string> { $"Product {line.FK_ProductID} not found." }, 400);
                        }

                        var insertedLine = await POS_StockRequestLines_Insert(new StockRequestLine()
                        {
                            FK_StockRequestID = existing.StockRequestID.Value,
                            FK_ProductID      = line.FK_ProductID,
                            FK_UnitID         = product.FK_UnitID,
                            Quantity          = line.Quantity,
                            Notes             = line.Notes,
                            IsDeclined        = false
                        }, sqlConn);

                        if (insertedLine?.StockRequestLineID == null)
                        {
                            _logger.LogService("Failed to insert Stock Request line during update", line);
                            return ApiResponse.Fail<object>(AppErrorCode.DatabaseError, new List<string> { "Failed to update Stock Request lines." }, 500);
                        }
                    }

                    scope.Complete();
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Stock Request Update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Submit_Stock_Request(Req_StockRequest_Submit request)
        {
            try
            {
                _logger.LogService("Starting Stock Request Submit", request);

                var connectionString = GetConnectionString();

                var existing = await POS_StockRequests_Select_Single(new StockRequest()
                {
                    StockRequestID = request.POS_StockRequestID
                }, connectionString);

                if (existing == null)
                    return ApiResponse.Fail<object>(AppErrorCode.StockRequestNotFound, new List<string> { "Stock Request not found." }, 400);

                if (existing.FK_OrderStatusID != (int)StockRequestStatus.Draft)
                    return ApiResponse.Fail<object>(AppErrorCode.StockRequestNotInDraft, new List<string> { "Only Draft requests can be submitted." }, 400);

                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    existing.FK_OrderStatusID = (int)StockRequestStatus.Pending;
                    existing.DateUpdated      = DateTime.Now;
                    await POS_StockRequests_Update(existing, sqlConn);
                }

                await Send_Stock_Request_Approval_Email_Async(existing.StockRequestID.Value, connectionString);

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Stock Request Submit", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Approve_Stock_Request(Req_StockRequest_Approve request)
        {
            try
            {
                _logger.LogService("Starting Stock Request Approve", request);

                var connectionString = GetConnectionString();

                var existing = await POS_StockRequests_Select_Single(new StockRequest()
                {
                    StockRequestID = request.POS_StockRequestID
                }, connectionString);

                if (existing == null)
                    return ApiResponse.Fail<object>(AppErrorCode.StockRequestNotFound, new List<string> { "Stock Request not found." }, 400);

                if (existing.FK_OrderStatusID != (int)StockRequestStatus.Pending)
                    return ApiResponse.Fail<object>(AppErrorCode.StockRequestAlreadyDecided, new List<string> { "Stock Request is not awaiting approval." }, 400);

                var lines = await StockRequestLines_Select_All_StockRequestLines(new StockRequestLine()
                {
                    FK_StockRequestID = existing.StockRequestID
                }, connectionString) ?? new List<StockRequestLine>();

                var lineById = lines.ToDictionary(l => l.StockRequestLineID ?? 0, l => l);

                foreach (var d in request.LineDecisions)
                {
                    if (d.POS_StockRequestLineID == null || !lineById.ContainsKey(d.POS_StockRequestLineID.Value))
                        return ApiResponse.Fail<object>(AppErrorCode.StockRequestLineMismatch, new List<string> { $"Line {d.POS_StockRequestLineID} does not belong to this request." }, 400);
                }

                int approvedCount = 0;
                int declinedCount = 0;
                int partialCount  = 0;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();

                    foreach (var d in request.LineDecisions)
                    {
                        var line = lineById[d.POS_StockRequestLineID.Value];

                        bool isDeclined = d.IsDeclined == true || (d.ApprovedQuantity ?? 0m) <= 0m;
                        decimal approvedQty = isDeclined ? 0m : Math.Min(d.ApprovedQuantity ?? line.Quantity ?? 0m, line.Quantity ?? 0m);

                        if (isDeclined)                        declinedCount++;
                        else if (approvedQty < (line.Quantity ?? 0m)) partialCount++;
                        else                                   approvedCount++;

                        await POS_StockRequestLines_Update(new StockRequestLine()
                        {
                            StockRequestLineID = line.StockRequestLineID,
                            FK_StockRequestID  = line.FK_StockRequestID,
                            FK_ProductID       = line.FK_ProductID,
                            Quantity           = line.Quantity,
                            Notes              = line.Notes,
                            ManagerNotes       = string.IsNullOrWhiteSpace(d.ManagerNotes) ? line.ManagerNotes : d.ManagerNotes,
                            IsDeclined         = isDeclined,
                            ApprovedQuantity   = approvedQty
                        }, sqlConn);
                    }

                    int overallStatus =
                        declinedCount == request.LineDecisions.Count                ? (int)StockRequestStatus.Cancelled :
                        partialCount > 0 || declinedCount > 0                       ? (int)StockRequestStatus.PartiallyApproved :
                                                                                      (int)StockRequestStatus.Approved;

                    existing.FK_OrderStatusID    = overallStatus;
                    existing.FK_ApprovedByUserID = _userContext.UserID;
                    existing.DateApproved        = DateTime.Now;
                    existing.DateUpdated         = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(request.ManagerNotes))
                        existing.ManagerNotes = request.ManagerNotes;

                    await POS_StockRequests_Update(existing, sqlConn);

                    scope.Complete();
                }

                await Send_Stock_Request_Decided_Email_Async(existing.StockRequestID.Value, connectionString);

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Stock Request Approve", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Stock Request Lines

        public async Task<ApiResponse<List<Res_StockRequestLine_List>>> List_Stock_Request_Lines(Req_StockRequestLine_List request)
        {
            try
            {
                _logger.LogService("Starting Stock Request Lines List", request);

                var lines = await StockRequestLines_Select_All_StockRequestLines(new StockRequestLine()
                {
                    FK_StockRequestID = request.StockRequestID
                }, GetConnectionString());

                var response = new List<Res_StockRequestLine_List>();

                if (lines != null && lines.Any())
                {
                    foreach (var l in lines)
                    {
                        response.Add(new Res_StockRequestLine_List()
                        {
                            POS_StockRequestLineID = l.StockRequestLineID,
                            FK_StockRequestID      = l.FK_StockRequestID,
                            FK_ProductID           = l.FK_ProductID,
                            ProductName            = l.ProductName,
                            FK_UnitID              = l.FK_UnitID,
                            Unit                   = l.Unit,
                            Symbol                 = l.Symbol,
                            Quantity               = l.Quantity,
                            Notes                  = l.Notes,
                            ManagerNotes           = l.ManagerNotes,
                            IsDeclined             = l.IsDeclined,
                            ApprovedQuantity       = l.ApprovedQuantity
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Stock Request Lines list", ex);
                return ApiResponse.Fail<List<Res_StockRequestLine_List>>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Stock Request - email helpers

        private string GetConnectionString() =>
            _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()));

        private async Task<StockRequest> Load_Stock_Request_With_Names(int stockRequestID, string connectionString)
        {
            var bare = await POS_StockRequests_Select_Single(new StockRequest() { StockRequestID = stockRequestID }, connectionString);
            if (bare?.FK_ToDebtorID == null) return null;

            var enriched = await StockRequests_Select_All_StockRequests(new StockRequest()
            {
                FK_ToDebtorID = bare.FK_ToDebtorID
            }, connectionString);

            return enriched?.FirstOrDefault(x => x.StockRequestID == stockRequestID);
        }

        private async Task Send_Stock_Request_Approval_Email_Async(int stockRequestID, string connectionString)
        {
            try
            {
                var sr = await Load_Stock_Request_With_Names(stockRequestID, connectionString);
                if (sr == null) return;

                var approvers = await POS_StockRequestReviewers_Select_By_Role("Approver", connectionString)
                                ?? new List<StockRequestReviewer>();

                var recipients = approvers
                    .Select(a => a.Email)
                    .Where(e => !string.IsNullOrWhiteSpace(e))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (recipients.Count == 0)
                {
                    _logger.LogService("No approvers configured; approval email skipped");
                    return;
                }

                await _emailService.Send_Stock_Request_Approval_Email(new StockRequestApprovalEmail
                {
                    StockRequestID = sr.StockRequestID ?? 0,
                    RefNumber      = sr.RefNumber ?? string.Empty,
                    To             = recipients
                });
            }
            catch (Exception ex)
            {
                _logger.LogService("Approval email send failed (non-fatal)", ex);
            }
        }

        private async Task Send_Stock_Request_Decided_Email_Async(int stockRequestID, string connectionString)
        {
            try
            {
                var sr = await Load_Stock_Request_With_Names(stockRequestID, connectionString);
                if (sr == null) return;

                var lines = await StockRequestLines_Select_All_StockRequestLines(new StockRequestLine() { FK_StockRequestID = stockRequestID }, connectionString)
                            ?? new List<StockRequestLine>();

                var outcome = sr.FK_OrderStatusID switch
                {
                    (int)StockRequestStatus.Approved          => "Approved",
                    (int)StockRequestStatus.PartiallyApproved => "PartiallyApproved",
                    (int)StockRequestStatus.Cancelled         => "Declined",
                    _                                         => sr.OrderStatus ?? string.Empty
                };

                var buyers = await POS_StockRequestReviewers_Select_By_Debtor_Role(sr.FK_ToDebtorID ?? 0, "Buyer", connectionString)
                             ?? new List<StockRequestReviewer>();

                if (buyers.Count == 0)
                {
                    _logger.LogService($"No buyers configured for ToDebtorID={sr.FK_ToDebtorID}; buyer email skipped");
                    return;
                }

                if (outcome == "Declined")
                {
                    _logger.LogService($"Stock Request {sr.RefNumber} declined; buyer email suppressed");
                    return;
                }

                var decidedBy = LTRIM_RTRIM($"{_userContext.Firstname} {_userContext.Lastname}");

                await _emailService.Send_Stock_Request_Decided_Email(new StockRequestDecidedEmail
                {
                    StockRequestID = sr.StockRequestID ?? 0,
                    RefNumber      = sr.RefNumber ?? string.Empty,
                    FromDebtorName = sr.FromDebtorName ?? string.Empty,
                    ToDebtorName   = sr.ToDebtorName ?? string.Empty,
                    DecidedBy      = decidedBy,
                    ManagerNotes   = sr.ManagerNotes ?? string.Empty,
                    Outcome        = outcome,
                    To             = buyers.Select(b => b.Email).Where(e => !string.IsNullOrWhiteSpace(e)).Distinct().ToList(),
                    Lines          = lines.Where(l => l.IsDeclined != true).Select(l => new StockRequestEmailLine
                    {
                        ProductName      = l.ProductName ?? string.Empty,
                        Quantity         = l.Quantity,
                        ApprovedQuantity = l.ApprovedQuantity,
                        IsDeclined       = l.IsDeclined,
                        Notes            = l.Notes ?? string.Empty,
                        ManagerNotes     = l.ManagerNotes ?? string.Empty
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogService("Decided email send failed (non-fatal)", ex);
            }
        }

        private static string LTRIM_RTRIM(string s) => string.IsNullOrWhiteSpace(s) ? string.Empty : s.Trim();
        #endregion

        //#region Stock Transfers

        //public async Task<ApiResponse<List<Res_StockTransfer_List>>> List_Stock_Transfers(Req_StockTransfer_List request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Stock Transfer List");

        //        var stockTransferResponse = await StockTransfers_Select_All_StockTransfers(new POS_StockTransfer()
        //        {
        //            FK_ToDebtorID = request.DebtorID,
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


        //        var response = new List<Res_StockTransfer_List>();

        //        if (stockTransferResponse != null && stockTransferResponse.Any())
        //        {
        //            foreach (var purchaseOrder in stockTransferResponse)
        //            {

        //                response.Add(new Res_StockTransfer_List()
        //                {

        //                });
        //            }
        //        }

        //        return ApiResponse.Success(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during address list", ex);
        //        return ApiResponse.Fail<List<Res_StockTransfer_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}

        //public async Task<ApiResponse<object>> Add_Stock_Transfer(Req_StockTransfer_Add request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Purchase Order Add", request);

        //        var purchaseOrderResponse = await PurchaseOrder_Select_Single_Number(new POS_PurchaseOrder()
        //        {
        //            //OrderNumber = request.OrderNumber
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

        //        if (purchaseOrderResponse != null)
        //        {
        //            //_logger.LogService("Purchase Order already exists", request.OrderNumber);
        //            return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderExists, new List<string> { "Purchase Order already exists." }, 400);
        //        }

        //        using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
        //        {
        //            await sqlConn.OpenAsync();

        //            //int statusId = request.IsSubmitted == true ? 1 : 5;

        //            var purchaseOrderInsert = await POS_PurchaseOrders_Insert(new POS_PurchaseOrder()
        //            {

        //            }, sqlConn);
        //        }

        //        return ApiResponse.Success(new object());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during Purchase Order add", ex);
        //        return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}

        //public async Task<ApiResponse<object>> Update_Stock_Transfer(Req_StockTransfer_Update request)
        //{
        //    try
        //    {
        //        _logger.LogService("Starting Purchase Order Update", request);

        //        var purchaseOrderResponse = await POS_PurchaseOrders_Select_Single(new POS_PurchaseOrder()
        //        {
        //            //POS_PurchaseOrderID = request.POS_PurchaseOrderID
        //        }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

        //        if (purchaseOrderResponse == null)
        //        {
        //            //_logger.LogService("Purchase Order not found", request.POS_PurchaseOrderID);
        //            return ApiResponse.Fail<object>(AppErrorCode.PurchaseOrderNotFound, new List<string> { "Purchase Order not found." }, 400);
        //        }

        //        using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
        //        {
        //            await sqlConn.OpenAsync();

        //            //int statusId = request.IsSubmitted == true ? 1 : 5;

        //            var purchaseOrderInsert = await POS_PurchaseOrders_Update(new POS_PurchaseOrder()
        //            {

        //            }, sqlConn);
        //        }

        //        return ApiResponse.Success(new object());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogService("Exception during Purchase Order add", ex);
        //        return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
        //        }, 500);
        //    }
        //}
        //#endregion
    }
}


