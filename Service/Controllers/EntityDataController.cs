using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_Api.ServiceInterfaces.EntityData;
using POS_Api.ServiceInterfaces.Logging;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.ModelsDto.EntityDataController;
using POS_Common.ModelsDto.EntityDataController.Address;
using POS_Common.ModelsDto.EntityDataController.Contact;
using POS_Common.ModelsDto.EntityDataController.Country;
using POS_Common.ModelsDto.EntityDataController.Currency;
using POS_Common.ModelsDto.EntityDataController.DialingCode;
using POS_Common.ModelsDto.EntityDataController.Entity;
using POS_Common.ModelsDto.EntityDataController.ExchangeRates;
using POS_Common.ModelsDto.EntityDataController.LocationCurrency;
using POS_Common.ModelsDto.EntityDataController.PaymentType;
using POS_Common.ModelsDto.EntityDataController.Settings;
using POS_Common.ModelsDto.EntityDataController.SlipPrinter;
using POS_Common.ModelsDto.EntityDataController.SlipType;
using POS_Common.ModelsDto.EntityDataController.TaxType;
using POS_Common.ModelsDto.EntityDataController.Timezone;
using TMIS_Common.ModelsDto.AuthController.Authenticate;
using TMIS_Common.ModelsDto.AuthController.PasswordReset;
using TMIS_Common.ModelsDto.AuthController.RefreshToken;

