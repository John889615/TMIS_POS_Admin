using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using POS_Common.ModelsDto.Authenticate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.Services
{
    public class AuthTokenManager
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private static string _token;
        private static DateTime _expiryUtc = DateTime.MinValue;
        private static readonly SemaphoreSlim _lock = new(1, 1);

        public AuthTokenManager(IHttpClientFactory httpClientFactory,
                                IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetTokenAsync(CancellationToken ct = default)
        {
            await _lock.WaitAsync(ct);
            try
            {
                if (!string.IsNullOrEmpty(_token) && DateTime.UtcNow < _expiryUtc)
                    return _token;

                var url = _configuration["AuthSync:Path"];

                var payload = new Req_Authenticate()
                {
                    StaffCode = _configuration["AuthSync:StaffCode"],
                    Pin = int.Parse(_configuration["AuthSync:Pin"])
                };

                // skies drop ek die call voor jy praat, los net so moenie goed change
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                var content = new StringContent(JsonConvert.SerializeObject(payload), null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var contentReturn = await response.Content.ReadAsStringAsync();
                var auth = JsonConvert.DeserializeObject<Res_Authenticate>(contentReturn);

                if (auth == null || string.IsNullOrWhiteSpace(auth.Data.AccessToken))
                    throw new InvalidOperationException("Authentication failed: empty token.");

                _token = auth.Data.AccessToken;

                // Refresh a bit early to avoid edge cases (30s skew)
                var expires = auth.Data.AccessTokenExpiration.ToUniversalTime() < DateTime.Now ? DateTime.Now.AddSeconds(210) : auth.Data.AccessTokenExpiration.ToUniversalTime();
                _expiryUtc = expires.AddSeconds(-30);

                return _token;
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}
