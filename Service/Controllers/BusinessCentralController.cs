using Microsoft.AspNetCore.Mvc;
using POS_Api.ServiceInterfaces.BusinessCentral;
using POS_Api.Services.BusinessCentral;

namespace POS_Webservice.Controllers
{
    [ApiController]
    [Route("api/bc")]
    public class BusinessCentralController : ControllerBase
    {
        private readonly IBusinessCentral_Service _svc;
        public BusinessCentralController(IBusinessCentral_Service svc) => _svc = svc;

        [HttpGet("ping")]
        public async Task<IActionResult> Ping()
        {
            var ok = await _svc.PingAsync();
            return Ok(new { ok });
        }

        [HttpGet("debtors")]
        public async Task<IActionResult> GetDebtors()
        {
            var result = await _svc.GetDebtorsAsync();
            return Ok(result);
        }

        [HttpGet("creditors")]
        public async Task<IActionResult> GetCreditors()
        {
            var result = await _svc.GetCreditorsAsync();
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> SyncAllAsync()
        {
            var result = await _svc.SyncAllAsync();
            return Ok(result);
        }

        [HttpGet("units")]
        public async Task<IActionResult> SyncUnitsFromItemsAsync()
        {
            var result = await _svc.SyncUnitsFromItemsAsync();
            return Ok(result);
        }

        [HttpGet("locations")]
        public async Task<IActionResult> SyncLocationsAsync()
        {
            var result = await _svc.SyncLocationsAsync();
            return Ok(result);
        }

        [HttpGet("product/categories")]
        public async Task<IActionResult> SyncProductCategoriesAsync()
        {
            var result = await _svc.SyncProductCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("price/codes")]
        public async Task<IActionResult> SyncPriceCodesAsync()
        {
            var result = await _svc.SyncPriceCodesAsync();
            return Ok(result);
        }

        [HttpGet("products")]
        public async Task<ActionResult> SyncProductsAsync()
        {
            var result = await _svc.SyncProductsAsync();
            return Ok(result);
        }

        [HttpGet("product/locations")]
        public async Task<IActionResult> SyncItemAvailabilityByLocationAsync()
        {
            var result = await _svc.SyncItemAvailabilityByLocationAsync();
            return Ok(result);
        }

        [HttpGet("product/location/prices")]
        public async Task<IActionResult> SyncSalesPricingAsync()
        {
            var result = await _svc.SyncSalesPricingAsync();
            return Ok(result);
        }

        [HttpGet("create/invoice")]
        public async Task<IActionResult> CreateInvoiceAsync()
        {
            var result = await _svc.CreateInvoiceAsync();
            return Ok(result);
        }
    }
}
