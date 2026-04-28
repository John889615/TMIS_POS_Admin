using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using POS_Api.ServiceInterfaces;
using POS_Api.ServiceInterfaces.Inventory;
using POS_Api.ServiceInterfaces.Logging;
using POS_Api.Services.Inventory;
using POS_Common.Models;
using POS_Common.Requests.Users;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using POS_Common.Enums;
using POS_Common.ModelsDto.InventoryController.Product;
using POS_Common.ModelsDto.InventoryController.ProductCategory;
using POS_Common.ModelsDto.InventoryController.Unit;
using POS_Common.ModelsDto.InventoryController.ProductType;
using POS_Common.ModelsDto.InventoryController.ProductCombination;
using POS_Common.ModelsDto.InventoryController.ProductExtraCategories;
using POS_Common.ModelsDto.InventoryController.ProductExtra;
using POS_Common.ModelsDto.InventoryController.ProductPreparation;
using POS_Common.ModelsDto.InventoryController.ProductPreparationMethod;
using POS_Common.ModelsDto.InventoryController.ProductSubstitution;
using POS_Common.ModelsDto.InventoryController.ServedAs;
using POS_Common.ModelsDto.InventoryController.ServedAsProducts;

namespace POS_Webservice.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        #region Members

        private readonly IInventory_Service _inventoryService;
        private readonly ILogging_Service _logger;
        #endregion

        #region Constructors

        public InventoryController(IInventory_Service inventoryService, ILogging_Service logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }
        #endregion

        #region Methods

        #region Products

        [HttpGet("list/products")]
        public async Task<IActionResult> List_Products_Async()
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

            ApiResponse<List<Res_Product_List>> result;

            try
            {
                result = await _inventoryService.List_Products();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Product_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_Product_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add_Product_Async([FromForm] Req_Product_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Product_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product(request);
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

        [HttpPost("update/product")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update_Product_Async([FromForm] Req_Product_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Product_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product(request);
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

        #region Product Combinations

        [HttpPost("list/product/combinations")]
        public async Task<IActionResult> List_Product_Combinations_Async([FromBody] Req_ProductCombination_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductCombination_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<Res_ProductCombination_List>> result;

            try
            {
                result = await _inventoryService.List_Product_Combinations(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductCombination_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductCombination_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product/combination")]
        public async Task<IActionResult> Add_Product_Combination_Async([FromBody] Req_ProductCombination_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductCombination_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product_Combination(request);
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

        [HttpPost("update/product/combination")]
        public async Task<IActionResult> Update_Product_Combination_Async([FromBody] Req_ProductCombination_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductCombination_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product_Combination(request);
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

        [HttpPost("remove/product/combination")]
        public async Task<IActionResult> Remove_Product_Combination_Async([FromBody] Req_ProductCombination_Delete request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductCombination_Delete>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Remove_Product_Combination(request);
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

        #region Product Extra Categories

        [HttpGet("list/product/extra/categories")]
        public async Task<IActionResult> List_Product_Extra_Categories_Async()
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

            ApiResponse<List<Res_ProductExtraCategory_List>> result;

            try
            {
                result = await _inventoryService.List_Product_Extra_Categories();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductExtraCategory_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductExtraCategory_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product/extra/category")]
        public async Task<IActionResult> Add_Product_Extra_Category_Async([FromBody] Req_ProductExtraCategory_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductExtraCategory_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product_Extra_Category(request);
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

        [HttpPost("update/product/extra/category")]
        public async Task<IActionResult> Update_Product_Extra_Category_Async([FromBody] Req_ProductExtraCategory_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductExtraCategory_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product_Extra_Category(request);
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

        #region Product Extras

        [HttpPost("list/product/extras")]
        public async Task<IActionResult> List_Product_Extras_Async([FromBody] Req_ProductExtra_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductExtra_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<Res_ProductExtra_List>> result;

            try
            {
                result = await _inventoryService.List_Product_Extras(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductExtra_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductExtra_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product/extra")]
        public async Task<IActionResult> Add_Product_Extra_Async([FromBody] Req_ProductExtra_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductExtra_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product_Extra(request);
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

        [HttpPost("update/product/extra")]
        public async Task<IActionResult> Update_Product_Extra_Async([FromBody] Req_ProductExtra_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductExtra_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product_Extra(request);
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

        [HttpPost("remove/product/extra")]
        public async Task<IActionResult> Remove_Product_Extra_Async([FromBody] Req_ProductExtra_Delete request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductExtra_Delete>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Remove_Product_Extra(request);
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

        #region Product Preparation

        [HttpPost("list/product/preparation")]
        public async Task<IActionResult> List_Product_Preparation_Async([FromBody] Req_ProductPreparation_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductPreparation_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<Res_ProductPreparation_List>> result;

            try
            {
                result = await _inventoryService.List_Product_Preparation(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductPreparation_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductPreparation_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product/preparation")]
        public async Task<IActionResult> Add_Product_Preparation_Async([FromBody] Req_ProductPreparation_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductPreparation_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product_Preparation(request);
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

        [HttpPost("update/product/preparation")]
        public async Task<IActionResult> Update_Product_Preparation_Async([FromBody] Req_ProductPreparation_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductPreparation_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product_Preparation(request);
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

        [HttpPost("remove/product/preparation")]
        public async Task<IActionResult> Remove_Product_Preparation_Async([FromBody] Req_ProductPreparation_Delete request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductPreparation_Delete>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Remove_Product_Preparation(request);
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

        #region Product Preparation Methods

        [HttpGet("list/product/preparation/methods")]
        public async Task<IActionResult> List_Product_Preparation_Methods_Async()
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

            ApiResponse<List<Res_ProductPreparationMethod_List>> result;

            try
            {
                result = await _inventoryService.List_Product_Preparation_Methods();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductPreparationMethod_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductPreparationMethod_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product/preparation/method")]
        public async Task<IActionResult> Add_Product_Preparation_Method_Async([FromBody] Req_ProductPreparationMethod_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductPreparationMethod_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product_Preparation_Method(request);
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

        [HttpPost("update/product/preparation/method")]
        public async Task<IActionResult> Update_Product_Preparation_Method_Async([FromBody] Req_ProductPreparationMethod_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductPreparationMethod_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product_Preparation_Method(request);
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

        #region Product Substitutions

        [HttpPost("list/product/substitutions")]
        public async Task<IActionResult> List_Product_Substitutions_Async(Req_Substitution_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Substitution_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<Res_ProductSubstitution_List>> result;

            try
            {
                result = await _inventoryService.List_Product_Substitutions(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductSubstitution_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductSubstitution_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product/substitution")]
        public async Task<IActionResult> Add_Product_Substitution_Async([FromBody] Req_ProductSubstitution_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductSubstitution_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product_Substitution(request);
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

        [HttpPost("update/product/substitution")]
        public async Task<IActionResult> Update_Product_Substitution_Async([FromBody] Req_ProductSubstitution_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductSubstitution_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product_Substitution(request);
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

        [HttpPost("remove/product/substitution")]
        public async Task<IActionResult> Remove_Product_Substitution_Async([FromBody] Req_ProductSubstitution_Delete request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductSubstitution_Delete>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Remove_Product_Substitution(request);
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

        #region Product Types

        [HttpGet("list/product/types")]
        public async Task<IActionResult> List_Product_Types_Async()
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

            ApiResponse<List<Res_ProductType_List>> result;

            try
            {
                result = await _inventoryService.List_Product_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductType_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductType_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product/type")]
        public async Task<IActionResult> Add_Product_Type_Async([FromBody] Req_ProductType_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductType_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product_Type(request);
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

        [HttpPost("update/product/type")]
        public async Task<IActionResult> Update_Product_Type_Async([FromBody] Req_ProductType_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductType_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product_Type(request);
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

        #region Product Categories

        [HttpGet("list/product/categories")]
        public async Task<IActionResult> List_Product_Categories_Async()
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

            ApiResponse<List<Res_ProductCategory_List>> result;

            try
            {
                result = await _inventoryService.List_Product_Categories();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductCategory_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductCategory_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/product/category")]
        public async Task<IActionResult> Add_Product_Category_Async([FromBody] Req_ProductCategory_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductCategory_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Product_Category(request);
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

        [HttpPost("update/product/category")]
        public async Task<IActionResult> Update_Product_Category_Async([FromBody] Req_ProductCategory_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ProductCategory_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Product_Category(request);
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

        #region Units

        [HttpGet("list/units")]
        public async Task<IActionResult> List_Units_Async()
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

            ApiResponse<List<Res_Unit_List>> result;

            try
            {
                result = await _inventoryService.List_Units();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Unit_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Category_Create"));

                return StatusCode(500, ApiResponse.Fail<Res_Unit_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "Create category response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Create category succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/unit")]
        public async Task<IActionResult> Add_Unit_Async([FromBody] Req_Unit_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Unit_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Unit(request);
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

        [HttpPost("update/unit")]
        public async Task<IActionResult> Update_Unit_Async([FromBody] Req_Unit_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Unit_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Unit(request);
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

        #region Served As

        [HttpGet("list/servedas")]
        public async Task<IActionResult> List_Served_As_Async()
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

            ApiResponse<List<Res_ServedAs_List>> result;

            try
            {
                result = await _inventoryService.List_Served_As();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ServedAs_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.List_Served_As"));

                return StatusCode(500, ApiResponse.Fail<Res_ServedAs_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "List served as response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List served as failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List served as succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/servedas")]
        public async Task<IActionResult> Add_Served_As_Async([FromBody] Req_ServedAs_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Unit_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Served_As(request);
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
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Add_Served_As"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Add Served As response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Add Served As failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Add Served As succeeded", result.Data));
            return Ok(result);
        }


        [HttpPost("update/servedas")]
        public async Task<IActionResult> Update_Served_As_Async([FromBody] Req_ServedAs_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Unit_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Served_As(request);
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
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Update_Served_As"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Update Served As response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Update Served As failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Update Served As succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #region Served As Products

        [HttpPost("list/servedas/products")]
        public async Task<IActionResult> List_Served_As_Products_Async([FromBody] Req_Served_As_Products_List request)
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

            ApiResponse<List<Res_Served_As_Products_List>> result;

            try
            {
                result = await _inventoryService.List_Served_As_Products(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Served_As_Products_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.List_Served_As_Products"));

                return StatusCode(500, ApiResponse.Fail<Res_ServedAs_List>(
                    AppErrorCode.ServerError,
                    new List<string> { "List served as products response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List served as products failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List served as products succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("add/servedas/product")]
        public async Task<IActionResult> Add_Served_As_Product_Async([FromBody] Req_Served_As_Products_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Unit_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Add_Served_As_Product(request);
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
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Add_Served_As_Product"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Add Served As Product response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Add Served As Product failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Add Served As Product succeeded", result.Data));
            return Ok(result);
        }



        [HttpPost("update/servedas/product")]
        public async Task<IActionResult> Update_Served_As_Product_Async([FromBody] Req_Served_As_Product_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Unit_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Update_Served_As_Product(request);
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
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Update_Served_As_Product"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Update Served As Product response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Update Served As Product failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Update Served As Product succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("remove/servedas/product")]
        public async Task<IActionResult> Remove_Served_As_Product([FromBody] Req_Served_As_Products_Remove request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Unit_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _inventoryService.Remove_Served_As_Product(request);
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
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _inventoryService.Remove_Served_As_Product"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "Remove Served As Product response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Remove Served As Product failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Remove Served As Product succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #endregion
    }
}