namespace POS_Webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EntityDataController : ControllerBase
    {
        #region Members

        private readonly IEntityData_Service _entityDataService;
        private readonly ILogging_Service _logger;
        #endregion

        #region Constructors

        public EntityDataController(IEntityData_Service entityDataService, ILogging_Service logger)
        {
            _entityDataService = entityDataService;
            _logger = logger;
        }
        #endregion

        #region Methods

        #region All Entity Addresses

        [HttpPost("list/all/entity/addresses")]
        public async Task<IActionResult> List_All_Entity_Addresses_Async([FromBody] Req_AllEntityAddress_List request)
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

            ApiResponse<List<Res_AllEntityAddress_List>> result;

            try
            {
                result = await _entityDataService.List_All_Entity_Addresses(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_AllEntityAddress_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_AllEntityAddress_List>(
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

        #region Addresses

        [HttpGet("list/addresses")]
        public async Task<IActionResult> List_Addresses_Async()
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

            ApiResponse<List<Res_Address_List>> result;

            try
            {
                result = await _entityDataService.List_Addresses();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Address_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_Address_List>(
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

        [HttpPost("add/address")]
        public async Task<IActionResult> Add_Address_Async([FromBody] Req_Address_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Address_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Address(request);
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

        [HttpPost("update/address")]
        public async Task<IActionResult> Update_Address_Async([FromBody] Req_Address_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Address_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Address(request);
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

        #region Address Regions

        [HttpGet("list/address/regions")]
        public async Task<IActionResult> List_Address_Regions_Async()
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

            ApiResponse<List<Res_AddressRegion_List>> result;

            try
            {
                result = await _entityDataService.List_Address_Regions();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_AddressRegion_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_AddressRegion_List>(
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

        [HttpPost("add/address/region")]
        public async Task<IActionResult> Add_Address_Region_Async([FromBody] Req_AddressRegion_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_AddressRegion_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Address_Region(request);
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

        [HttpPost("update/address/region")]
        public async Task<IActionResult> Update_Address_Region_Async([FromBody] Req_AddressRegion_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_AddressRegion_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Address_Region(request);
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

        #region Address Types

        [HttpGet("list/address/types")]
        public async Task<IActionResult> List_Address_Types_Async()
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

            ApiResponse<List<Res_AddressType_List>> result;

            try
            {
                result = await _entityDataService.List_Address_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_AddressType_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_AddressType_List>(
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

        [HttpPost("add/address/type")]
        public async Task<IActionResult> Add_Address_Type_Async([FromBody] Req_AddressType_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_AddressType_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Address_Type(request);
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

        [HttpPost("update/address/type")]
        public async Task<IActionResult> Update_Address_Type_Async([FromBody] Req_AddressType_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_AddressType_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Address_Type(request);
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

        #region All Entity Contacts

        [HttpPost("list/all/entity/contacts")]
        public async Task<IActionResult> List_All_Entity_Contacts_Async([FromBody] Req_AllEntityContact_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_AllEntityContact_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_AllEntityContact_List>> result;

            try
            {
                result = await _entityDataService.List_All_Entity_Contacts(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_AllEntityContact_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_AllEntityContact_List>(
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

        #region Contacts

        [HttpGet("list/contacts")]
        public async Task<IActionResult> List_Contacts_Async()
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

            ApiResponse<List<Res_Contact_List>> result;

            try
            {
                result = await _entityDataService.List_Contacts();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Contact_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_Contact_List>(
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

        [HttpPost("add/contact")]
        public async Task<IActionResult> Add_Contact_Async([FromBody] Req_Contact_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Contact_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Contact(request);
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

        [HttpPost("update/contact")]
        public async Task<IActionResult> Update_Contact_Async([FromBody] Req_Contact_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Contact_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Contact(request);
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

        #region Contact Types

        [HttpGet("list/contact/types")]
        public async Task<IActionResult> List_Contact_Types_Async()
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

            ApiResponse<List<Res_ContactType_List>> result;

            try
            {
                result = await _entityDataService.List_Contact_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ContactType_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_ContactType_List>(
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

        [HttpPost("add/contact/type")]
        public async Task<IActionResult> Add_Contact_Type_Async([FromBody] Req_ContactType_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ContactType_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Contact_Type(request);
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

        [HttpPost("update/contact/type")]
        public async Task<IActionResult> Update_Contact_Type_Async([FromBody] Req_ContactType_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ContactType_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Contact_Type(request);
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

        #region Continents

        [HttpGet("list/continents")]
        public async Task<IActionResult> List_Continents_Async()
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

            ApiResponse<List<Res_Continent_List>> result;

            try
            {
                result = await _entityDataService.List_Continents();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Continent_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_Continent_List>(
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

        #region Countries

        [HttpGet("list/countries")]
        public async Task<IActionResult> List_Countries_Async()
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

            ApiResponse<List<Res_Country_List>> result;

            try
            {
                result = await _entityDataService.List_Countries();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Country_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_Country_List>(
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

        [HttpPost("add/country")]
        public async Task<IActionResult> Add_Country_Async([FromBody] Req_Country_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Country_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Country(request);
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

        [HttpPost("update/country")]
        public async Task<IActionResult> Update_Country_Async([FromBody] Req_Country_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Country_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Country(request);
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

        #region Country Provinces

        [HttpGet("list/country/provinces")]
        public async Task<IActionResult> List_Country_Provinces_Async()
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

            ApiResponse<List<Res_CountryProvince_List>> result;

            try
            {
                result = await _entityDataService.List_Country_Provinces();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CountryProvince_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_CountryProvince_List>(
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

        [HttpPost("add/country/province")]
        public async Task<IActionResult> Add_Country_Province_Async([FromBody] Req_CountryProvince_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CountryProvince_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Country_Province(request);
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

        [HttpPost("update/country/province")]
        public async Task<IActionResult> Update_Country_Province_Async([FromBody] Req_CountryProvince_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CountryProvince_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Country_Province(request);
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

        #region Country Regions

        [HttpGet("list/country/regions")]
        public async Task<IActionResult> List_Country_Regions_Async()
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

            ApiResponse<List<Res_CountryRegion_List>> result;

            try
            {
                result = await _entityDataService.List_Country_Regions();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CountryRegion_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_CountryRegion_List>(
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

        [HttpPost("add/country/region")]
        public async Task<IActionResult> Add_Country_Region_Async([FromBody] Req_CountryRegion_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CountryRegion_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Country_Region(request);
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

        [HttpPost("update/country/region")]
        public async Task<IActionResult> Update_Country_Region_Async([FromBody] Req_CountryRegion_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CountryRegion_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Country_Region(request);
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

        #region Country Subregions

        [HttpGet("list/country/subregions")]
        public async Task<IActionResult> List_Country_Subregions_Async()
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

            ApiResponse<List<Res_CountrySubRegion_List>> result;

            try
            {
                result = await _entityDataService.List_Country_Subregions();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_CountrySubRegion_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_CountrySubRegion_List>(
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

        [HttpPost("add/country/subregion")]
        public async Task<IActionResult> Add_Country_Subregion_Async([FromBody] Req_CountrySubRegion_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CountrySubRegion_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Country_Subregion(request);
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

        [HttpPost("update/country/subregion")]
        public async Task<IActionResult> Update_Country_Subregion_Async([FromBody] Req_CountrySubRegion_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_CountryRegion_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Country_Subregion(request);
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

            ApiResponse<List<Res_Currency_List>> result;

            try
            {
                result = await _entityDataService.List_Currencies();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Currency_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_Currency_List>(
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

        [HttpPost("add/currency")]
        public async Task<IActionResult> Add_Currency_Async([FromBody] Req_Currency_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Currency_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Currency(request);
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

        [HttpPost("update/currency")]
        public async Task<IActionResult> Update_Currency_Async([FromBody] Req_Currency_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Currency_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Currency(request);
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

        #region Location Currencies

        [HttpPost("list/location/currencies")]
        public async Task<IActionResult> List_Location_Currencies_Async([FromBody] Req_LocationCurrency_List request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_LocationCurrency_List>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            ApiResponse<List<Res_LocationCurrency_List>> result;

            try
            {
                result = await _entityDataService.List_Location_Currencies(request);
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_LocationCurrency_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_LocationCurrency_List>(
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

        [HttpPost("add/location/currency")]
        public async Task<IActionResult> Add_Location_Currency_Async([FromBody] Req_LocationCurrency_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_LocationCurrency_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Location_Currency(request);
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

        [HttpPost("remove/location/currency")]
        public async Task<IActionResult> Remove_Location_Currency_Async([FromBody] Req_LocationCurrency_Remove request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_LocationCurrency_Remove>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Remove_Location_Currency(request);
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

        #region Dialing Codes

        [HttpGet("list/dialing/codes")]
        public async Task<IActionResult> List_Dialing_Codes_Async()
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

            ApiResponse<List<Res_DialingCode_List>> result;

            try
            {
                result = await _entityDataService.List_Dialing_Codes();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_DialingCode_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_DialingCode_List>(
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

        [HttpPost("add/dialing/code")]
        public async Task<IActionResult> Add_Dialing_Code_Async([FromBody] Req_DialingCode_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DialingCode_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Dialing_Code(request);
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

        [HttpPost("update/dialing/code")]
        public async Task<IActionResult> Update_Dialing_Code_Async([FromBody] Req_DialingCode_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_DialingCode_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Dialing_Code(request);
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

        #region Entities

        [HttpGet("list/entities")]
        public async Task<IActionResult> List_Entities_Async()
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

            ApiResponse<List<Res_Entity_List>> result;

            try
            {
                result = await _entityDataService.List_Entities();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Entity_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_Entity_List>(
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

        #region Entity Addresses

        [HttpGet("list/entity/addresses")]
        public async Task<IActionResult> List_Entity_Addresses_Async()
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

            ApiResponse<List<Res_EntityAddress_List>> result;

            try
            {
                result = await _entityDataService.List_Entity_Addresses();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_EntityAddress_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_EntityAddress_List>(
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

        [HttpPost("add/entity/address")]
        public async Task<IActionResult> Add_Entity_Address_Async([FromBody] Req_EntityAddress_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_EntityAddress_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Entity_Address(request);
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

        [HttpPost("update/entity/address")]
        public async Task<IActionResult> Update_Entity_Address_Async([FromBody] Req_EntityAddress_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_EntityAddress_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Entity_Address(request);
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

        #region Entity Contacts

        [HttpGet("list/entity/contacts")]
        public async Task<IActionResult> List_Entity_Contacts_Async()
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

            ApiResponse<List<Res_EntityContact_List>> result;

            try
            {
                result = await _entityDataService.List_Entity_Contacts();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_EntityContact_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_EntityContact_List>(
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

        [HttpPost("add/entity/contact")]
        public async Task<IActionResult> Add_Entity_Contact_Async([FromBody] Req_EntityContact_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_EntityContact_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Entity_Contact(request);
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

        [HttpPost("update/entity/contact")]
        public async Task<IActionResult> Update_Entity_Contact_Async([FromBody] Req_EntityContact_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_EntityContact_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Entity_Contact(request);
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

        #region Statuses

        [HttpGet("list/statuses")]
        public async Task<IActionResult> List_Statuses_Async()
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

            ApiResponse<List<Res_Status_List>> result;

            try
            {
                result = await _entityDataService.List_Statuses();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Status_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_Status_List>(
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

        [HttpGet("list/status/groups")]
        public async Task<IActionResult> List_Status_Groups_Async()
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

            ApiResponse<List<Res_StatusGroup_List>> result;

            try
            {
                result = await _entityDataService.List_Status_Groups();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_StatusGroup_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_StatusGroup_List>(
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

        #region Timezones

        [HttpGet("list/timezones")]
        public async Task<IActionResult> List_Timezones_Async()
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

            ApiResponse<List<Res_Timezone_List>> result;

            try
            {
                result = await _entityDataService.List_Timezones();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Timezone_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_Timezone_List>(
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

        [HttpPost("add/timezone")]
        public async Task<IActionResult> Add_Timezone_Async([FromBody] Req_Timezone_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Timezone_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Timezone(request);
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

        [HttpPost("update/timezone")]
        public async Task<IActionResult> Update_Timezone_Async([FromBody] Req_Timezone_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Timezone_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Timezone(request);
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

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_SlipPrinter_List>> result;

            try
            {
                result = await _entityDataService.List_Slip_Printers();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_SlipPrinter_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_SlipPrinter_List>(
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

        [HttpPost("add/slip/printer")]
        public async Task<IActionResult> Add_Slip_Printer_Async([FromBody] Req_SlipPrinter_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_SlipPrinter_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Slip_Printer(request);
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

        [HttpPost("update/slip/printer")]
        public async Task<IActionResult> Update_Slip_Printer_Async([FromBody] Req_SlipPrinter_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_SlipPrinter_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Slip_Printer(request);
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

        #region Payment Types

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

            ApiResponse<List<Res_PaymentTypeIcon_List>> result;

            try
            {
                result = await _entityDataService.List_Payment_Type_Icons();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_PaymentTypeIcon_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_PaymentTypeIcon_List>(
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

            ApiResponse<List<Res_PaymentType_List>> result;

            try
            {
                result = await _entityDataService.List_Payment_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_PaymentType_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_PaymentType_List>(
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

        [HttpPost("add/payment/type")]
        public async Task<IActionResult> Add_Payment_Type_Async([FromBody] Req_PaymentType_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_PaymentType_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Payment_Type(request);
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

        [HttpPost("update/payment/type")]
        public async Task<IActionResult> Update_Payment_Type_Async([FromBody] Req_PaymentType_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_PaymentType_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Payment_Type(request);
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

            ApiResponse<List<Res_TaxType_List>> result;

            try
            {
                result = await _entityDataService.List_Tax_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_TaxType_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_TaxType_List>(
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

        [HttpPost("add/tax/types")]
        public async Task<IActionResult> Add_Tax_Type_Async([FromBody] Req_TaxType_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_TaxType_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Tax_Type(request);
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

        [HttpPost("update/tax/types")]
        public async Task<IActionResult> Update_Tax_Type_Async([FromBody] Req_TaxType_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_TaxType_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Tax_Type(request);
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

            ApiResponse<List<Res_Settings_List>> result;

            try
            {
                result = await _entityDataService.List_Settings();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_Settings_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_Settings_List>(
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

        [HttpPost("add/setting")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add_Setting_Async([FromForm] Req_Settings_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Settings_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Setting(request);
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

        [HttpPost("update/setting")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update_Setting_Async([FromForm] Req_Settings_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_Settings_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Setting(request);
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

        #region Exchange Rates

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

            ApiResponse<List<Res_ExchangeRate_List>> result;

            try
            {
                result = await _entityDataService.List_Exchange_Rates();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_ExchangeRate_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response from _authService.Authenticate"));

                return StatusCode(500, ApiResponse.Fail<Res_ExchangeRate_List>(
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

        [HttpPost("add/exchange/rate")]
        public async Task<IActionResult> Add_Exchange_Rate_Async([FromBody] Req_ExchangeRate_Add request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ExchangeRate_Add>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Add_Exchange_Rate(request);
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

        [HttpPost("update/exchange/rate")]
        public async Task<IActionResult> Update_Exchange_Rate_Async([FromBody] Req_ExchangeRate_Update request)
        {
            // ✅ Validate Request
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                _logger.LogValidation(ControllerLoggerExtensions.Format_ControllerInfo(this, "Validation failed", errors));

                return BadRequest(ApiResponse.Fail<Req_ExchangeRate_Update>(
                    AppErrorCode.ValidationError,
                    errors
                ));
            }

            // ✅ Log Incoming Request
            _logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, request));

            ApiResponse<object> result;

            try
            {
                result = await _entityDataService.Update_Exchange_Rate(request);
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

        #region Slip Types

        [HttpGet("list/slip/types")]
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

            // ✅ Log Incoming Request
            //_logger.LogController(ControllerLoggerExtensions.Format_RequestInfo(this, ""));

            ApiResponse<List<Res_SlipType_List>> result;

            try
            {
                result = await _entityDataService.List_Slip_Types();
            }
            catch (Exception ex)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo(this, "Unhandled exception", ex));

                result = ApiResponse.Fail<List<Res_SlipType_List>>(
                    AppErrorCode.ServerError,
                    new List<string> { ex.Message },
                    500
                );
            }

            if (result == null)
            {
                _logger.LogController(ControllerLoggerExtensions.Format_ControllerInfo<object>(this, "Null response"));

                return StatusCode(500, ApiResponse.Fail<Res_SlipType_List>(
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
    }
}
