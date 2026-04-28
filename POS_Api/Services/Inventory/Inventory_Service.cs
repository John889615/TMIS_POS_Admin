using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using POS_Api.Helpers;
using POS_Api.ServiceInterfaces.Cache;
using POS_Api.ServiceInterfaces.Inventory;
using POS_Api.ServiceInterfaces.Logging;
using POS_Api.Services.Inventory;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.Models.Creditors.Creditors;
using POS_Common.Models.Creditors.CreditorTypeMappings;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.Inventory.POS_ProductCategories;
using POS_Common.Models.Inventory.POS_ProductCombinations;
using POS_Common.Models.Inventory.POS_ProductExtraCategories;
using POS_Common.Models.Inventory.POS_ProductExtras;
using POS_Common.Models.Inventory.POS_ProductPreparation;
using POS_Common.Models.Inventory.POS_ProductPreparationMethods;
using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Inventory.POS_ProductSubstitutions;
using POS_Common.Models.Inventory.POS_ProductTypes;
using POS_Common.Models.Inventory.POS_ServedAs;
using POS_Common.Models.Inventory.POS_ServedAsProducts;
using POS_Common.Models.Inventory.POS_Units;
using POS_Common.ModelsDto.CreditorsController.Creditor;
using POS_Common.ModelsDto.InventoryController.Product;
using POS_Common.ModelsDto.InventoryController.ProductCategory;
using POS_Common.ModelsDto.InventoryController.ProductCombination;
using POS_Common.ModelsDto.InventoryController.ProductExtra;
using POS_Common.ModelsDto.InventoryController.ProductExtraCategories;
using POS_Common.ModelsDto.InventoryController.ProductPreparation;
using POS_Common.ModelsDto.InventoryController.ProductPreparationMethod;
using POS_Common.ModelsDto.InventoryController.ProductSubstitution;
using POS_Common.ModelsDto.InventoryController.ProductType;
using POS_Common.ModelsDto.InventoryController.ServedAs;
using POS_Common.ModelsDto.InventoryController.ServedAsProducts;
using POS_Common.ModelsDto.InventoryController.Unit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Interfaces;

namespace POS_Api.Services
{
    public class Inventory_Service : Inventory_Custom_Service, IInventory_Service
    {
        #region Members

        private readonly ILogging_Service _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        private readonly ICache_Service _cacheService;
        private readonly ImageHelper _imageHelper;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Inventory_Service(IConfiguration configuration, ILogging_Service logger
            , IHttpContextAccessor httpContextAccessor, IUserContext userContext, ICache_Service cacheService, ImageHelper imageHelper)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
            _cacheService = cacheService;

            Current_User_Management();
            _imageHelper = imageHelper;
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

        #region Products

