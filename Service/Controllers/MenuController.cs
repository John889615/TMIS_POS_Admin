using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_Api.ServiceInterfaces.Logging;
using POS_Api.ServiceInterfaces.Menu;
using POS_Api.ServiceInterfaces.Stock;
using POS_Api.Services.Menu;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.ModelsDto.MenuController.DebtorMenu;
using POS_Common.ModelsDto.MenuController.DebtorMenuItemProduct;
using POS_Common.ModelsDto.MenuController.DebtorMenuPrinter;
using POS_Common.ModelsDto.MenuController.Menu;
using POS_Common.ModelsDto.MenuController.MenuItem;
using POS_Common.ModelsDto.MenuController.MenuItemProduct;
using POS_Common.ModelsDto.MenuController.TabLine;
using POS_Common.ModelsDto.MenuController.Tabs;
using POS_Common.ModelsDto.StockController.PurchaseOrder;
using POS_Common.ModelsDto.StockController.PurchaseOrderLine;
using POS_Common.ModelsDto.StockController.StockRequest;
using POS_Common.ModelsDto.StockController.StockRequestLine;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrder;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrderLines;
using System.Net.Sockets;
using System.Text;

namespace POS_Webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MenuController : ControllerBase
    {
        #region Members

        private readonly IMenu_Service _menuService;
        private readonly ILogging_Service _logger;
        #endregion

        #region Constructors

        public MenuController(IMenu_Service menuService, ILogging_Service logger)
        {
            _menuService = menuService;
            _logger = logger;
        }
        #endregion

        #region Methods

        #region Menu

        [HttpPost("list/menus")]
        public async Task<IActionResult> List_Menus_Async([FromBody] Req_Menu_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<object>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<Res_Menu_List>> result;

            try
            {
                result = await _menuService.List_Menus(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Menu_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Menu_List>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("list/menu/tree")]
        public async Task<IActionResult> List_Menu_Tree_Async([FromBody] Req_MenuTree_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuTree_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<MenuTreeResponse> result;

            try
            {
                result = await _menuService.List_Menu_Tree(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<MenuTreeResponse>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<MenuTreeResponse>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("list/all/menu/tree")]
        public async Task<IActionResult> List_All_Menu_Tree_Async()
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<object>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<MenuTreeResponse>> result;

            try
            {
                result = await _menuService.List_All_Menu_Tree();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<MenuTreeResponse>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<MenuTreeResponse>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("copy/menu")]
        public async Task<IActionResult> Copy_Menu_Async([FromBody] Req_Menu_Copy request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Menu_Copy>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Copy_Menu(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/menu")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add_Menu_Async([FromForm] Req_Menu_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Menu_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Add_Menu(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("update/menu")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update_Menu_Async([FromForm] Req_Menu_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Menu_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Update_Menu(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #region Menu Items Default/Global

        [HttpPost("list/menu/items")]
        public async Task<IActionResult> List_Menu_Items_Async([FromBody] Req_MenuItem_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItem_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<Res_MenuItem_List>> result;

            try
            {
                result = await _menuService.List_Menu_Items(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_MenuItem_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_MenuItem_List>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/menu/item")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add_Menu_Item_Async([FromForm] Req_MenuItem_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItem_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Add_Menu_Item(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("update/menu/item")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update_Menu_Item_Async([FromForm] Req_MenuItem_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItem_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Update_Menu_Item(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("remove/menu/item")]
        public async Task<IActionResult> Remove_Menu_Item_Async([FromBody] Req_MenuItem_Remove request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItem_Remove>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Remove_Menu_Item(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #region Menu Item Products Default/Global

        [HttpPost("list/menu/item/products")]
        public async Task<IActionResult> List_Menu_Item_Products_Async([FromBody] Req_MenuItemProduct_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItemProduct_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<Res_MenuItemProduct_List>> result;

            try
            {
                result = await _menuService.List_Menu_Item_Products(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_MenuItemProduct_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_MenuItemProduct_List>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/menu/item/product")]
        public async Task<IActionResult> Add_Menu_Item_Product_Async([FromBody] Req_MenuItemProduct_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItemProduct_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Add_Menu_Item_Product(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("remove/menu/item/product")]
        public async Task<IActionResult> Remove_Menu_Item_Product_Async([FromBody] Req_MenuItemProduct_Remove request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItemProduct_Remove>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Remove_Menu_Item_Product(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #region Debtor Menu

        [HttpPost("update/debtor/menu")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update_Debtor_Menu_Async([FromForm] Req_DebtorMenu_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorMenu_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Update_Debtor_Menu(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("list/debtor/menu/tree")]
        public async Task<IActionResult> List_Debtor_Menu_Tree_Async([FromBody] Req_DebtorMenuTree_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorMenuTree_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<DebtorMenuTreeResponse> result;

            try
            {
                result = await _menuService.List_Debtor_Menu_Tree(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<DebtorMenuTreeResponse>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<DebtorMenuTreeResponse>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #region Debtor Menu Items

        [HttpPost("add/debtor/menu/item")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add_Debtor_Menu_Item_Async([FromForm] Req_MenuItem_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItem_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Add_Debtor_Menu_Item(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("update/debtor/menu/item")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update_Debtor_Menu_Item_Async([FromForm] Req_MenuItem_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItem_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Update_Debtor_Menu_Item(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("remove/debtor/menu/item")]
        public async Task<IActionResult> Remove_Debtor_Menu_Item_Async([FromBody] Req_MenuItem_Remove request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItem_Remove>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Remove_Debtor_Menu_Item(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #region Debtor Menu Item Products

        [HttpPost("add/debtor/menu/item/product/printer")]
        public async Task<IActionResult> Add_Debtor_Menu_Item_Product_Printer_Async([FromBody] Req_DebtorMenuItemProductPrinter_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorMenuItemProductPrinter_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Add_Debtor_Menu_Item_Product_Printer(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/debtor/menu/item/product")]
        public async Task<IActionResult> Add_Debtor_Menu_Item_Product_Async([FromBody] Req_MenuItemProduct_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItemProduct_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Add_Debtor_Menu_Item_Product(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("remove/debtor/menu/item/product")]
        public async Task<IActionResult> Remove_Debtor_Menu_Item_Product_Async([FromBody] Req_MenuItemProduct_Remove request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_MenuItemProduct_Remove>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Remove_Debtor_Menu_Item_Product(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #region Debtor Menu Printers

        [HttpPost("add/debtor/menu/printer")]
        public async Task<IActionResult> Add_Debtor_Menu_Printer_Async([FromBody] Req_DebtorMenuPrinter_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorMenuPrinter_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Add_Debtor_Menu_Printer(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("update/debtor/menu/printer")]
        public async Task<IActionResult> Update_Debtor_Menu_Printer_Async([FromBody] Req_DebtorMenuPrinter_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorMenuPrinter_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _menuService.Update_Debtor_Menu_Printer(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<object>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("list/debtor/menu/printers")]
        public async Task<IActionResult> List_Debtor_Menu_Printers_Async([FromBody] Req_DebtorMenuPrinter_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorMenuPrinter_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<List<Res_DebtorMenuPrinter_List>> result;

            try
            {
                result = await _menuService.List_Debtor_Menu_Printers(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_DebtorMenuPrinter_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Authentication response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #endregion
    }
}
