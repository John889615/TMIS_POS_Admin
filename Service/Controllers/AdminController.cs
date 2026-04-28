using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using POS_Api.ServiceInterfaces.Authenticate;
using POS_Api.Services.Inventory;
using POS_Common.Enums;
using POS_Common.Models;
using POS_Common.Requests.Users;
using Serilog;
using TMIS_Common.Extensions;

namespace POS_Webservice.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            //var result = await _userService.RevokeRefreshTokenAsync(refreshToken);
            //return result
            //    ? Ok(ApiResponse.Success(true, "Logged out successfully"))
            //    : BadRequest(ApiResponse.Fail<bool>("Logout failed", AppErrorCode.ServerError.ToDescriptionString()));
            return Ok();
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromQuery] string name)
        {
            //var sql = "INSERT INTO Roles (Name) VALUES (@Name)";
            //var success = await _userService.ExecuteAsync(sql, new { Name = name }) > 0;
            //return success
            //    ? Ok(ApiResponse.Success(true, "Role created"))
            //    : BadRequest(ApiResponse.Fail<bool>("Failed to create role", AppErrorCode.ServerError.ToDescriptionString()));
            return Ok();
        }

        [HttpPost("create-permission")]
        public async Task<IActionResult> CreatePermission([FromQuery] string name)
        {
            //var sql = "INSERT INTO Permissions (Name) VALUES (@Name)";
            //var success = await _userService.ExecuteAsync(sql, new { Name = name }) > 0;
            //return success
            //    ? Ok(ApiResponse.Success(true, "Permission created"))
            //    : BadRequest(ApiResponse.Fail<bool>("Failed to create permission", AppErrorCode.ServerError.ToDescriptionString()));
            return Ok();
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            //var result = await _userService.GetAllRolesAsync();
            //return Ok(ApiResponse.Success(result));
            return Ok();
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions()
        {
            //var result = await _userService.GetAllPermissionsAsync();
            //return Ok(ApiResponse.Success(result));
            return Ok();
        }

        [HttpGet("role-permissions")]
        public async Task<IActionResult> GetRolePermissions([FromQuery] int roleId)
        {
            //var result = await _userService.GetRolePermissionsAsync(roleId);
            //return Ok(ApiResponse.Success(result));
            return Ok();
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromQuery] int userId, [FromQuery] int roleId)
        {
            //var success = await _userService.AssignRoleAsync(userId, roleId);
            //return success
            //    ? Ok(ApiResponse.Success(true, "Role assigned"))
            //    : BadRequest(ApiResponse.Fail<bool>("Role assignment failed", AppErrorCode.ServerError.ToDescriptionString()));
            return Ok();
        }

        [HttpPost("assign-permission")]
        public async Task<IActionResult> AssignPermission([FromQuery] int roleId, [FromQuery] int permissionId)
        {
            //var success = await _userService.AssignPermissionToRoleAsync(roleId, permissionId);
            //return success
            //    ? Ok(ApiResponse.Success(true, "Permission assigned"))
            //    : BadRequest(ApiResponse.Fail<bool>("Permission assignment failed", AppErrorCode.ServerError.ToDescriptionString()));
            return Ok();
        }
    }
}
