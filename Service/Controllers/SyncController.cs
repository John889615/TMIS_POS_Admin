using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_Api.ServiceInterfaces.Logging;
using POS_Api.ServiceInterfaces.Menu;
using POS_Api.ServiceInterfaces.Sync;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.Models.Sync;
using POS_Common.ModelsDto.EntityDataController.SlipPrinter;
using POS_Common.ModelsDto.StockController.PurchaseOrder;
using POS_Common.ModelsDto.SyncController;
using POS_Common.ModelsDto.SyncController.FromServer;

namespace POS_Webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SyncController : ControllerBase
    {
        #region Members

        private readonly ISync_Service _syncService;
        private readonly ILogging_Service _logger;
        #endregion

        #region Constructors

        public SyncController(ISync_Service syncService, ILogging_Service logger)
        {
            _syncService = syncService;
            _logger = logger;
        }
        #endregion

        #region Methods

        #region From Server

        #region Locations

        [HttpGet("list/locations")]
        public async Task<IActionResult> List_Locations_Async()
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

            ApiResponse<List<Res_Location_Sync>> result;

            try
            {
                result = await _syncService.List_Locations();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Location_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Location_Sync>(
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

        #region Cost Centers

        [HttpGet("list/cost/centers")]
        public async Task<IActionResult> List_Cost_Centers_Async()
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

            ApiResponse<List<Res_CostCenter_Sync>> result;

            try
            {
                result = await _syncService.List_Cost_Centers();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CostCenter_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CostCenter_Sync>(
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

        #region Slip Printers

        [HttpGet("list/slip/printers")]
        public async Task<IActionResult> List_Slip_Printers_Async()
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

            ApiResponse<List<Res_SlipPrinter_Sync>> result;

            try
            {
                result = await _syncService.List_Slip_Printers();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_SlipPrinter_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_SlipPrinter_Sync>(
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

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_Unit_Sync>> result;

            try
            {
                result = await _syncService.List_Units();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Unit_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Unit_Sync>(
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

        #region Tax Types

        [HttpGet("list/tax/types")]
        public async Task<IActionResult> List_Tax_Types_Async()
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

            ApiResponse<List<Res_TaxType_Sync>> result;

            try
            {
                result = await _syncService.List_Tax_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_TaxType_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_TaxType_Sync>(
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

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_Product_Sync>> result;

            try
            {
                result = await _syncService.List_Products();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Product_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Product_Sync>(
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

        #region Location Products

        [HttpGet("list/location/products")]
        public async Task<IActionResult> List_Location_Products_Async()
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

            ApiResponse<List<Res_LocationProduct_Sync>> result;

            try
            {
                result = await _syncService.List_Location_Products();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_LocationProduct_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_LocationProduct_Sync>(
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

        #region Price Codes

        [HttpGet("list/price/codes")]
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

            ApiResponse<List<Res_PriceCode_Sync>> result;

            try
            {
                result = await _syncService.List_Price_Codes();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_PriceCode_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_PriceCode_Sync>(
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

        #region Debtor Product Prices

        [HttpGet("list/debtor/product/prices")]
        public async Task<IActionResult> List_Debtor_Product_Prices_Async()
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

            ApiResponse<List<Res_DebtorProductPrices_Sync>> result;

            try
            {
                result = await _syncService.List_Debtor_Product_Prices();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_DebtorProductPrices_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_DebtorProductPrices_Sync>(
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

        #region Cost Centers Products

        [HttpGet("list/cost/center/products")]
        public async Task<IActionResult> List_Cost_Center_Products_Async()
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

            ApiResponse<List<Res_CostCenterProduct_Sync>> result;

            try
            {
                result = await _syncService.List_Cost_Center_Products();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CostCenterProduct_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CostCenterProduct_Sync>(
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

        #region Booking Headers

        [HttpGet("list/bookings")]
        public async Task<IActionResult> List_Booking_Headers_Async()
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

            ApiResponse<List<Res_BookingHeader_Sync>> result;

            try
            {
                result = await _syncService.List_Booking_Headers();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_BookingHeader_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_BookingHeader_Sync>(
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

        #region Guests

        [HttpGet("list/guests")]
        public async Task<IActionResult> List_Guests_Async()
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

            ApiResponse<List<Res_Guest_Sync>> result;

            try
            {
                result = await _syncService.List_Guests();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Guest_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Guest_Sync>(
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

        #region Booking Guests

        [HttpGet("list/booking/guests")]
        public async Task<IActionResult> List_Booking_Guests_Async()
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

            ApiResponse<List<Res_BookingGuest_Sync>> result;

            try
            {
                result = await _syncService.List_Booking_Guests();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_BookingGuest_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_BookingGuest_Sync>(
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

        #region Payment Types

        [HttpGet("list/payment/types")]
        public async Task<IActionResult> List_Payment_Types_Async()
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

            ApiResponse<List<Res_PaymentType_Sync>> result;

            try
            {
                result = await _syncService.List_Payment_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_PaymentType_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_PaymentType_Sync>(
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

        [HttpGet("list/payment/type/icons")]
        public async Task<IActionResult> List_Payment_Type_Icons_Async()
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

            ApiResponse<List<Res_PaymentTypeIcon_Sync>> result;

            try
            {
                result = await _syncService.List_Payment_Type_Icons();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_PaymentTypeIcon_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_PaymentTypeIcon_Sync>(
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

        #region Menus

        [HttpGet("list/menus")]
        public async Task<IActionResult> List_Menus_Async()
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

            ApiResponse<List<Res_Menu_Sync>> result;

            try
            {
                result = await _syncService.List_Menus();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Menu_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Menu_Sync>(
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

        [HttpGet("list/menu/printers")]
        public async Task<IActionResult> List_Menu_Printers_Async()
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

            ApiResponse<List<Res_MenuPrinter_Sync>> result;

            try
            {
                result = await _syncService.List_Menu_Printers();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_MenuPrinter_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_MenuPrinter_Sync>(
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

        [HttpGet("list/menu/items")]
        public async Task<IActionResult> List_Menu_Items_Async()
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

            ApiResponse<List<Res_MenuItem_Sync>> result;

            try
            {
                result = await _syncService.List_Menu_Items();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_MenuItem_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_MenuItem_Sync>(
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

        [HttpGet("list/menu/item/products")]
        public async Task<IActionResult> List_Menu_Item_Products_Async()
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

            ApiResponse<List<Res_MenuItemProduct_Sync>> result;

            try
            {
                result = await _syncService.List_Menu_Item_Products();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_MenuItemProduct_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_MenuItemProduct_Sync>(
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

        [HttpGet("list/menu/item/product/printers")]
        public async Task<IActionResult> List_Menu_Item_Product_Printers_Async()
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

            ApiResponse<List<Res_MenuItemProductPrinter_Sync>> result;

            try
            {
                result = await _syncService.List_Menu_Item_Product_Printers();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_MenuItemProductPrinter_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_MenuItemProductPrinter_Sync>(
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

        #region Product Combinations

        [HttpGet("list/product/combinations")]
        public async Task<IActionResult> List_Product_Combinations_Async()
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

            ApiResponse<List<Res_ProductCombination_Sync>> result;

            try
            {
                result = await _syncService.List_Product_Combinations();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductCombination_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductCombination_Sync>(
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

        #region Product Extras

        [HttpGet("list/product/extras")]
        public async Task<IActionResult> List_Product_Extras_Async()
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

            ApiResponse<List<Res_ProductExtra_Sync>> result;

            try
            {
                result = await _syncService.List_Product_Extras();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductExtra_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductExtra_Sync>(
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

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_ProductExtraCategory_Sync>> result;

            try
            {
                result = await _syncService.List_Product_Extra_Categories();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductExtraCategory_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductExtraCategory_Sync>(
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

        #region Product Preparation

        [HttpGet("list/product/preparation")]
        public async Task<IActionResult> List_Product_Preparation_Async()
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

            ApiResponse<List<Res_ProductPreparation_Sync>> result;

            try
            {
                result = await _syncService.List_Product_Preparation();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductPreparation_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductPreparation_Sync>(
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

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_ProductPreparationMethod_Sync>> result;

            try
            {
                result = await _syncService.List_Product_Preparation_Methods();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductPreparationMethod_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductPreparationMethod_Sync>(
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

        #region Product Substitutions

        [HttpGet("list/product/subtitutions")]
        public async Task<IActionResult> List_Product_Substitutions_Async()
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

            ApiResponse<List<Res_ProductSubstitution_Sync>> result;

            try
            {
                result = await _syncService.List_Product_Substitutions();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ProductSubstitution_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ProductSubstitution_Sync>(
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

        #region Images

        [HttpGet("list/images")]
        public async Task<IActionResult> List_Images_Async()
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

            ApiResponse<List<Res_Image_Sync>> result;

            try
            {
                result = await _syncService.List_Images();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Image_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Image_Sync>(
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

        [HttpGet("image/{id:int}")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 404)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        public async Task<IActionResult> Get_Image_Bytes_Async(int id)
        {
            // ✅ Validate Request
            if (id <= 0)
            {
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", new List<string> { "id must be a positive integer" }));

                return BadRequest(ApiResponse.Fail<object>(
                    AppErrorCode.ValidationError,
                    new List<string> { "id must be a positive integer" }
                ));
            }

            ApiResponse<ImageBytesResult> result;

            try
            {
                result = await _syncService.Get_Image_Bytes(id, HttpContext.RequestAborted);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<ImageBytesResult>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<ImageBytesResult>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Get_Image_Bytes failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Get bytes succeeded", new { id, size = result.Data.Bytes.Length }));

            Response.Headers["Last-Modified"] = result.Data.LastModified.ToString("R");
            return File(result.Data.Bytes, result.Data.ContentType);
        }

        [HttpGet("list/image/categories")]
        public async Task<IActionResult> List_Image_Categories_Async()
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

            ApiResponse<List<Res_ImageCategory_Sync>> result;

            try
            {
                result = await _syncService.List_Image_Categories();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ImageCategory_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ImageCategory_Sync>(
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

        #region Settings

        [HttpGet("list/settings")]
        public async Task<IActionResult> List_Settings_Async()
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

            ApiResponse<List<Res_Settings_Sync>> result;

            try
            {
                result = await _syncService.List_Settings();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Settings_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Settings_Sync>(
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

        [HttpGet("list/global/settings")]
        public async Task<IActionResult> List_Global_Settings_Async()
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

            ApiResponse<List<Res_GlobalSettings_Sync>> result;

            try
            {
                result = await _syncService.List_Global_Settings();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_GlobalSettings_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_GlobalSettings_Sync>(
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

        #region Served As

        [HttpGet("list/served/as")]
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

            ApiResponse<List<Res_ServedAs_Sync>> result;

            try
            {
                result = await _syncService.List_Served_As();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ServedAs_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ServedAs_Sync>(
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

        [HttpGet("list/served/as/products")]
        public async Task<IActionResult> List_Served_As_Products_Async()
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

            ApiResponse<List<Res_ServedAsProducts_Sync>> result;

            try
            {
                result = await _syncService.List_Served_As_Products();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ServedAsProducts_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_ServedAsProducts_Sync>(
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

        #region Currencies

        [HttpGet("list/currencies")]
        public async Task<IActionResult> List_Currencies_Async()
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

            ApiResponse<List<Res_Currency_Sync>> result;

            try
            {
                result = await _syncService.List_Currencies();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Currency_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Currency_Sync>(
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

        [HttpGet("list/exchange/rates")]
        public async Task<IActionResult> List_Exchange_Rates_Async()
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

            ApiResponse<List<Res_CurrencyExchangeRate_Sync>> result;

            try
            {
                result = await _syncService.List_Currency_Exchange_Rates();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CurrencyExchangeRate_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CurrencyExchangeRate_Sync>(
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

        [HttpGet("list/location/currencies")]
        public async Task<IActionResult> List_Location_Currencies_Async()
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

            ApiResponse<List<Res_LocationCurrency_Sync>> result;

            try
            {
                result = await _syncService.List_Location_Currencies();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_LocationCurrency_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_LocationCurrency_Sync>(
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

        #region Cost Center Printers

        [HttpGet("list/cost/center/printers")]
        public async Task<IActionResult> List_Cost_Center_Printers_Async()
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

            ApiResponse<List<Res_CostCenterPrinter_Sync>> result;

            try
            {
                result = await _syncService.List_Cost_Center_Printers();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CostCenterPrinter_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CostCenterPrinter_Sync>(
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

        #region Slip Types

        [HttpGet("list/sliptypes")]
        public async Task<IActionResult> List_Slip_Types_Async()
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

            ApiResponse<List<Res_SlipTypes_Sync>> result;

            try
            {
                result = await _syncService.List_Slip_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_SlipTypes_Sync>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CostCenterPrinter_Sync>(
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

        #endregion

        #region To Server

        //[HttpPost("sync/invoice/headers")]
        //public async Task<IActionResult> List_Invoice_Headers_Async([FromBody] Req_InvoiceHeader_Sync request)
        //{
        //    // ✅ Validate Request
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values
        //            .SelectMany(v => v.Errors)
        //            .Select(e => e.ErrorMessage)
        //            .ToList();

        //        _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

        //        return BadRequest(ApiResponse.Fail<Req_InvoiceHeader_Sync>(
        //            AppErrorCode.ValidationError,
        //            errors
        //        ));
        //    }

        //    ApiResponse<bool> result;

        //    try
        //    {
        //        result = await _syncService.List_Invoice_Headers(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

        //        result = ApiResponse.Fail<bool>(
        //            AppErrorCode.ServerError,
        //            new List<string> { ex.Message },
        //            500
        //        );
        //    }

        //    if (result == null)
        //    {
        //        _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

        //        return StatusCode(500, ApiResponse.Fail<object>(
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

        [HttpPost("sync/bookings")]
        public async Task<IActionResult> List_Booking_Headers_Async([FromBody] Req_BookingHeader_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_BookingHeader_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;

            try
            {
                result = await _syncService.List_Booking_Headers(request.Data);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/guests")]
        public async Task<IActionResult> List_Guests_Async([FromBody] Req_Guest_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_Guest_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Guests(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/booking/guests")]
        public async Task<IActionResult> List_Booking_Guests_Async([FromBody] Req_BookingGuest_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_BookingGuest_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Booking_Guests(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/accounts")]
        public async Task<IActionResult> List_Accounts_Async([FromBody] Req_Account_Sync request) 
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_Account_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Accounts(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/account/guests")]
        public async Task<IActionResult> List_Account_Guests_Async([FromBody] Req_AccountGuest_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_AccountGuest_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Account_Guests(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/arrivals")]
        public async Task<IActionResult> List_Arrivals_Async([FromBody] Req_Arrival_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_Arrival_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Arrivals(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/cashup/headers")]
        public async Task<IActionResult> List_CashUp_Headers_Async([FromBody] Req_CashUpHeader_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_CashUpHeader_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_CashUp_Headers(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/cashup/lines")]
        public async Task<IActionResult> List_CashUp_Lines_Async([FromBody] Req_CashUpLine_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_CashUpLine_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_CashUp_Lines(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/tabs")]
        public async Task<IActionResult> List_Tabs_Async([FromBody] Req_Tab_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_Tab_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Tabs(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/tab/lines")]
        public async Task<IActionResult> List_Tab_Lines_Async([FromBody] Req_TabLine_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_TabLine_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Tab_Lines(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/tab/line/combinations")]
        public async Task<IActionResult> List_TabLine_Combinations_Async([FromBody] Req_TabLineCombination_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_TabLineCombination_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_TabLine_Combinations(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/tab/line/extras")]
        public async Task<IActionResult> List_TabLine_Extras_Async([FromBody] Req_TabLineExtra_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_TabLineExtra_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_TabLine_Extras(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/tab/line/guests")]
        public async Task<IActionResult> List_TabLine_Guests_Async([FromBody] Req_TabLineGuest_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_TabLineGuest_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_TabLine_Guests(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/tab/line/preparation/methods")]
        public async Task<IActionResult> List_TabLine_Preparation_Methods_Async([FromBody] Req_TabLinePreparationMethod_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_TabLinePreparationMethod_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_TabLine_Preparation_Methods(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/tab/line/substitutes")]
        public async Task<IActionResult> List_Tabline_Substitutes_Async([FromBody] Req_TablineSubstitute_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_TablineSubstitute_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Tabline_Substitutes(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/invoice/headers")]
        public async Task<IActionResult> List_Invoice_Headers_Async([FromBody] Req_InvoiceHeader_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_InvoiceHeader_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Invoice_Headers(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/invoice/tabs")]
        public async Task<IActionResult> List_Invoice_Tabs_Async([FromBody] Req_InvoiceTab_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_InvoiceTab_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Invoice_Tabs(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/invoice/lines")]
        public async Task<IActionResult> List_Invoice_Lines_Async([FromBody] Req_InvoiceLine_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_InvoiceLine_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Invoice_Lines(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/invoice/payments")]
        public async Task<IActionResult> List_Invoice_Payments_Async([FromBody] Req_InvoicePayment_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_InvoicePayment_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Invoice_Payments(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "List succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("sync/void/logs")]
        public async Task<IActionResult> List_Void_Logs_Async([FromBody] Req_VoidLog_Sync request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));
                return BadRequest(ApiResponse.Fail<Req_VoidLog_Sync>(AppErrorCode.ValidationError, errors));
            }

            ApiResponse<bool> result;
            try { result = await _syncService.List_Void_Logs(request.Data); }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));
                result = ApiResponse.Fail<bool>(AppErrorCode.ServerError, new List<string> { ex.Message }, 500);
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
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

        #region Admin

        [HttpPost("notify-result")]
        public async Task<IActionResult> Notify_Result_Async([FromBody] Req_Notify_Result request)
        {
            var ctx = request == null ? "null body" : $"siteId={request.SiteId}, typeName={request.TypeName}, status={request.Status}";

            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, $"Notify_Result: validation failed ({ctx})", errors));

                return BadRequest(ApiResponse.Fail<object>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<bool> result;

            try
            {
                result = await _syncService.Notify_Result(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, $"Notify_Result: unhandled exception ({ctx})", ex));

                result = ApiResponse.Fail<bool>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, $"Notify_Result: null response ({ctx})"));
                return StatusCode(500, ApiResponse.Fail<object>(AppErrorCode.UnknownError, new List<string> { "response was null" }, 500));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, $"Notify_Result: failed ({ctx})", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, $"Notify_Result: succeeded ({ctx})", result.Data));
            return Ok(result);
        }

        #endregion

        #endregion
    }
}
