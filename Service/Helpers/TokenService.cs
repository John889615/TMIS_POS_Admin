using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//using TMIS_Common.Models.Authenticate.Users;

namespace POS_Webservice.Helpers
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config) => _config = config;

        //public (string AccessToken, string RefreshToken) GenerateTokens(User user, IEnumerable<string> roles, IEnumerable<string> permissions)
        //{
        //    var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
        //    new Claim(ClaimTypes.Name, user.Username),
        //    new Claim("TenantId", user.FK_TenantID.ToString())
        //};

        //    claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        //    claims.AddRange(permissions.Select(p => new Claim("Permission", p)));

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var accessToken = new JwtSecurityToken(
        //        issuer: _config["Jwt:Issuer"],
        //        audience: _config["Jwt:Audience"],
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(15),
        //        signingCredentials: creds);

        //    var tokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

        //    var refreshToken = Guid.NewGuid().ToString(); // Store in DB w/ expiration

        //    return (tokenString, refreshToken);
        //}
    }

}
