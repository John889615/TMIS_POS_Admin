using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Security.Claims;

using POS_Common.Models.Menu.POS_DebtorMenuItemProducts;
using POS_Common.Models.Menu.POS_DebtorMenuItems;
using POS_Common.Models.Menu.POS_DebtorMenus;
using POS_Common.Models.Menu.POS_MenuItemProducts;
using POS_Common.Models.Menu.POS_MenuItems;
using POS_Common.Models.Menu.POS_Menus;
using POS_Api.ServiceInterfaces.Menu;
using POS_Api.Services.Menu;
using POS_Api.ServiceInterfaces.Logging;
using Microsoft.Data.SqlClient;
using POS_Common.Models.Stock.POS_PurchaseOrderLines;
using POS_Common.Models.Stock.POS_PurchaseOrders;
using POS_Common.Models.Stock.POS_StockRequestLines;
using POS_Common.Models.Stock.POS_StockRequests;
using POS_Common.ModelsDto.StockController.PurchaseOrder;
using POS_Common.ModelsDto.StockController.PurchaseOrderLine;
using POS_Common.ModelsDto.StockController.StockRequest;
using POS_Common.ModelsDto.StockController.StockRequestLine;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrder;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrderLines;
using TMIS_Common.Sql;
using POS_Common.ModelsDto.MenuController.Menu;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.ModelsDto.MenuController.MenuItem;
using POS_Common.ModelsDto.MenuController.MenuItemProduct;
using TMIS_Common.Interfaces;
using POS_Common.Models.Logs.POS_Logs;
using POS_Common.ModelsDto.MenuController.DebtorMenu;
using POS_Common.Models.EntityData.Users;
using Azure;
using POS_Common.ModelsDto.MenuController.Tabs;
using POS_Common.ModelsDto.MenuController.TabLine;
using POS_Common.Models.Menu.POS_DebtorMenuPrinters;
using POS_Api.Helpers;
using POS_Common.Models.EntityData.POS_Images;
using POS_Common.ModelsDto.MenuController.DebtorMenuItemProduct;
using POS_Api.ServiceInterfaces.Cache;
using POS_Common.ModelsDto.MenuController.DebtorMenuPrinter;

namespace POS_Api.Services
{
    public class Menu_Service : Menu_Custom_Service, IMenu_Service
    {
        #region Members

        private readonly ILogging_Service _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        private readonly ImageHelper _imageHelper;
        private readonly ICache_Service _cacheService;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Menu_Service(IConfiguration configuration, ILogging_Service logger
            , IHttpContextAccessor httpContextAccessor, IUserContext userContext, ImageHelper imageHelper, ICache_Service cache_Service)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
            _imageHelper = imageHelper;
            _cacheService = cache_Service;
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

        #region Menus

