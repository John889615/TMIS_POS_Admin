using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Interfaces;

namespace POS_Api.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public int UserID => int.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        public string Firstname => User?.FindFirst("Firstname")?.Value ?? string.Empty;

        public string Lastname => User?.FindFirst("Lastname")?.Value ?? string.Empty;

        public string Username => User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

        public int TenantID => int.Parse(User?.FindFirst("TenantID")?.Value ?? "0");

        public int ApplicationID => int.Parse(User?.FindFirst("ApplicationID")?.Value ?? "0");
    }
}
