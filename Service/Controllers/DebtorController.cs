using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_Api.ServiceInterfaces.Debtors;
using POS_Api.ServiceInterfaces.EntityData;
using POS_Api.ServiceInterfaces.Logging;
using POS_Api.Services;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.Models.Debtors.POS_CostCenterPrinters;
using POS_Common.ModelsDto.DebtorsController;
using POS_Common.ModelsDto.DebtorsController.Branch;
using POS_Common.ModelsDto.DebtorsController.CostCenter;
using POS_Common.ModelsDto.DebtorsController.CostCenterPrinter;
using POS_Common.ModelsDto.DebtorsController.CostCenterType;
using POS_Common.ModelsDto.DebtorsController.DebtorAddress;
using POS_Common.ModelsDto.DebtorsController.DebtorContact;
using POS_Common.ModelsDto.DebtorsController.Department;
using POS_Common.ModelsDto.EntityDataController;
using POS_Common.ModelsDto.EntityDataController.Address;

namespace POS_Webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class DebtorController : ControllerBase
    {
        #region Members

        private readonly IDebtors_Service _debtorsService;
        private readonly ILogging_Service _logger;
        #endregion

        #region Constructors

        public DebtorController(IDebtors_Service debtorsService, ILogging_Service logger)
        {
            _debtorsService = debtorsService;
            _logger = logger;
        }
        #endregion

        #region Methods

        #region Debtors

        [HttpGet("list/debtors")]
        public async Task<IActionResult> List_Debtors_Async()
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

            ApiResponse<List<Res_Debtor_List>> result;

            try
            {
                result = await _debtorsService.List_Debtors();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Debtor_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Debtor_List>(
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

        [HttpPost("add/debtor")]
        public async Task<IActionResult> Add_Debtor_Async([FromBody] Req_Debtor_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Debtor_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Add_Debtor(request);
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

        [HttpPost("update/debtor")]
        public async Task<IActionResult> Update_Debtor_Async([FromBody] Req_Debtor_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Debtor_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Update_Debtor(request);
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

        #region Debtor Addresses

        [HttpPost("add/debtor/address")]
        public async Task<IActionResult> Add_Debtor_Address_Async([FromBody] Req_DebtorAddress_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorAddress_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Add_Debtor_Address(request);
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

        [HttpPost("update/debtor/address")]
        public async Task<IActionResult> Update_Debtor_Address_Async([FromBody] Req_DebtorAddress_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorAddress_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Update_Debtor_Address(request);
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

        [HttpPost("list/address/types")]
        public async Task<IActionResult> List_Debtor_Address_Types_Async()
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

            ApiResponse<List<Res_DebtorAddressType_List>> result;

            try
            {
                result = await _debtorsService.List_Debtor_Address_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_DebtorAddressType_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_DebtorAddressType_List>(
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

        #region Debtor Contacts

        [HttpPost("add/debtor/contact")]
        public async Task<IActionResult> Add_Debtor_Contact_Async([FromBody] Req_DebtorContact_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorContact_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Add_Debtor_Contact(request);
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

        [HttpPost("update/debtor/contact")]
        public async Task<IActionResult> Update_Debtor_Contact_Async([FromBody] Req_DebtorContact_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DebtorContact_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Update_Debtor_Contact(request);
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

        #region Cost Centers

        [HttpGet("list/cost/center")]
        public async Task<IActionResult> List_CostCenters_Async()
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

            ApiResponse<List<Res_CostCenter_List>> result;

            try
            {
                result = await _debtorsService.List_CostCenters();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CostCenter_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CostCenter_List>(
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

        [HttpPost("add/cost/center")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add_CostCenter_Async([FromForm] Req_CostCenter_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenter_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Add_CostCenter(request);
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

        [HttpPost("update/cost/center")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update_CostCenter_Async([FromForm] Req_CostCenter_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenter_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Update_CostCenter(request);
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

        #region Cost Center Types

        [HttpGet("list/cost/center/types")]
        public async Task<IActionResult> List_CostCenterTypes_Async()
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

            ApiResponse<List<Res_CostCenterType_List>> result;

            try
            {
                result = await _debtorsService.List_CostCenterTypes();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CostCenterType_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CostCenterType_List>(
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

        [HttpPost("list/cost/center/printers")]
        public async Task<IActionResult> List_CostCenter_Printers_Async([FromBody] Req_CostCenterPrinter_List request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenterPrinter_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<List<Res_CostCenterPrinter_List>> result;

            try
            {
                result = await _debtorsService.List_CostCenter_Printers(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CostCenterPrinter_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_CostCenterPrinter_List>(
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

        [HttpPost("add/cost/center/printer")]
        public async Task<IActionResult> Add_CostCenter_Printer_Async([FromBody] Req_CostCenterPrinter_Add request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenterPrinter_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Add_CostCenter_Printer(request);
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
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Add failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Add succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("update/cost/center/printer")]
        public async Task<IActionResult> Update_CostCenter_Printer_Async([FromBody] Req_CostCenterPrinter_Update request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenterPrinter_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _debtorsService.Update_CostCenter_Printer(request);
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
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<object>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Update failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Update succeeded", result.Data));
            return Ok(result);
        }

        [HttpPost("cost/center/printer/toggle")]
        public async Task<IActionResult> Switch_CostCenter_Printer_Link_Async([FromBody] Req_CostCenterPrinter_Switch request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CostCenterPrinter_Switch>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<CostCenterPrinter> result;

            try
            {
                result = await _debtorsService.Switch_CostCenter_Printer_Link(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<CostCenterPrinter>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<CostCenterPrinter>(
                    AppErrorCode.UnknownError,
                    new List<string> { "response was null" },
                    500
                ));
            }

            if (!result.Success)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Switch link failed", result));
                return StatusCode(result.StatusCode ?? 400, result);
            }

            _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Switch link succeeded", result.Data));
            return Ok(result);
        }
        #endregion

        #endregion
    }
}
