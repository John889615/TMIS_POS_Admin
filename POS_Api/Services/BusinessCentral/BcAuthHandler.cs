using POS_Api.ServiceInterfaces.BusinessCentral;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace POS_Api.Services.BusinessCentral
{
    public class BcAuthHandler : DelegatingHandler
    {
        private readonly IBcTokenProvider _tokenProvider;

        public BcAuthHandler(IBcTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Ignore caller cancellation completely
            var token = await _tokenProvider.GetAccessTokenAsync();

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // 🚨 Force NO cancellation downstream
            return await base.SendAsync(request, CancellationToken.None);
        }
    }
}
