using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_Api.ServiceInterfaces.Logging;
using POS_Api.ServiceInterfaces.Stock;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.ModelsDto.DebtorsController;
using POS_Common.ModelsDto.StockController.CostCenterProduct;
using POS_Common.ModelsDto.StockController.DebtorProduct;
using POS_Common.ModelsDto.StockController.DebtorProductPrice;
using POS_Common.ModelsDto.StockController.PriceCode;
using POS_Common.ModelsDto.StockController.PurchaseOrder;
using POS_Common.ModelsDto.StockController.PurchaseOrderLine;
using POS_Common.ModelsDto.StockController.StockRequest;
using POS_Common.ModelsDto.StockController.StockRequestLine;
using POS_Common.ModelsDto.StockController.StockTransfer;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrder;
using POS_Common.ModelsDto.StockController.SubmittedPurchaseOrderLines;
using POS_Common.ModelsDto.StockController.SupplierProduct;

namespace POS_Webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        #region Members

        private readonly IStock_Service _stockService;
        private readonly ILogging_Service _logger;
        #endregion

        #region Constructors

        public StockController(IStock_Service stockService, ILogging_Service logger)
        {
            _stockService = stockService;
            _logger = logger;
        }
        #endregion

        #region Methods

        #region Price Codes

        [HttpPost("list/price/codes")]
        public async Task<IActionResult> List_Price_Codes_Async()
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

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_PriceCode_List>> result;

            try
            {
                result = await _stockService.List_Price_Codes();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_PriceCode_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_PriceCode_List>(
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

        [HttpPost("add/price/code")]
        public async Task<IActionResult> Add_Price_Code_Async([FromBody] Req_PriceCode_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_PriceCode_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Add_Price_Code(request);
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

        [HttpPost("update/price/code")]
        public async Task<IActionResult> Update_Price_Code_Async([FromBody] Req_PriceCode_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_PriceCode_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Update_Price_Code(request);
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

        #region Debtor Product

        [HttpPost("list/debtor/products")]
        public async Task<IActionResult> List_Debtor_Products_Async([FromBody] Req_DebtorProduct_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorProduct_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_DebtorProduct_List>> result;

            try
            {
                result = await _stockService.List_Debtor_Products(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_DebtorProduct_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_DebtorProduct_List>(
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

        [HttpPost("add/debtor/product")]
        public async Task<IActionResult> Add_Debtor_Product_Async([FromBody] Req_DebtorProduct_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorProduct_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Add_Debtor_Product(request);
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

        [HttpPost("update/debtor/product")]
        public async Task<IActionResult> Update_Debtor_Product_Async([FromBody] Req_DebtorProduct_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorProduct_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Update_Debtor_Product(request);
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

        #region Debtor Product Prices

        [HttpPost("list/debtor/product/prices")]
        public async Task<IActionResult> List_Debtor_Product_Prices_Async([FromBody] Req_DebtorProductPrice_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorProductPrice_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_DebtorProductPrice_List>> result;

            try
            {
                result = await _stockService.List_Debtor_Product_Prices(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_DebtorProductPrice_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_DebtorProductPrice_List>(
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

        [HttpPost("add/debtor/product/price")]
        public async Task<IActionResult> Add_Debtor_Product_Price_Async([FromBody] Req_DebtorProductPrice_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorProductPrice_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Add_Debtor_Product_Price(request);
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

        [HttpPost("update/debtor/product/price")]
        public async Task<IActionResult> Update_Debtor_Product_Price_Async([FromBody] Req_DebtorProductPrice_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorProductPrice_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Update_Debtor_Product_Price(request);
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

        #region Cost Center Product

        [HttpPost("list/cost/center/products")]
        public async Task<IActionResult> List_Cost_Center_Products_Async([FromBody] Req_CostCenterProduct_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenterProduct_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_CostCenterProduct_List>> result;

            try
            {
                result = await _stockService.List_Cost_Center_Products(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CostCenterProduct_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CostCenterProduct_List>(
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

        [HttpPost("add/cost/center/product")]
        public async Task<IActionResult> Add_Cost_Center_Product_Async([FromBody] Req_CostCenterProduct_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenterProduct_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Add_Cost_Center_Product(request);
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

        [HttpPost("update/cost/center/product")]
        public async Task<IActionResult> Update_Cost_Center_Product_Async([FromBody] Req_CostCenterProduct_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenterProduct_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Update_Cost_Center_Product(request);
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

        #region Supplier Product

        [HttpPost("list/supplier/products")]
        public async Task<IActionResult> List_Supplier_Products_Async([FromBody] Req_SupplierProduct_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_SupplierProduct_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_SupplierProduct_List>> result;

            try
            {
                result = await _stockService.List_Supplier_Products(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_SupplierProduct_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_SupplierProduct_List>(
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

        [HttpPost("add/supplier/product")]
        public async Task<IActionResult> Add_Supplier_Product_Async([FromBody] Req_SupplierProduct_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_SupplierProduct_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Add_Supplier_Product(request);
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

        [HttpPost("update/supplier/product")]
        public async Task<IActionResult> Update_Supplier_Product_Async([FromBody] Req_SupplierProduct_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_SupplierProduct_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _stockService.Update_Supplier_Product(request);
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

        #endregion

        //#region PurchaseOrders

        //[HttpPost("list/purchase/orders")]
        //public async Task<IActionResult> List_Purchase_Orders_Async([FromBody] Req_PurchaseOrder_List request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_PurchaseOrder_List>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

        //    ApiResponse<List<Res_PurchaseOrder_List>> result;

        //    try
        //    {
        //        result = await _stockService.List_Purchase_Orders(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<List<Res_PurchaseOrder_List>>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

        //        return StatusCode(500, ApiResponse.Fail<Res_PurchaseOrder_List>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("add/purchase/order")]
        //public async Task<IActionResult> Add_Purchase_Order_Async([FromBody] Req_PurchaseOrder_Add request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_PurchaseOrder_Add>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Add_Purchase_Order(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("update/purchase/order")]
        //public async Task<IActionResult> Update_Purchase_Order_Async([FromBody] Req_PurchaseOrder_Update request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_PurchaseOrder_Update>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Update_Purchase_Order(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}
        //#endregion

        //#region Purchase Order Lines

        //[HttpPost("list/purchase/order/lines")]
        //public async Task<IActionResult> List_Purchase_Order_Lines_Async([FromBody] Req_PurchaseOrderLine_List request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_PurchaseOrderLine_List>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

        //    ApiResponse<List<Res_PurchaseOrderLine_List>> result;

        //    try
        //    {
        //        result = await _stockService.List_Purchase_Order_Lines(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<List<Res_PurchaseOrderLine_List>>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

        //        return StatusCode(500, ApiResponse.Fail<Res_PurchaseOrderLine_List>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("add/purchase/order/line")]
        //public async Task<IActionResult> Add_Purchase_Order_Line_Async([FromBody] Req_PurchaseOrderLine_Add request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_PurchaseOrderLine_Add>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Add_Purchase_Order_Line(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}
        //#endregion

        //#region Purchase Order Manager Review

        //[HttpPost("list/submitted/purchase/orders")]
        //public async Task<IActionResult> List_Submitted_Purchase_Orders_Async()
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<object>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

        //    ApiResponse<List<Res_SubmittedPurchaseOrder_List>> result;

        //    try
        //    {
        //        result = await _stockService.List_Submitted_Purchase_Orders();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<List<Res_SubmittedPurchaseOrder_List>>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

        //        return StatusCode(500, ApiResponse.Fail<Res_SubmittedPurchaseOrder_List>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("update/submitted/purchase/order/status")]
        //public async Task<IActionResult> Update_Purchase_Order_Status_Async([FromBody] Req_PurchaseOrderStatus_Update request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_PurchaseOrderStatus_Update>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Update_Purchase_Order_Status(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}
        //#endregion

        //#region Purchase Order Lines Manager Review

        //[HttpPost("list/submitted/purchase/order/lines")]
        //public async Task<IActionResult> List_Submitted_Purchase_Order_Lines_Async([FromBody] Req_SubmittedPurchaseOrderLines_List request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_SubmittedPurchaseOrderLines_List>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

        //    ApiResponse<List<Res_SubmittedPurchaseOrderLines_List>> result;

        //    try
        //    {
        //        result = await _stockService.List_Submitted_Purchase_Order_Lines(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<List<Res_SubmittedPurchaseOrderLines_List>>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

        //        return StatusCode(500, ApiResponse.Fail<Res_SubmittedPurchaseOrderLines_List>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("update/submitted/purchase/order/line/status")]
        //public async Task<IActionResult> Update_Purchase_Order_Line_Status_Async([FromBody] Req_PurchaseOrderLineStatus_Update request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_PurchaseOrderLineStatus_Update>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Update_Purchase_Order_Line_Status(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}
        //#endregion

        //#region Stock Request

        //[HttpPost("list/stock/requests")]
        //public async Task<IActionResult> List_Stock_Requests_Async([FromBody] Req_StockRequest_List request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_StockRequest_List>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

        //    ApiResponse<List<Res_StockRequest_List>> result;

        //    try
        //    {
        //        result = await _stockService.List_Stock_Requests(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<List<Res_StockRequest_List>>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

        //        return StatusCode(500, ApiResponse.Fail<Res_StockRequest_List>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("add/stock/request")]
        //public async Task<IActionResult> Add_Stock_Request_Async([FromBody] Req_StockRequest_Add request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_StockRequest_Add>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Add_Stock_Request(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("update/stock/request")]
        //public async Task<IActionResult> Update_Stock_Request_Async([FromBody] Req_StockRequest_Update request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_StockRequest_Update>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Update_Stock_Request(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}
        //#endregion

        //#region Stock Request Lines

        //[HttpPost("list/stock/request/lines")]
        //public async Task<IActionResult> List_Stock_Request_Lines_Async([FromBody] Req_StockRequestLine_List request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_StockRequestLine_List>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

        //    ApiResponse<List<Res_StockRequestLine_List>> result;

        //    try
        //    {
        //        result = await _stockService.List_Stock_Request_Lines(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<List<Res_StockRequestLine_List>>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

        //        return StatusCode(500, ApiResponse.Fail<Res_StockRequestLine_List>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("add/stock/request/line")]
        //public async Task<IActionResult> Add_Stock_Request_Line_Async([FromBody] Req_StockRequestLine_Add request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_StockRequestLine_Add>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Add_Stock_Request_Line(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}
        //#endregion

        //#region Stock Transfer

        //[HttpPost("list/stock/tranfers")]
        //public async Task<IActionResult> List_Stock_Transfers_Async([FromBody] Req_StockTransfer_List request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_StockTransfer_List>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

        //    ApiResponse<List<Res_StockTransfer_List>> result;

        //    try
        //    {
        //        result = await _stockService.List_Stock_Transfers(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<List<Res_StockTransfer_List>>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

        //        return StatusCode(500, ApiResponse.Fail<Res_StockTransfer_List>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("add/stock/tranfer")]
        //public async Task<IActionResult> Add_Stock_Transfer_Async([FromBody] Req_StockTransfer_Add request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_StockTransfer_Add>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Add_Stock_Transfer(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}

        //[HttpPost("update/stock/tranfer")]
        //public async Task<IActionResult> Update_Stock_Transfer_Async([FromBody] Req_StockTransfer_Update request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_StockTransfer_Update>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    // ✅ Log Incoming Request
        //    _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

        //    ApiResponse<object> result;

        //    try
        //    {
        //        result = await _stockService.Update_Stock_Transfer(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<object>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
        //            AppErrorCode.UnknownError,
        //            new List<string> { "Authentication response was null" },
        //            500
        //        ));
        //    }

        //    if (!result.Success)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication failed", result));
        //        return StatusCode(result.StatusCode ?? 400, result);
        //    }

        //    _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Authentication succeeded", result.Data));
        //    return Ok(result);
        //}
        //#endregion

        //#region Stock Transfer Lines
        //#endregion

        //#region Stock Receive
        //#endregion

        //#region Stock Adjustment
        //#endregion
    }
}
