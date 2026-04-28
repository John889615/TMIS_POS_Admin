using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using POS_Api.ServiceInterfaces.BusinessCentral;
using POS_Common.Models.BusinessCentral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace POS_Api.Services.BusinessCentral
{
    public class BcTokenProvider : IBcTokenProvider
    {
        private const string CacheKey = "BC_OAUTH_TOKEN";
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger<BcTokenProvider> _logger;

        public BcTokenProvider(IConfiguration config, IMemoryCache cache, IHttpClientFactory httpFactory, ILogger<BcTokenProvider> logger)
        {
            _config = config;
            _cache = cache;
            _httpFactory = httpFactory;
            _logger = logger;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (_cache.TryGetValue<string>(CacheKey, out var cached))
                return cached;

            var settings = new BusinessCentralSettings();
            _config.GetSection("BusinessCentral").Bind(settings);

            var tokenEndpoint = $"{settings.Authority}/{settings.TenantId}/oauth2/v2.0/token";

            using var client = _httpFactory.CreateClient(nameof(BcTokenProvider));
            using var req = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);

            var body = new StringBuilder();
            body.Append("grant_type=client_credentials");
            body.Append("&client_id=").Append(Uri.EscapeDataString(settings.ClientId));
            body.Append("&client_secret=").Append(Uri.EscapeDataString(settings.ClientSecret));
            body.Append("&scope=").Append(Uri.EscapeDataString(settings.Scope));

            req.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            var res = await client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadAsStringAsync();
                _logger.LogError("BC OAuth token request failed: {Status} {Body}", res.StatusCode, err);
                throw new InvalidOperationException($"BC token acquisition failed: {res.StatusCode}");
            }

            var payload = await res.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(payload);
            var token = doc.RootElement.GetProperty("access_token").GetString();
            var expiresIn = doc.RootElement.TryGetProperty("expires_in", out var expEl) ? expEl.GetInt32() : 3600;

            // cache with small safety skew
            var lifetime = TimeSpan.FromSeconds(Math.Max(120, expiresIn - 90));
            _cache.Set(CacheKey, token!, lifetime);

            _logger.LogInformation("BC OAuth token acquired. Cached for {Seconds} seconds", lifetime.TotalSeconds);
            return token!;
        }
    }
}
