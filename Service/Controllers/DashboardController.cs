using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using POS_Api.Services.Inventory;
using POS_Common.Requests.Users;
using Serilog;

namespace POS_Webservice.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : Controller
    {
        #region Members

        private readonly IConfiguration _configuration;
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods

        [HttpPost("login")]
        public IActionResult Login(Login_Request request)
        {
            if (request.Username == "admin" && request.Password == "password")
            {
                return Ok(new { Message = "Login successful", Token = "dummy-jwt-token" });
            }

            return Unauthorized(new { Message = "Invalid username or password" });
        }


        [HttpGet("ping")]
        public IActionResult Ping()
        {
            try
            {
                Log.Information("User {UserId} attempted to login");

                //Inventory_Service service = new Inventory_Service(_configuration, Log.Logger);
                //var result = service.testLogging();

                throw new Exception("Jou ma se kinders");


                return Ok(new { Message = "Server Online" });
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Error in {ActionName} with request {RequestId}");
                return BadRequest("Sies");
            }
        }
        #endregion
    }
}