        public async Task<ApiResponse<List<Res_Menu_List>>> List_Menus(Req_Menu_List request)
        {
            try
            {
                _logger.LogService("Starting Menu List");

                var menuResponse = await Menus_Select_All(new DebtorMenu()
                {
                    FK_LocationID = request.DebtorID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_Menu_List>();

                if (menuResponse != null && menuResponse.Any())
                {
                    foreach (var menu in menuResponse)
                    {

                        response.Add(new Res_Menu_List()
                        {
                            MenuID = menu.MenuID,
                            MenuName = menu.MenuName,
                            ValidFrom = menu.ValidFrom,
                            ValidTo = menu.ValidTo,
                            DateCreated = menu.DateCreated,
                            DateUpdated = menu.DateUpdated,
                            SourceType = menu.SourceType,
                            Location = menu.Location,
                            IsActive = menu.IsActive,
                            ImageUrl = menu.ImageUrl
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during menu list", ex);
                return ApiResponse.Fail<List<Res_Menu_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<MenuTreeResponse>> List_Menu_Tree(Req_MenuTree_List request)
        {
            try
            {
                _logger.LogService("Starting Menu Tree List");

                // Get flat list from DB
                var menuTreeResponse = await MenuTree_Select(new _Menu()
                {
                    MenuID = request.MenuID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuTreeResponse == null || !menuTreeResponse.Any())
                {
                    return ApiResponse.Success(new MenuTreeResponse());
                }

                // Prepare structures
                var menuID = (int)request.MenuID;
                var menuName = menuTreeResponse.First().MenuName;

                var itemLookup = new Dictionary<int, MenuItemNode>();
                var rootItems = new List<MenuItemNode>();
                var response = new List<MenuTreeResponse>();

                // Build tree
                foreach (var row in menuTreeResponse)
                {
                    if (row.ItemID == null)
                    {
                        response.Add(new MenuTreeResponse()
                        {
                            MenuID = (int)row.MenuID,
                            MenuName = row.MenuName
                        });

                        //return ApiResponse.Success(response);
                    }
                    else
                    {
                        // Ensure the current item exists
                        if (!itemLookup.ContainsKey((int)row.ItemID))
                        {
                            itemLookup[(int)row.ItemID] = new MenuItemNode
                            {
                                ItemID = (int)row.ItemID,
                                Item = row.Item
                            };
                        }

                        var currentItem = itemLookup[(int)row.ItemID];

                        // Add products if available
                        if (row.ProductID.HasValue && !currentItem.Product.Any(p => p.ProductID == row.ProductID.Value))
                        {
                            currentItem.Product.Add(new MenuProduct
                            {
                                POS_MenuItemProductID = (int)row.MenuItemProductID,
                                ProductID = row.ProductID.Value,
                                Product = row.Product
                            });
                        }

                        // Handle parent/child relationship
                        if (row.ParentItemID.HasValue)
                        {
                            if (!itemLookup.ContainsKey(row.ParentItemID.Value))
                            {
                                itemLookup[row.ParentItemID.Value] = new MenuItemNode
                                {
                                    ItemID = row.ParentItemID.Value,
                                    Item = row.ParentItem
                                };
                            }

                            var parentItem = itemLookup[row.ParentItemID.Value];

                            if (!parentItem.ChildItem.Any(c => c.ItemID == currentItem.ItemID))
                            {
                                parentItem.ChildItem.Add(currentItem);
                            }
                        }
                        else
                        {
                            // If no parent, it's a root item
                            if (!rootItems.Any(r => r.ItemID == currentItem.ItemID))
                            {
                                rootItems.Add(currentItem);
                            }
                        }
                    }
                }

                // Build final tree object
                var treeResult = new MenuTreeResponse
                {
                    MenuID = menuID,
                    MenuName = menuName,
                    MenuItems = rootItems
                };

                return ApiResponse.Success(treeResult);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during menu list", ex);
                return ApiResponse.Fail<MenuTreeResponse>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }
        }

        public async Task<ApiResponse<DebtorMenuTreeResponse>> List_Debtor_Menu_Tree(Req_DebtorMenuTree_List request)
        {
            try
            {
                _logger.LogService("Starting Menu Tree List");

                var flatMenuList = await DebtorMenuTree_Select(new DebtorMenu()
                {
                    DebtorMenuID = request.DebtorMenuID,
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (flatMenuList == null || !flatMenuList.Any())
                {
                    return ApiResponse.Success(new DebtorMenuTreeResponse());
                }

                //var groupedMenus = flatMenuList
                //    .GroupBy(x => new { x.MenuID, x.SourceType });

                //var resultList = new List<DebtorMenuTreeResponse>();

                var menuID = (int)request.DebtorMenuID;
                var menuName = flatMenuList.First().MenuName;
                var validFrom = flatMenuList.First()?.ValidFrom;
                var validTo = flatMenuList.First()?.ValidTo;

                var itemLookup = new Dictionary<int, DebtorMenuItemNode>();
                var rootItems = new List<DebtorMenuItemNode>();
                var response = new List<DebtorMenuTreeResponse>();

                foreach (var row in flatMenuList)
                {
                    if (row.ItemID == null)
                    {
                        response.Add(new DebtorMenuTreeResponse()
                        {
                            DebtorMenuID = (int)row.MenuID,
                            MenuName = row.MenuName,
                            ValidFrom = row.ValidFrom,
                            ValidTo = row.ValidTo
                        });
                    }

                    else
                    {
                        // Ensure the current item exists
                        if (!itemLookup.ContainsKey((int)row.ItemID))
                        {
                            itemLookup[(int)row.ItemID] = new DebtorMenuItemNode
                            {
                                ItemID = (int)row.ItemID,
                                Item = row.Item
                            };
                        }

                        var currentItem = itemLookup[(int)row.ItemID];

                        // Add products if available
                        if (row.ProductID.HasValue && !currentItem.Product.Any(p => p.ProductID == row.ProductID.Value))
                        {
                            currentItem.Product.Add(new DebtorMenuProduct
                            {
                                DebtorMenuItemProductID = (int)row.MenuItemProductID,
                                ProductID = row.ProductID.Value,
                                Product = row.Product
                            });
                        }

                        // Handle parent/child relationship
                        if (row.ParentItemID.HasValue)
                        {
                            if (!itemLookup.ContainsKey(row.ParentItemID.Value))
                            {
                                itemLookup[row.ParentItemID.Value] = new DebtorMenuItemNode
                                {
                                    ItemID = row.ParentItemID.Value,
                                    Item = row.ParentItem
                                };
                            }

                            var parentItem = itemLookup[row.ParentItemID.Value];

                            if (!parentItem.ChildItem.Any(c => c.ItemID == currentItem.ItemID))
                            {
                                parentItem.ChildItem.Add(currentItem);
                            }
                        }
                        else
                        {
                            // If no parent, it's a root item
                            if (!rootItems.Any(r => r.ItemID == currentItem.ItemID))
                            {
                                rootItems.Add(currentItem);
                            }
                        }
                    }
                }

                var treeResult = new DebtorMenuTreeResponse
                {
                    DebtorMenuID = menuID,
                    MenuName = menuName,
                    ValidFrom = validFrom,
                    ValidTo = validTo,
                    MenuItems = rootItems
                };

                return ApiResponse.Success(treeResult);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during menu list", ex);
                return ApiResponse.Fail<DebtorMenuTreeResponse>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }
        }

        public async Task<ApiResponse<List<MenuTreeResponse>>> List_All_Menu_Tree()
        {
            try
            {
                _logger.LogService("Starting Menu Tree List");

                var flatMenuList = await MenuTree_Select_All(new _Menu(), _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (flatMenuList == null || !flatMenuList.Any())
                {
                    return ApiResponse.Success(new List<MenuTreeResponse>());
                }

                var groupedMenus = flatMenuList.GroupBy(x => x.MenuID);

                var resultList = new List<MenuTreeResponse>();

                foreach (var menuGroup in groupedMenus)
                {
                    var menuID = menuGroup.Key ?? 0;
                    var menuName = menuGroup.FirstOrDefault()?.MenuName ?? string.Empty;

                    var itemLookup = new Dictionary<int, MenuItemNode>();
                    var rootItems = new List<MenuItemNode>();

                    foreach (var row in menuGroup)
                    {
                        if (row.ItemID == null)
                            continue;

                        // Ensure item exists in lookup
                        if (!itemLookup.ContainsKey((int)row.ItemID))
                        {
                            itemLookup[(int)row.ItemID] = new MenuItemNode
                            {
                                ItemID = (int)row.ItemID,
                                Item = row.Item
                            };
                        }

                        var currentItem = itemLookup[(int)row.ItemID];

                        // Add product if not already added
                        if (row.ProductID.HasValue && !currentItem.Product.Any(p => p.ProductID == row.ProductID.Value))
                        {
                            currentItem.Product.Add(new MenuProduct
                            {
                                POS_MenuItemProductID = row.MenuItemProductID.Value,
                                ProductID = row.ProductID.Value,
                                Product = row.Product
                            });
                        }

                        // Parent-child relation
                        if (row.ParentItemID.HasValue)
                        {
                            if (!itemLookup.ContainsKey(row.ParentItemID.Value))
                            {
                                itemLookup[row.ParentItemID.Value] = new MenuItemNode
                                {
                                    ItemID = row.ParentItemID.Value,
                                    Item = row.ParentItem
                                };
                            }

                            var parent = itemLookup[row.ParentItemID.Value];

                            if (!parent.ChildItem.Any(c => c.ItemID == currentItem.ItemID))
                            {
                                parent.ChildItem.Add(currentItem);
                            }
                        }
                        else
                        {
                            // Root item
                            if (!rootItems.Any(r => r.ItemID == currentItem.ItemID))
                            {
                                rootItems.Add(currentItem);
                            }
                        }
                    }

                    resultList.Add(new MenuTreeResponse
                    {
                        MenuID = menuID,
                        MenuName = menuName,
                        MenuItems = rootItems
                    });
                }

                return ApiResponse.Success(resultList);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during menu list", ex);
                return ApiResponse.Fail<List<MenuTreeResponse>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }
        }

        public async Task<ApiResponse<object>> Add_Menu(Req_Menu_Add request)
        {
            try
            {
                _logger.LogService("Starting Menu Add", request);

                var menuResponse = await Menu_Select_Single_Name(new _Menu()
                {
                    MenuName = request.MenuName
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuResponse != null)
                {
                    _logger.LogService("Menu already exists", request.MenuName);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuExists, new List<string> { "Menu already exists." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuInsert = await POS_Menus_Insert(new _Menu()
                    {
                        MenuName = request.MenuName,
                        IsActive = request.IsActive,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "menus";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }

                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];
                        

                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 1,  // e.g. 1 = Menu
                            FK_ItemID = menuInsert.MenuID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Copy_Menu(Req_Menu_Copy request)
        {
            try
            {
                _logger.LogService("Starting Menu Add", request);

                var menuCopyResponse = await Menu_Copy_To_Debtor(new _Menu()
                {
                    MenuID = request.SourceMenuID,
                    DebtorID = request.TargetDebtorID,
                    CostCenterID = request.TargetCostCenterID,
                    Override = request.Override,
                    UserID = _userContext.UserID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));



                //var debtorMenuPrinter = POS_DebtorMenuPrinters_Insert(new DebtorMenuPrinter()
                //{
                //    FK_DebtorMenuID = menuCopyResponse.DebtorMenuID,
                //    FK_PrinterID = request.DefaultSlipPrinterID,
                //    DateCreated = DateTime.Now,
                //    FK_CreatedUserID = _userContext.UserID,
                //    DateUpdated = DateTime.Now,
                //    FK_UpdatedUserID = null
                //}, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu copy", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Menu(Req_Menu_Update request)
        {
            try
            {
                _logger.LogService("Starting Menu Update", request);

                var menuResponse = await POS_Menus_Select_Single(new _Menu()
                {
                    MenuID = request.POS_MenuID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuResponse == null)
                {
                    _logger.LogService("Menu not found", request.POS_MenuID);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuNotFound, new List<string> { "Menu not found." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuInsert = await POS_Menus_Update(new _Menu()
                    {
                        MenuID = request.POS_MenuID,
                        MenuName = request.MenuName ?? menuResponse.MenuName,
                        IsActive = request.IsActive ?? menuResponse.IsActive,
                        DateUpdated = DateTime.Now,
                        DateCreated = menuResponse.DateCreated
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "menus";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }

                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];
                        

                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 1,
                            FK_ItemID = menuInsert.MenuID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Menu Items

        public async Task<ApiResponse<List<Res_MenuItem_List>>> List_Menu_Items(Req_MenuItem_List request)
        {
            try
            {
                _logger.LogService("Starting Menu Item List");

                var menuItemResponse = await MenuItems_Select_All_MenuItems(new MenuItem()
                {
                    FK_MenuID = request.MenuID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_MenuItem_List>();

                if (menuItemResponse != null && menuItemResponse.Any())
                {
                    foreach (var menuItem in menuItemResponse)
                    {

                        response.Add(new Res_MenuItem_List()
                        {
                            POS_MenuItemID = menuItem.MenuItemID,
                            FK_MenuID = menuItem.FK_MenuID,
                            MenuName = menuItem.MenuName,
                            Item = menuItem.Item,
                            Description = menuItem.Description,
                            FK_ParentMenuItemID = menuItem.FK_MenuItemID,
                            ParentItem = menuItem.ParentItem
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during menu list", ex);
                return ApiResponse.Fail<List<Res_MenuItem_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Menu_Item(Req_MenuItem_Add request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Add", request);

                var menuItemResponse = await MenuItem_Select_Single_Name(new MenuItem()
                {
                    Item = request.Item
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                if (menuItemResponse != null)
                {
                    if (menuItemResponse.Item == request.Item && menuItemResponse.FK_MenuID == request.FK_MenuID)
                    {
                        _logger.LogService("Menu Item already exists", request.Item);
                        return ApiResponse.Fail<object>(AppErrorCode.MenuItemExists, new List<string> { "Menu Item already exists." }, 400);
                    }
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemInsert = await POS_MenuItems_Insert(new MenuItem()
                    {
                        FK_MenuID = request.FK_MenuID,
                        Item = request.Item,
                        Description = request.Description,
                        FK_MenuItemID = request.FK_POS_MenuItemID,
                        DateCreated = DateTime.Now,
                        FK_CreatedUserID = _userContext.UserID,
                        DateUpdated = DateTime.Now,
                        FK_UpdatedUserID = _userContext.UserID
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "menu_items";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }

                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];
                        

                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 2,  // e.g. 1 = Menu
                            FK_ItemID = menuItemInsert.MenuItemID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Menu_Item(Req_MenuItem_Update request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Update", request);

                var menuItemResponse = await POS_MenuItems_Select_Single(new MenuItem()
                {
                    MenuItemID = request.POS_MenuItemID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuItemResponse == null)
                {
                    _logger.LogService("Menu Item not found", request.POS_MenuItemID);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuItemNotFound, new List<string> { "Menu Item not found." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemInsert = await POS_MenuItems_Update(new MenuItem()
                    {
                        MenuItemID = request.POS_MenuItemID,
                        FK_MenuID = request.FK_MenuID ?? menuItemResponse.FK_MenuID,
                        Item = request.Item ?? menuItemResponse.Item,
                        Description = request.Description ?? menuItemResponse.Description,
                        FK_MenuItemID = request.FK_POS_MenuItemID ?? menuItemResponse.FK_MenuItemID,
                        DateUpdated = DateTime.Now,
                        FK_UpdatedUserID = _userContext.UserID,
                        DateCreated = menuItemResponse.DateCreated,
                        FK_CreatedUserID = menuItemResponse.FK_CreatedUserID
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "menu_items";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }

                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];
                        

                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 2,  // e.g. 1 = Menu
                            FK_ItemID = request.POS_MenuItemID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Menu_Item(Req_MenuItem_Remove request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Remove", request);

                var menuItemResponse = await POS_MenuItems_Select_Single(new MenuItem()
                {
                    MenuItemID = request.POS_MenuItemID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuItemResponse == null)
                {
                    _logger.LogService("Menu Item not found", request.POS_MenuItemID);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuItemNotFound, new List<string> { "Menu Item not found." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemRemove = await MenuItem_Remove(new MenuItem()
                    {
                        MenuItemID = request.POS_MenuItemID
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item remove", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Menu Item Products

        public async Task<ApiResponse<List<Res_MenuItemProduct_List>>> List_Menu_Item_Products(Req_MenuItemProduct_List request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Product List");

                var menuItemProductResponse = await MenuItemProducts_Select_All_MenuItemProducts(new MenuItemProduct()
                {
                    FK_MenuItemID = request.FK_MenuItemID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));


                var response = new List<Res_MenuItemProduct_List>();

                if (menuItemProductResponse != null && menuItemProductResponse.Any())
                {
                    foreach (var menuItemProduct in menuItemProductResponse)
                    {

                        response.Add(new Res_MenuItemProduct_List()
                        {
                            POS_MenuItemProductID = menuItemProduct.MenuItemProductID,
                            FK_MenuItemID = menuItemProduct.FK_MenuItemID,
                            Item = menuItemProduct.Item,
                            FK_ProductID = menuItemProduct.FK_ProductID,
                            ProductName = menuItemProduct.ProductName
                        });
                    }
                }

                return ApiResponse.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during menu list", ex);
                return ApiResponse.Fail<List<Res_MenuItemProduct_List>>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Add_Menu_Item_Product(Req_MenuItemProduct_Add request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Product Add", request);

                var menuItemProductResponse = await MenuItemProduct_Select_Single_ID(new MenuItemProduct()
                {
                    FK_MenuItemID = request.FK_MenuItemID,
                    FK_ProductID = request.FK_ProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuItemProductResponse != null)
                {
                    _logger.LogService("Menu Item Product already exists", request.FK_ProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuItemProductExists, new List<string> { "Menu Item Product already exists." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemProductInsert = await POS_MenuItemProducts_Insert(new MenuItemProduct()
                    {
                        FK_ProductID = request.FK_ProductID,
                        FK_MenuItemID = request.FK_MenuItemID,
                        DateCreated = DateTime.Now,
                        FK_CreatedUserID = _userContext.UserID,
                        DateUpdated = DateTime.Now,
                        FK_UpdatedUserID = _userContext.UserID
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Menu_Item_Product(Req_MenuItemProduct_Remove request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Product Update", request);

                var menuItemProductResponse = await POS_MenuItemProducts_Select_Single(new MenuItemProduct()
                {
                    MenuItemProductID = request.POS_MenuItemProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuItemProductResponse == null)
                {
                    _logger.LogService("Menu Item Product not found", request.POS_MenuItemProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuItemProductNotFound, new List<string> { "Menu Item Product not found." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemProductRemove = await MenuItemProducts_Remove(new MenuItemProduct()
                    {
                        MenuItemProductID = request.POS_MenuItemProductID,
                    }, sqlConn);

                    var log = await Logs_Service.POS_Logs_Insert(new POS_Log()
                    {
                        Action = "Menu Item Product Delete",
                        ItemID = request.POS_MenuItemProductID,
                        Item = menuItemProductResponse.ProductName,
                        FK_UserID = _userContext.UserID,
                        ActionDate = DateTime.Now
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Debtor Menus

        public async Task<ApiResponse<object>> Update_Debtor_Menu(Req_DebtorMenu_Update request)
        {
            try
            {
                _logger.LogService("Starting Debtor Menu Update", request);

                var menuResponse = await POS_DebtorMenus_Select_Single(new DebtorMenu()
                {
                    DebtorMenuID = request.POS_MenuID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuResponse == null)
                {
                    _logger.LogService("Debtor Menu not found", request.POS_MenuID);
                    return ApiResponse.Fail<object>(AppErrorCode.DebtorMenuNotFound, new List<string> { "Debtor Menu not found." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    //if (request.IsActive == true)
                    //{
                    //    if (request.FK_CostCenterID == null)
                    //    {
                    //        var updateMenuStatus = await DebtorMenus_Update_Status(new DebtorMenu()
                    //        {
                    //            DebtorMenuID = request.POS_MenuID,
                    //        }, sqlConn);
                    //    }

                    //    else
                    //    {
                    //        var updateMenuStatus = await DebtorMenus_Update_Status_CostCenter(new DebtorMenu()
                    //        {
                    //            DebtorMenuID = request.POS_MenuID,
                    //            FK_CostCenterID = request.FK_CostCenterID
                    //        }, sqlConn);
                    //    }
                    //}

                    var menuInsert = await POS_DebtorMenus_Update(new DebtorMenu()
                    {
                        DebtorMenuID = request.POS_MenuID,
                        FK_LocationID = request.FK_DebtorID ?? menuResponse.FK_LocationID,
                        FK_CostCenterID = request.FK_CostCenterID ?? menuResponse.FK_CostCenterID,
                        FK_MenuID = menuResponse.FK_MenuID,
                        MenuName = request.MenuName ?? menuResponse.MenuName,
                        ValidFrom = request.ValidFrom ?? menuResponse.ValidFrom,
                        ValidTo = request.ValidTo ?? menuResponse.ValidTo,
                        IsActive = true,
                        DateCreated = menuResponse.DateCreated,
                        DateUpdated = DateTime.Now
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "menus";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }

                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];


                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 1,
                            FK_ItemID = menuInsert.MenuID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Debtor Menu update", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Debtor Menu Items

        public async Task<ApiResponse<object>> Add_Debtor_Menu_Item(Req_MenuItem_Add request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Add", request);

                //var menuItemResponse = await DebtorMenuItem_Select_Single_Name(new MenuItem()
                //{
                //    Item = request.Item
                //}, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                //if (menuItemResponse.Item == request.Item && menuItemResponse.FK_MenuID == request.FK_MenuID)
                //{
                //    _logger.LogService("Menu Item already exists", request.Item);
                //    return ApiResponse.Fail<object>(AppErrorCode.MenuItemExists, new List<string> { "Menu Item already exists." }, 400);
                //}

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemInsert = await POS_DebtorMenuItems_Insert(new DebtorMenuItem()
                    {
                        FK_DebtorMenuID = request.FK_MenuID,
                        Item = request.Item,
                        Description = request.Description,
                        FK_MenuItemID = request.FK_POS_MenuItemID,
                        DateCreated = DateTime.Now,
                        FK_CreatedUserID = _userContext.UserID,
                        DateUpdated = DateTime.Now,
                        FK_UpdatedUserID = _userContext.UserID
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "menu_items";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }

                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];


                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 2,  // e.g. 1 = Menu
                            FK_ItemID = menuItemInsert.DebtorMenuItemID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Debtor_Menu_Item(Req_MenuItem_Update request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Update", request);

                var menuItemResponse = await POS_DebtorMenuItems_Select_Single(new DebtorMenuItem()
                {
                    DebtorMenuItemID = request.POS_MenuItemID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuItemResponse == null)
                {
                    _logger.LogService("Menu Item not found", request.POS_MenuItemID);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuItemNotFound, new List<string> { "Menu Item not found." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemInsert = await POS_DebtorMenuItems_Update(new DebtorMenuItem()
                    {
                        DebtorMenuItemID = request.POS_MenuItemID,
                        FK_DebtorMenuID = request.FK_MenuID ?? menuItemResponse.FK_DebtorMenuID,
                        Item = request.Item ?? menuItemResponse.Item,
                        Description = request.Description ?? menuItemResponse.Description,
                        FK_MenuItemID = request.FK_POS_MenuItemID ?? menuItemResponse.FK_MenuItemID,
                        DateUpdated = DateTime.Now,
                        FK_UpdatedUserID = _userContext.UserID,
                        DateCreated = menuItemResponse.DateCreated,
                        FK_CreatedUserID = menuItemResponse.FK_CreatedUserID
                    }, sqlConn);

                    var globalSettings = (_cacheService.GetCacheAsync(_userContext.TenantID).Result.GlobalSettings)
                                        .Where(x => x.Environment == _configuration["Environment"]).ToList();

                    if (request.ImageFile != null)
                    {
                        var relativePath = "menu_items";

                        var imageUrl = await _imageHelper.SaveImageAsync(request.ImageFile, relativePath, globalSettings);

                        if (imageUrl == null)
                        {
                            return ApiResponse.Fail<object>(AppErrorCode.ImageUploadFailed, new List<string> { "Image upload failed." }, 500);
                        }

                        string rootPath = _configuration["ImageStorage:RootFileSystemPath"];


                        await EntityData.EntityData_Custom_Service.POS_Images_Insert_Replace(new Image
                        {
                            FK_ImageCategoryID = 2,  // e.g. 1 = Menu
                            FK_ItemID = request.POS_MenuItemID,
                            FileSystemPath = rootPath,
                            RelativePath = relativePath,
                            ImageName = Path.GetFileName(imageUrl.BaseUrl),
                            FileExtension = Path.GetExtension(imageUrl.BaseUrl),
                            ImageUrl = imageUrl.BaseUrl,
                            LocalUrl = imageUrl.LocalUrl,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now
                        }, sqlConn);
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Debtor_Menu_Item(Req_MenuItem_Remove request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Remove", request);

                //var menuItemResponse = await POS_DebtorMenuItems_Select_Single(new DebtorMenuItem()
                //{
                //    DebtorMenuItemID = request.POS_MenuItemID
                //}, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                //if (menuItemResponse == null)
                //{
                //    _logger.LogService("Menu Item not found", request.POS_MenuItemID);
                //    return ApiResponse.Fail<object>(AppErrorCode.MenuItemNotFound, new List<string> { "Menu Item not found." }, 400);
                //}

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemRemove = await DebtorMenuItem_Remove(new DebtorMenuItem()
                    {
                        DebtorMenuItemID = request.POS_MenuItemID
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item remove", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Debtor Menu Item Products

        public Task<ApiResponse<object>> Add_Debtor_Menu_Item_Product_Printer(Req_DebtorMenuItemProductPrinter_Add request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<object>> Add_Debtor_Menu_Item_Product(Req_MenuItemProduct_Add request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Product Add", request);

                var menuItemProductResponse = await DebtorMenuItemProduct_Select_Single_ID(new DebtorMenuItemProduct()
                {
                    FK_DebtorMenuItemID = request.FK_MenuItemID,
                    FK_ProductID = request.FK_ProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuItemProductResponse != null)
                {
                    _logger.LogService("Menu Item Product already exists", request.FK_ProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuItemProductExists, new List<string> { "Menu Item Product already exists." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemProductInsert = await POS_DebtorMenuItemProducts_Insert(new DebtorMenuItemProduct()
                    {
                        FK_ProductID = request.FK_ProductID,
                        FK_DebtorMenuItemID = request.FK_MenuItemID,
                        IsActive = true,
                        DateCreated = DateTime.Now,
                        FK_CreatedUserID = _userContext.UserID,
                        DateUpdated = DateTime.Now,
                        FK_UpdatedUserID = _userContext.UserID
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item Product add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Remove_Debtor_Menu_Item_Product(Req_MenuItemProduct_Remove request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Product Update", request);

                var menuItemProductResponse = await POS_DebtorMenuItemProducts_Select_Single(new DebtorMenuItemProduct()
                {
                    MenuItemProductID = request.POS_MenuItemProductID
                }, _configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString())));

                if (menuItemProductResponse == null)
                {
                    _logger.LogService("Menu Item Product not found", request.POS_MenuItemProductID);
                    return ApiResponse.Fail<object>(AppErrorCode.MenuItemProductNotFound, new List<string> { "Menu Item Product not found." }, 400);
                }

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var menuItemProductRemove = await DebtorMenuItemProducts_Remove(new DebtorMenuItemProduct()
                    {
                        MenuItemProductID = request.POS_MenuItemProductID,
                    }, sqlConn);

                    //var log = await Logs_Service.POS_Logs_Insert(new POS_Log()
                    //{
                    //    Action = "Menu Item Product Delete",
                    //    ItemID = request.POS_MenuItemProductID,
                    //    Item = menuItemProductResponse.ProductName,
                    //    FK_UserID = _userContext.UserID,
                    //    ActionDate = DateTime.Now
                    //}, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }
        #endregion

        #region Debtor Menu Printers

        public async Task<ApiResponse<object>> Add_Debtor_Menu_Printer(Req_DebtorMenuPrinter_Add request)
        {
            try
            {
                _logger.LogService("Starting Debtor Menu Printer Item Add", request);

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorMenuPrinterInsert = await POS_DebtorMenuPrinters_Insert(new DebtorMenuPrinter()
                    {
                        FK_DebtorMenuID = request.FK_DebtorMenuID,
                        FK_PrinterID = request.FK_PrinterID,
                        FK_OrderSlipTypeID = request.FK_OrderSlipTypeID,
                        DateCreated = DateTime.Now,
                        FK_CreatedUserID = _userContext.UserID,
                        DateUpdated = DateTime.Now,
                        FK_UpdatedUserID = _userContext.UserID
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Update_Debtor_Menu_Printer(Req_DebtorMenuPrinter_Update request)
        {
            try
            {
                _logger.LogService("Starting Menu Item Update", request);

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorMenuPrinterResponse = await POS_DebtorMenuPrinters_Select_Single(new DebtorMenuPrinter()
                    {
                        DebtorMenuPrinterID = request.DebtorMenuPrinterID
                    }, sqlConn);


                    var debtorMenuPrinterUpdate = await POS_DebtorMenuPrinters_Update(new DebtorMenuPrinter()
                    {
                        DebtorMenuPrinterID = request.DebtorMenuPrinterID,
                        FK_DebtorMenuID = request.FK_DebtorMenuID,
                        FK_PrinterID = request.FK_PrinterID,
                        FK_OrderSlipTypeID = request.FK_OrderSlipTypeID,
                        DateCreated = debtorMenuPrinterResponse.DateCreated,
                        FK_CreatedUserID = debtorMenuPrinterResponse.FK_CreatedUserID,
                        DateUpdated = DateTime.Now,
                        FK_UpdatedUserID = _userContext.UserID
                    }, sqlConn);
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Menu Item add", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message
                }, 500);
            }
        }

        public async Task<ApiResponse<object>> Delete_Debtor_Menu_Printer(Req_DebtorMenuPrinter_Delete request)
        {
            try
            {
                _logger.LogService("Starting Debtor Menu Printer Delete", request);

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorMenuPrinterResponse = await POS_DebtorMenuPrinters_Select_Single(new DebtorMenuPrinter()
                    {
                        DebtorMenuPrinterID = request.DebtorMenuPrinterID
                    }, sqlConn);

                    if (debtorMenuPrinterResponse == null)
                    {
                        _logger.LogService("Debtor Menu Printer not found", request.DebtorMenuPrinterID);
                        return ApiResponse.Fail<object>(AppErrorCode.ValidationError, new List<string> { "Debtor menu printer link not found." }, 404);
                    }

                    var debtorMenuPrinterDelete = await POS_DebtorMenuPrinters_Delete(new DebtorMenuPrinter()
                    {
                        DebtorMenuPrinterID = request.DebtorMenuPrinterID
                    }, sqlConn);

                    var verifyLink = await POS_DebtorMenuPrinters_Select_Single(new DebtorMenuPrinter()
                    {
                        DebtorMenuPrinterID = request.DebtorMenuPrinterID
                    }, sqlConn);

                    if (verifyLink != null)
                    {
                        _logger.LogService("Debtor Menu Printer delete did not remove the row", request.DebtorMenuPrinterID);
                        return ApiResponse.Fail<object>(
                            AppErrorCode.ServerError,
                            new List<string> { "Delete did not remove the debtor menu printer link. Verify the POS_DebtorMenuPrinters_delete stored procedure exists and runs without errors." },
                            500
                        );
                    }
                }

                return ApiResponse.Success(new object());
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Debtor Menu Printer delete", ex);
                return ApiResponse.Fail<object>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        public async Task<ApiResponse<List<Res_DebtorMenuPrinter_List>>> List_Debtor_Menu_Printers(Req_DebtorMenuPrinter_List request)
        {
            try
            {
                _logger.LogService("Starting Debtor Menu Printer List", request);

                using (SqlConnection sqlConn = SqlClient.CreateInstance(_configuration.GetConnectionString(string.Format("ApplicationDb_{0}", _userContext.TenantID.ToString()))))
                {
                    await sqlConn.OpenAsync();

                    var debtorMenuPrinters = await DebtorMenuPrinters_Select_All_ID(new Req_DebtorMenuPrinter_List()
                    {
                        DebtorMenuID = request.DebtorMenuID
                    }, sqlConn);

                    return ApiResponse.Success(debtorMenuPrinters);
                }
            }
            catch (Exception ex)
            {
                _logger.LogService("Exception during Debtor Menu Printer List", ex);
                return ApiResponse.Fail<List<Res_DebtorMenuPrinter_List>>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }
        }

        #endregion

        #endregion
    }
}






