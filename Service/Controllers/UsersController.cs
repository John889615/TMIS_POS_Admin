using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using POS_Api.Services;
using POS_Api.Services.Inventory;
using POS_Common.Requests.Users;
using Serilog;
//using TMIS_Common.Models.Authenticate.Users;

namespace POS_Webservice.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Members

        private readonly IConfiguration _config;
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public UsersController(IConfiguration config)
        {
            _config = config;
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


        //[HttpPost("ping")]
        //public async Task<IActionResult> Ping(User item)
        //{
        //    try
        //    {
        //        item.UserID = 1; // Always set UserID to 1
        //        string sqlConn = ("data source=172.17.3.4;initial catalog=TravelPOS;user id=Developer;password=M@n828258da;MultipleActiveResultSets=True;Connection Timeout=300;TrustServerCertificate=True;");
        //        var user = await Inventory_Base_Service_Sample.Users_Select_Single(item, sqlConn);
        //        return Ok(new { Message = "Server Online", User = user });
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Error in Ping endpoint");
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}
        #endregion
    }
}