        public async Task<ApiResponse<List<Res_Product_List>>> List_Products()
        {
            try
            {
                var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                    .Where(x => x.Environment == _configuration["Environment"]).ToList();

                _logger.LogService("Starting Product List");

                var productResponse = await Products_Select_All_Products(new Product()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Product_List>();

                if (productResponse != null && productResponse.Any())
                {
                    foreach (var product in productResponse)
                    {
                        response.Add(new Res_Product_List()
                        {
                            POS_ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            Description = product.Description,
                            FK_ProductTypeID = product.FK_ProductTypeID,
                            ProductType = product.ProductType,
                            IsInventory = product.IsInventory,
                            IsManufactured = product.IsManufactured,
                            IsService = product.IsService,
                            IsComposite = product.IsComposite,
                            IsStockTracked = product.IsStockTracked,
                            FK_UnitID = product.FK_UnitID,
                            Unit = product.Unit,
                            Symbol = product.Symbol,
                            FK_ProductCategoryID = product.FK_ProductCategoryID,
                            CategoryName = product.ProductCategory,
                            FK_DefaultUnitID = product.FK_DefaultUnitID,
                            DefaultUnit = product.DefaultUnit,
                            DefaultSymbol = product.DefaultSymbol,
                            SKU = product.SKU,
                            Barcode = product.Barcode,
                            QrCode = product.QrCode,
                            ImageUrl = string.IsNullOrWhiteSpace(product.ImageUrl)
                                       ? null
                                       : product.ImageUrl
                                           .Replace("{Path}", globalSettings.FirstOrDefault(x => x.Key == "Image_Admin_Server_Url")?.Value ?? "")
                                           .Replace("{TenantID}", _userContext.TenantID.ToString())
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_Product_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product(Req_Product_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Add", request);

                var productResponse = await Products_Select_Single_Name(new Product()
                {
                    ProductName = request.ProductName
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse != null)
                {
                    _logger.LogService("Product already exists", request.ProductName);
                    return ApiResponse.Fail<object>(AppErrorCode.ProductExists, new List<string> { "Product already exists." }, 400);
                }

                var productInsert = await POS_Products_Insert(new Product()
                {
                    ProductName = request.ProductName,
                    Description = request.Description,
                    FK_ProductTypeID = request.FK_ProductTypeID,
                    IsStockTracked = request.IsStockTracked,
                    FK_UnitID = request.FK_UnitID,
                    FK_ProductCategoryID = request.FK_ProductCategoryID,
                    FK_DefaultUnitID = request.FK_DefaultUnitID,
                    SKU = request.SKU,
                    Barcode = request.Barcode,
                    QrCode = request.QrCode,
                    IsActive = true,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                    .Where(x => x.Environment == _configuration["Environment"]).ToList();

                if (request.ImageFile != null)
                {
                    var relativePath = "products";

                    var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                    if (imageUrl == null)
                    {
                        return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                    }

                    string rootPath = _configuration["ImageStorage:RootFileSystemPath"];

                    await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                    {
                        FK_ImageCategoryID = 3,  // e.g. 1 = Menu
                        FK_ItemID = productInsert.ProductID,
                        FileSystemPath = rootPath,
                        RelativePath = relativePath,
                        ImageName = Path.GetFileName(imageUrl.BaseUrl),
                        FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                        ImageUrl = imageUrl.BaseUrl,
                        LocalUrl = imageUrl.LocalUrl,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Product(Req_Product_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Update", request);

                var productResponse = await POS_Products_Select_Single(new Product()
                {
                    ProductID = request.POS_ProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    _logger.LogService("Product not found", request.POS_ProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.ProductNotFound, new List<string> { "Product not found." }, 404);
                }

                var productUpdate = await POS_Products_Update(new Product()
                {
                    ProductID = request.POS_ProductID,
                    ProductName = request.ProductName ?? productResponse.ProductName,
                    Description = request.Description ?? productResponse.Description,
                    FK_ProductTypeID = request.FK_ProductTypeID ?? productResponse.FK_ProductTypeID,
                    IsStockTracked = request.IsStockTracked ?? productResponse.IsStockTracked,
                    FK_UnitID = request.FK_UnitID ?? productResponse.FK_UnitID,
                    FK_ProductCategoryID = request.FK_ProductCategoryID ?? productResponse.FK_ProductCategoryID,
                    FK_DefaultUnitID = request.FK_DefaultUnitID ?? productResponse.FK_DefaultUnitID,
                    SKU = request.SKU ?? productResponse.SKU,
                    Barcode = request.Barcode ?? productResponse.Barcode,
                    QrCode = request.QrCode ?? productResponse.QrCode,
                    IsActive = true,
                    DateAdded = productResponse.DateAdded,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                    .Where(x => x.Environment == _configuration["Environment"]).ToList();

                if (request.ImageFile != null)
                {
                    var relativePath = "products";

                    var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                    if (imageUrl == null)
                    {
                        return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                    }

                    string rootPath = _configuration["ImageStorage:RootFileSystemPath"];
                    

                    await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                    {
                        FK_ImageCategoryID = 3,  // e.g. 1 = Menu
                        FK_ItemID = request.POS_ProductID,
                        FileSystemPath = rootPath,
                        RelativePath = relativePath,
                        ImageName = Path.GetFileName(imageUrl.BaseUrl),
                        FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                        ImageUrl = imageUrl.BaseUrl,
                        LocalUrl = imageUrl.LocalUrl,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion

        #region Product Combinations

        public async Task<ApiResponse<List<Res_ProductCombination_List>>> List_Product_Combinations(Req_ProductCombination_List request)
        {
            try
            {
                _logger.LogService("Starting Product List");

                var productResponse = await ProductCombinations_Select_All_ProductID(new ProductCombination()
                {
                    FK_ProductID = request.FK_ProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    return ApiResponse.Fail<List<Res_ProductCombination_List>>("No Combination Found.");
                }

                var products = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Products;

                var response = new List<Res_ProductCombination_List>();

                if (productResponse != null && productResponse.Any())
                {
                    foreach (var product in productResponse)
                    {

                        response.Add(new Res_ProductCombination_List()
                        {
                            ProductCombinationID = product.ProductCombinationID,
                            FK_ProductItemID = product.FK_ProductItemID,

                            ProductItemName = product.FK_ProductItemID != null
                               ? products.FirstOrDefault(x => x.ProductID == product.FK_ProductItemID).ProductName
                               : null,

                            IsQuantified = product.IsQuantified,
                            Quantity = product.Quantity,
                            IsOptional = product.IsOptional,
                            IsExtraCharge = product.IsExtraCharge,
                            DisplayOrder = product.DisplayOrder
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_ProductCombination_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product_Combination(Req_ProductCombination_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Add", request);

                var productResponse = await Product_Combinations_Select_Single_ID(new ProductCombination()
                {
                    FK_ProductID = request.FK_ProductID,
                    FK_ProductItemID = request.FK_ProductItemID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse != null)
                {
                    _logger.LogService("Combination already exists", request.FK_ProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.CombinationExists, new List<string> { "Combination already exists." }, 400);
                }

                var productInsert = await POS_ProductCombinations_Insert(new ProductCombination()
                {
                    FK_ProductID = request.FK_ProductID,
                    FK_ProductItemID = request.FK_ProductItemID,
                    IsQuantified = request.IsQuantified,
                    Quantity = request.Quantity,
                    IsOptional = request.IsOptional,
                    IsExtraCharge = request.IsExtraCharge,
                    DisplayOrder = request.DisplayOrder,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = null,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productInsert == null)
                {
                    _logger.LogService("Product Combination insert failed", request);
                    return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { "Product Combination insert failed." }, 400);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Product_Combination(Req_ProductCombination_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Update", request);

                var productResponse = await POS_ProductCombinations_Update(new ProductCombination()
                {
                    ProductCombinationID = request.ProductCombinationID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    _logger.LogService("Product Combination not found", request.ProductCombinationID);
                    return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { "Product Combination not found." }, 404);
                }

                var productUpdate = await POS_ProductCombinations_Update(new ProductCombination()
                {
                    ProductCombinationID = request.ProductCombinationID,
                    FK_ProductID = request.FK_ProductID ?? productResponse.FK_ProductID,
                    FK_ProductItemID = request.FK_ProductItemID ?? productResponse.FK_ProductItemID,
                    IsQuantified = request.IsQuantified ?? productResponse.IsQuantified,
                    Quantity = request.Quantity ?? productResponse.Quantity,
                    IsOptional = request.IsOptional ?? productResponse.IsOptional,
                    IsExtraCharge = request.IsExtraCharge ?? productResponse.IsExtraCharge,
                    DisplayOrder = request.DisplayOrder ?? productResponse.DisplayOrder,
                    FK_UpdatedUserID = _userContext.UserID,
                    FK_CreatedUserID = productResponse.FK_CreatedUserID,
                    DateCreated = productResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Product_Combination(Req_ProductCombination_Delete request)
        {
            try
            {
                _logger.LogService("Starting Creditor Delete", request);

                var productResponse = await POS_ProductCombinations_Delete(new ProductCombination()
                {
                    ProductCombinationID = request.ProductCombinationID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Product Extra Categories

        public async Task<ApiResponse<List<Res_ProductExtraCategory_List>>> List_Product_Extra_Categories()
        {
            try
            {
                _logger.LogService("Starting Product List");

                var productResponse = await POS_ProductExtraCategories_Select_All(new ProductExtraCategory()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductExtraCategory_List>();

                if (productResponse != null && productResponse.Any())
                {
                    foreach (var product in productResponse)
                    {

                        response.Add(new Res_ProductExtraCategory_List()
                        {
                            ProductExtraCategoryID = product.ProductExtraCategoryID,
                            Category = product.Category,
                            DisplayOrder = product.DisplayOrder
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_ProductExtraCategory_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product_Extra_Category(Req_ProductExtraCategory_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Add", request);

                var productInsert = await POS_ProductExtraCategories_Insert(new ProductExtraCategory()
                {
                    Category = request.Category,
                    DisplayOrder = request.DisplayOrder,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = null,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Product_Extra_Category(Req_ProductExtraCategory_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Update", request);

                var productResponse = await POS_ProductExtraCategories_Select_Single(new ProductExtraCategory()
                {
                    ProductExtraCategoryID = request.ProductExtraCategoryID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    _logger.LogService("Product not found", request.ProductExtraCategoryID);
                    return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { "Product Extra Category not found." }, 404);
                }

                var productUpdate = await POS_ProductExtraCategories_Update(new ProductExtraCategory()
                {
                    ProductExtraCategoryID = request.ProductExtraCategoryID,
                    Category = request.Category ?? productResponse.Category,
                    DisplayOrder = request.DisplayOrder ?? productResponse.DisplayOrder,
                    FK_UpdatedUserID = _userContext.UserID,
                    FK_CreatedUserID = productResponse.FK_CreatedUserID,
                    DateCreated = productResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Product Extras

        public async Task<ApiResponse<List<Res_ProductExtra_List>>> List_Product_Extras(Req_ProductExtra_List request)
        {
            try
            {
                _logger.LogService("Starting Product List");

                var productResponse = await Product_Extras_Select_All_ProductID(new ProductExtra()
                {
                    FK_ProductID = request.FK_ProductID,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var products = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Products;
                var categories = _cacheService.GetCacheAsync(_userContext.TenantID).Result.ProductCategories;

                var response = new List<Res_ProductExtra_List>();

                if (productResponse != null && productResponse.Any())
                {
                    foreach (var product in productResponse)
                    {
                        response.Add(new Res_ProductExtra_List()
                        {
                            ProductExtraID = product.ProductExtraID,
                            FK_ProductID = product.FK_ProductID,
                            ProductName = product.FK_ProductID != null
                               ? products.FirstOrDefault(x => x.ProductID == product.FK_ProductID).ProductName
                               : null,
                            FK_ProductExtraCategoryID = product.FK_ProductExtraCategoryID,
                            CategoryName = product.FK_ProductExtraCategoryID != null
                               ? categories.FirstOrDefault(x => x.ProductExtraCategoryID == product.FK_ProductExtraCategoryID).Category
                               : null,
                            FK_ProductExtraID = product.FK_ProductExtraID,
                            ExtraName = product.FK_ProductExtraID != null
                               ? products.FirstOrDefault(x => x.ProductID == product.FK_ProductExtraID).ProductName
                               : null,
                            IsQuantified = product.IsQuantified,
                            Quantity = product.Quantity,
                            IsExtraCharge = product.IsExtraCharge,
                            DisplayOrder = product.DisplayOrder
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_ProductExtra_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product_Extra(Req_ProductExtra_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Add", request);

                var productInsert = await POS_ProductExtras_Insert(new ProductExtra()
                {
                    FK_ProductID = request.FK_ProductID,
                    FK_ProductExtraCategoryID = request.FK_ProductExtraCategoryID,
                    FK_ProductExtraID = request.FK_ProductExtraID,
                    IsQuantified = request.IsQuantified,
                    Quantity = request.Quantity,
                    IsExtraCharge = request.IsExtraCharge,
                    DisplayOrder = request.DisplayOrder,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = null,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Product_Extra(Req_ProductExtra_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Update", request);

                var productResponse = await POS_ProductExtras_Select_Single(new ProductExtra()
                {
                    ProductExtraID = request.ProductExtraID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    _logger.LogService("Product not found", request.ProductExtraID);
                    return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { "Product Extra not found." }, 404);
                }

                var productUpdate = await POS_ProductExtras_Update(new ProductExtra()
                {
                    ProductExtraID = request.ProductExtraID,
                    FK_ProductID = request.FK_ProductID ?? productResponse.FK_ProductID,
                    FK_ProductExtraCategoryID = request.FK_ProductExtraCategoryID ?? productResponse.FK_ProductExtraCategoryID,
                    FK_ProductExtraID = request.FK_ProductExtraID ?? productResponse.FK_ProductExtraID,
                    IsQuantified = request.IsQuantified ?? productResponse.IsQuantified,
                    Quantity = request.Quantity ?? productResponse.Quantity,
                    IsExtraCharge = request.IsExtraCharge ?? productResponse.IsExtraCharge,
                    DisplayOrder = request.DisplayOrder ?? productResponse.DisplayOrder,
                    FK_UpdatedUserID = _userContext.UserID,
                    FK_CreatedUserID = productResponse.FK_CreatedUserID,
                    DateCreated = productResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Product_Extra(Req_ProductExtra_Delete request)
        {
            try
            {
                _logger.LogService("Starting Creditor Delete", request);

                var productResponse = await POS_ProductExtras_Delete(new ProductExtra()
                {
                    ProductExtraID = request.ProductExtraID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Product Preparation

        public async Task<ApiResponse<List<Res_ProductPreparation_List>>> List_Product_Preparation(Req_ProductPreparation_List request)
        {
            try
            {
                _logger.LogService("Starting Product List");

                var productResponse = await ProductPreparation_Select_All_ProductID(new ProductPreparation()
                {
                    FK_ProductID = request.FK_ProductID,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var products = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Products;
                var methods = _cacheService.GetCacheAsync(_userContext.TenantID).Result.ProductPreparationMethods;


                var response = new List<Res_ProductPreparation_List>();

                if (productResponse != null && productResponse.Any())
                {
                    foreach (var product in productResponse)
                    {

                        response.Add(new Res_ProductPreparation_List()
                        {
                            ProductPreparationID = product.ProductPreparationID,
                            FK_ProductID = product.FK_ProductID,
                            ProductName = product.FK_ProductID != null
                               ? products.FirstOrDefault(x => x.ProductID == product.FK_ProductID).ProductName
                               : null,
                            FK_ProductPreparationMethodID = product.FK_ProductPreparationMethodID,
                            PreparationMethod = product.FK_ProductPreparationMethodID != null
                               ? methods.FirstOrDefault(x => x.ProductPreparationMethodID == product.FK_ProductPreparationMethodID).Method
                               : null,
                            MethodShortCode = product.FK_ProductPreparationMethodID != null
                               ? methods.FirstOrDefault(x => x.ProductPreparationMethodID == product.FK_ProductPreparationMethodID).ShortCode
                               : null
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_ProductPreparation_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product_Preparation(Req_ProductPreparation_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Add", request);

                var productInsert = await POS_ProductPreparation_Insert(new ProductPreparation()
                {
                    FK_ProductID = request.FK_ProductID,
                    FK_ProductPreparationMethodID = request.FK_ProductPreparationMethodID,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = null,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Product_Preparation(Req_ProductPreparation_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Update", request);

                var productResponse = await POS_ProductPreparation_Select_Single(new ProductPreparation()
                {
                    ProductPreparationID = request.ProductPreparationID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    _logger.LogService("Product not found", request.ProductPreparationID);
                    return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { "Product Preparation not found." }, 404);
                }

                var productUpdate = await POS_ProductPreparation_Update(new ProductPreparation()
                {
                    ProductPreparationID = request.ProductPreparationID,
                    FK_ProductID = request.FK_ProductID ?? productResponse.FK_ProductID,
                    FK_ProductPreparationMethodID = request.FK_ProductPreparationMethodID ?? productResponse.FK_ProductPreparationMethodID,
                    FK_UpdatedUserID = _userContext.UserID,
                    FK_CreatedUserID = productResponse.FK_CreatedUserID,
                    DateCreated = productResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Product_Preparation(Req_ProductPreparation_Delete request)
        {
            try
            {
                _logger.LogService("Starting Creditor Delete", request);

                var productResponse = await POS_ProductPreparations_Delete(new ProductPreparation()
                {
                    ProductPreparationID = request.ProductPreparationID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Product Preparation Methods

        public async Task<ApiResponse<List<Res_ProductPreparationMethod_List>>> List_Product_Preparation_Methods()
        {
            try
            {
                _logger.LogService("Starting Product List");

                var productResponse = await POS_ProductPreparationMethods_Select_All(new ProductPreparationMethod()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductPreparationMethod_List>();

                if (productResponse != null && productResponse.Any())
                {
                    foreach (var product in productResponse)
                    {

                        response.Add(new Res_ProductPreparationMethod_List()
                        {
                            ProductPreparationMethodID = product.ProductPreparationMethodID,
                            Method = product.Method,
                            ShortCode = product.ShortCode
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_ProductPreparationMethod_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product_Preparation_Method(Req_ProductPreparationMethod_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Add", request);

                var productInsert = await POS_ProductPreparationMethods_Insert(new ProductPreparationMethod()
                {
                    Method = request.Method,
                    ShortCode = request.ShortCode,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = null,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Product_Preparation_Method(Req_ProductPreparationMethod_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Update", request);

                var productResponse = await POS_ProductPreparationMethods_Select_Single(new ProductPreparationMethod()
                {
                    ProductPreparationMethodID = request.ProductPreparationMethodID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    _logger.LogService("Product not found", request.ProductPreparationMethodID);
                    return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { "Product Preparation Method not found." }, 404);
                }

                var productUpdate = await POS_ProductPreparationMethods_Update(new ProductPreparationMethod()
                {
                    ProductPreparationMethodID = request.ProductPreparationMethodID,
                    Method = request.Method ?? productResponse.Method,
                    ShortCode = request.ShortCode ?? productResponse.ShortCode,
                    FK_UpdatedUserID = _userContext.UserID,
                    FK_CreatedUserID = productResponse.FK_CreatedUserID,
                    DateCreated = productResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion

        #region Product Substitutions

        public async Task<ApiResponse<List<Res_ProductSubstitution_List>>> List_Product_Substitutions(Req_Substitution_List request)
        {
            try
            {
                _logger.LogService("Starting Product List");

                var productSubstitutionResponse = await ProductSubstitutions_Select_All_ProductID(new ProductSubstitution()
                {
                    FK_ProductID = request.FK_ProductID,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var products = _cacheService.GetCacheAsync(_userContext.TenantID).Result.Products;

                var response = new List<Res_ProductSubstitution_List>();

                if (productSubstitutionResponse != null && productSubstitutionResponse.Any())
                {
                    foreach (var substitute in productSubstitutionResponse)
                    {

                        response.Add(new Res_ProductSubstitution_List()
                        {
                            ProductSubstitutionID = substitute.ProductSubstitutionID,
                            FK_ProductID = substitute.FK_ProductID,
                            ProductName = substitute.FK_ProductID != null
                               ? products.FirstOrDefault(x => x.ProductID == substitute.FK_ProductID).ProductName
                               : null,
                            FK_ProductSubstitutionID = substitute.FK_ProductSubstitutionID,
                            ProductSubstitute = substitute.FK_ProductSubstitutionID != null
                               ? products.FirstOrDefault(x => x.ProductID == substitute.FK_ProductSubstitutionID).ProductName
                               : null,
                            IsQuantified = substitute.IsQuantified,
                            Quantity = substitute.Quantity,
                            IsExtraCharge = substitute.IsExtraCharge
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_ProductSubstitution_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product_Substitution(Req_ProductSubstitution_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Add", request);

                var productInsert = await POS_ProductSubstitutions_Insert(new ProductSubstitution()
                {
                    FK_ProductID = request.FK_ProductID,
                    FK_ProductSubstitutionID = request.FK_ProductSubstitutionID,
                    IsQuantified = request.IsQuantified,
                    Quantity = request.Quantity,
                    IsExtraCharge = request.IsExtraCharge,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Product_Substitution(Req_ProductSubstitution_Update request)
        {
            try
            {
                _logger.LogService("Starting Creditor Update", request);

                var productSubstituteResponse = await POS_ProductSubstitutions_Select_Single(new ProductSubstitution()
                {
                    ProductSubstitutionID = request.ProductSubstitutionID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productSubstituteResponse == null)
                {
                    //_logger.LogService("Product not found", request.ProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.ProductNotFound, new List<string> { "Product not found." }, 404);
                }

                var productUpdate = await POS_ProductSubstitutions_Update(new ProductSubstitution()
                {
                    ProductSubstitutionID = request.ProductSubstitutionID,
                    FK_ProductID = request.FK_ProductID ?? productSubstituteResponse.FK_ProductID,
                    FK_ProductSubstitutionID = request.FK_ProductSubstitutionID ?? productSubstituteResponse.FK_ProductSubstitutionID,
                    IsQuantified = request.IsQuantified ?? productSubstituteResponse.IsQuantified,
                    Quantity = request.Quantity ?? productSubstituteResponse.Quantity,
                    IsExtraCharge = request.IsExtraCharge ?? productSubstituteResponse.IsExtraCharge,
                    FK_CreatedUserID = productSubstituteResponse.FK_CreatedUserID,
                    FK_UpdatedUserID = _userContext.UserID,
                    DateCreated = productSubstituteResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Product_Substitution(Req_ProductSubstitution_Delete request)
        {
            try
            {
                _logger.LogService("Starting Creditor Delete", request);

                var productSubstituteResponse = await POS_ProductSubstitutions_Delete(new ProductSubstitution()
                {
                    ProductSubstitutionID = request.ProductSubstitutionID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Creditor add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Product Types

        public async Task<ApiResponse<List<Res_ProductType_List>>> List_Product_Types()
        {
            try
            {
                _logger.LogService("Starting Product Type List");

                var productTypeResponse = await POS_ProductTypes_Select_All(new ProductType()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductType_List>();

                if (productTypeResponse != null && productTypeResponse.Any())
                {
                    foreach (var product in productTypeResponse)
                    {

                        response.Add(new Res_ProductType_List()
                        {
                            POS_ProductTypeID = product.ProductTypeID,
                            ProductType = product.ProductType,
                            IsInventory = product.IsInventory,
                            IsManufactured = product.IsManufactured,
                            IsService = product.IsService,
                            IsComposite = product.IsComposite
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_ProductType_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product_Type(Req_ProductType_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Type Add", request);

                var productTypeInsert = await POS_ProductTypes_Insert(new ProductType()
                {
                    ProductType = request.ProductType,
                    IsInventory = request.IsInventory,
                    IsManufactured = request.IsManufactured,
                    IsService = request.IsService,
                    IsComposite = request.IsComposite
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Product_Type(Req_ProductType_Update request)
        {
            try
            {
                _logger.LogService("Starting Product Type Update", request);

                var productResponse = await POS_ProductTypes_Select_Single(new ProductType()
                {
                    ProductTypeID = request.POS_ProductTypeID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    _logger.LogService("Product type not found", request.POS_ProductTypeID);
                    return ApiResponse.Fail<object>(AppErrorCode.ProductTypeNotFound, new List<string> { "Product type not found." }, 404);
                }

                var productTypeUpdate = await POS_ProductTypes_Update(new ProductType()
                {
                    ProductTypeID = request.POS_ProductTypeID,
                    ProductType = request.ProductType ?? productResponse.ProductType,
                    IsInventory = request.IsInventory ?? productResponse.IsInventory,
                    IsManufactured = request.IsManufactured ?? productResponse.IsManufactured,
                    IsService = request.IsService ?? productResponse.IsService,
                    IsComposite = request.IsComposite ?? productResponse.IsComposite
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during product type add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Product Categories

        public async Task<ApiResponse<List<Res_ProductCategory_List>>> List_Product_Categories()
        {
            try
            {
                _logger.LogService("Starting Product Category List");

                var productCategoryResponse = await POS_ProductCategories_Select_All(new ProductCategory()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ProductCategory_List>();

                if (productCategoryResponse != null && productCategoryResponse.Any())
                {
                    foreach (var productCategory in productCategoryResponse)
                    {

                        response.Add(new Res_ProductCategory_List()
                        {
                            POS_ProductCategoryID = productCategory.ProductCategoryID,
                            CategoryName = productCategory.CategoryName,
                            FK_ProductCategoryID = productCategory.FK_ProductCategoryID,
                            IsMaster = productCategory.IsMaster,
                            IsActive = productCategory.IsActive,
                            DateAdded = productCategory.DateAdded,
                            DateUpdated = productCategory.DateUpdated,
                            CategoryMaster = productCategory.FK_ProductCategoryID.HasValue ? productCategoryResponse.FirstOrDefault(x => x.ProductCategoryID == productCategory.FK_ProductCategoryID)?.CategoryName : "N/A"
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product list", ex);
                return ApiResponse.Fail<List<Res_ProductCategory_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Product_Category(Req_ProductCategory_Add request)
        {
            try
            {
                _logger.LogService("Starting Product Category Add", request);

                var productCategoryResponse = await Category_Select_Single_Name(new ProductCategory()
                {
                    CategoryName = request.CategoryName
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productCategoryResponse != null)
                {
                    _logger.LogService("Category already exists", request.CategoryName);
                    return ApiResponse.Fail<object>(AppErrorCode.CategoryExists, new List<string> { "Category already exists." }, 400);
                }

                var productCategoryInsert = await POS_ProductCategories_Insert(new ProductCategory()
                {
                    CategoryName = request.CategoryName,
                    FK_ProductCategoryID = request.FK_ProductCategoryID,
                    IsMaster = request.IsMaster,
                    IsActive = true,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Product Category add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }

        public async Task<ApiResponse<object>> Update_Product_Category(Req_ProductCategory_Update request)
        {
            try
            {
                _logger.LogService("Starting Product Category Update", request);

                if (request.POS_ProductCategoryID == request.FK_ProductCategoryID)
                {
                    _logger.LogService("Category can't be linked to itself", request.POS_ProductCategoryID);
                    return ApiResponse.Fail<object>(AppErrorCode.CategoryMasterError, new List<string> { "Category can't be linked to itself." }, 500);
                }

                var productCategoryResponse = await POS_ProductCategories_Select_Single(new ProductCategory()
                {
                    ProductCategoryID = request.POS_ProductCategoryID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productCategoryResponse == null)
                {
                    _logger.LogService("Product Category not found", request.POS_ProductCategoryID);
                    return ApiResponse.Fail<object>(AppErrorCode.CategoryNotFound, new List<string> { "Product Category not found." }, 404);
                }

                var productCategoryUpdate = await POS_ProductCategories_Update(new ProductCategory()
                {
                    ProductCategoryID = request.POS_ProductCategoryID,
                    CategoryName = request.CategoryName ?? productCategoryResponse.CategoryName,
                    FK_ProductCategoryID = request.FK_ProductCategoryID ?? productCategoryResponse.FK_ProductCategoryID,
                    IsMaster = request.IsMaster ?? productCategoryResponse.IsMaster,
                    IsActive = true,
                    DateAdded = productCategoryResponse.DateAdded,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during product Category update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
            finally
            {
                _cacheService.RefreshAsync(_userContext.TenantID);
            }
        }
        #endregion

        #region Units

        public async Task<ApiResponse<List<Res_Unit_List>>> List_Units()
        {
            try
            {
                _logger.LogService("Starting Unit List");

                var unitResponse = await POS_Units_Select_All(new Unit()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Unit_List>();

                if (unitResponse != null && unitResponse.Any())
                {
                    foreach (var unit in unitResponse)
                    {

                        response.Add(new Res_Unit_List()
                        {
                            POS_UnitID = unit.UnitID,
                            Unit = unit.Unit,
                            Symbol = unit.Symbol
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during unit list", ex);
                return ApiResponse.Fail<List<Res_Unit_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Unit(Req_Unit_Add request)
        {
            try
            {
                _logger.LogService("Starting Unit Add", request);

                var unitResponse = await Unit_Select_Single_Name(new Unit()
                {
                    Unit = request.Unit
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (unitResponse != null)
                {
                    _logger.LogService("Unit already exists", request.Unit);
                    return ApiResponse.Fail<object>(AppErrorCode.UnitExists, new List<string> { "Unit already exists." }, 400);
                }

                var unitInsert = await POS_Units_Insert(new Unit()
                {
                    Unit = request.Unit,
                    Symbol = request.Symbol,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Unit add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Unit(Req_Unit_Update request)
        {
            try
            {
                _logger.LogService("Starting Unit Update", request);

                var productResponse = await POS_Units_Select_Single(new Unit()
                {
                    UnitID = request.POS_UnitID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (productResponse == null)
                {
                    _logger.LogService("Product not found", request.POS_UnitID);
                    return ApiResponse.Fail<object>(AppErrorCode.UnitNotFound, new List<string> { "Unit not found." }, 404);
                }

                var productUpdate = await POS_Units_Update(new Unit()
                {
                    UnitID = request.POS_UnitID,
                    Unit = request.Unit ?? productResponse.Unit,
                    Symbol = request.Symbol ?? productResponse.Symbol,
                    IsActive = true,
                    DateCreated = productResponse.DateCreated,
                    DateUpdated = DateTime.Now
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Unit add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Served As

        public async Task<ApiResponse<List<Res_ServedAs_List>>> List_Served_As()
        {
            try
            {
                _logger.LogService("Starting Served As List");

                var servedAsResponse = await POS_ServedAs_Select_All(new ServedAs()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_ServedAs_List>();

                if (servedAsResponse != null && servedAsResponse.Any())
                {
                    foreach (var servedAs in servedAsResponse)
                    {
                        response.Add(new Res_ServedAs_List()
                        {
                            ServedAsID = servedAs.ServedAsID,
                            ServedAsType = servedAs.ServedAsType,
                            Name = servedAs.Name,
                            DateCreated = servedAs.DateCreated,
                            DateUpdated = servedAs.DateUpdated
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during served as list", ex);
                return ApiResponse.Fail<List<Res_ServedAs_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Served_As(Req_ServedAs_Add request)
        {
            try
            {
                _logger.LogService("Starting Served As Add", request);

                var servedAsResponse = await POS_ServedAs_Select_All(new ServedAs()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (servedAsResponse != null && servedAsResponse.FirstOrDefault(x => x.ServedAsType == request.ServedAsType && x.Name == request.Name) != null)
                {
                    _logger.LogService("Served As Type Name already exists", request.ServedAsType + " - " + request.Name);
                    return ApiResponse.Fail<object>(AppErrorCode.ServedAsTypeName, new List<string> { "Served As Type Name already exists." }, 400);
                }

                var servedAsInsert = await POS_ServedAs_Insert(new ServedAs()
                {
                    ServedAsType = request.ServedAsType,
                    Name = request.Name,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during served as add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Served_As(Req_ServedAs_Update request)
        {
            try
            {
                _logger.LogService("Starting Served As Update", request);

                var servedAsResponse = await POS_ServedAs_Select_All(new ServedAs()
                {

                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (servedAsResponse != null && servedAsResponse.FirstOrDefault(x => x.ServedAsType == request.ServedAsType && x.Name == request.Name && x.ServedAsID != request.ServedAsID) != null)
                {
                    _logger.LogService("Served As Type Name already exists", request.ServedAsType + " - " + request.Name);
                    return ApiResponse.Fail<object>(AppErrorCode.ServedAsTypeName, new List<string> { "Served As Type Name already exists." }, 400);
                }

                var servedAsUpdate = await POS_ServedAs_Update(new ServedAs()
                {
                    ServedAsID = request.ServedAsID,
                    ServedAsType = request.ServedAsType,
                    Name = request.Name,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Served As update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion

        #region Served As Products

        public async Task<ApiResponse<List<Res_Served_As_Products_List>>> List_Served_As_Products(Req_Served_As_Products_List request)
        {
            try
            {
                _logger.LogService("Starting Served As Product List");

                var servedAsProductsResponse = await POS_ServedAsProducts_Select_All_Product(new ServedAsProduct()
                {
                    FK_ProductID = request.ProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Served_As_Products_List>();

                if (servedAsProductsResponse != null && servedAsProductsResponse.Any())
                {
                    foreach (var servedAs in servedAsProductsResponse)
                    {
                        response.Add(new Res_Served_As_Products_List()
                        {
                            Name = servedAs.Name,
                            ServedAsType = servedAs.ServedAsType,
                            DateCreated = servedAs.DateCreated,
                            DateUpdated = servedAs.DateUpdated,
                            IsQuantified = servedAs.IsQuantified,
                            Quantity = servedAs.Quantity,
                            IsDefault = servedAs.IsDefault,
                            ServedAsID = (int)servedAs.FK_ServedAsID,
                            ServedAsProductID = (int)servedAs.ServedAsProductID,
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during served as product list", ex);
                return ApiResponse.Fail<List<Res_Served_As_Products_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Served_As_Product(Req_Served_As_Products_Add request)
        {
            try
            {
                _logger.LogService("Starting Served As Product Add", request);

                var servedAsProductResponse = await POS_ServedAsProducts_Select_All_Product(new ServedAsProduct()
                {
                    FK_ProductID = request.ProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (servedAsProductResponse != null && servedAsProductResponse.FirstOrDefault(x => x.FK_ServedAsID == request.ServedAsID && x.FK_ProductID == request.ProductID) != null)
                {
                    _logger.LogService("Served As Type Name Product already linked", null);
                    return ApiResponse.Fail<object>(AppErrorCode.ServedAsTypeNameProduct, new List<string> { "Served As Type Name Product already exists." }, 400);
                }

                if ((bool)request.IsDefault)
                    await POS_ServedAsProducts_Set_Default(new ServedAsProduct()
                    {
                        FK_ProductID = request.ProductID,
                        FK_ServedAsID = request.ServedAsID
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var servedAsInsert = await POS_ServedAsProducts_Insert(new ServedAsProduct()
                {
                    FK_ServedAsID = request.ServedAsID,
                    FK_ProductID = request.ProductID,
                    IsQuantified = request.IsQuantified,
                    Quantity = request.Quantity,
                    IsDefault = request.IsDefault,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during served as add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Served_As_Product(Req_Served_As_Product_Update request)
        {
            try
            {
                _logger.LogService("Starting Served As Product Add", request);

                var servedAsProductResponse = await POS_ServedAsProducts_Select_All_Product(new ServedAsProduct()
                {
                    FK_ProductID = request.ProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (servedAsProductResponse != null && servedAsProductResponse.FirstOrDefault(x => x.FK_ServedAsID == request.ServedAsID && x.FK_ProductID == request.ProductID && x.ServedAsProductID != request.ServedAsProductID) != null)
                {
                    _logger.LogService("Served As Type Name Product already linked", null);
                    return ApiResponse.Fail<object>(AppErrorCode.ServedAsTypeNameProduct, new List<string> { "Served As Type Name Product already exists." }, 400);
                }

                if ((bool)request.IsDefault)
                    await POS_ServedAsProducts_Set_Default(new ServedAsProduct()
                    {
                        FK_ProductID = request.ProductID,
                        FK_ServedAsID = request.ServedAsID
                    }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                var servedAsInsert = await POS_ServedAsProducts_Update(new ServedAsProduct()
                {
                    ServedAsProductID = request.ServedAsProductID,
                    FK_ServedAsID = request.ServedAsID,
                    FK_ProductID = request.ProductID,
                    IsQuantified = request.IsQuantified,
                    Quantity = request.Quantity,
                    IsDefault = request.IsDefault,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    FK_CreatedUserID = _userContext.UserID,
                    FK_UpdatedUserID = _userContext.UserID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during served as add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Served_As_Product(Req_Served_As_Products_Remove request)
        {
            try
            {
                _logger.LogService("Starting Served As Product Remove", request);

                var servedAsProductResponse = await POS_ServedAsProducts_Remove_Product(new ServedAsProduct()
                {
                    ServedAsProductID = request.ServedAsProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during served as product remove", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }
        #endregion
    }
}