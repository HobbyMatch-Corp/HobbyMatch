using HobbyMatch.App.Auth.TokenService;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;

namespace HobbyMatch.App.Auth
{
    /// <summary>
    /// This class is responsible for adding the Authorization header and baseUrl from appSettings to the request
    /// Use it whenever you need to send an authenticated request to the API.
    /// </summary>
    public class AuthHttpClientHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;
        private readonly string _baseUrl;
        public AuthHttpClientHandler(ITokenService tokenService, IOptions<ApiSettings> apiSettings)
        {
            _tokenService = tokenService;
            _baseUrl = apiSettings.Value.BaseUrl;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.RequestUri.IsAbsoluteUri)
            {
                var combinedUri = new Uri(new Uri(_baseUrl), request.RequestUri);
                request.RequestUri = combinedUri;
            }
            var token = await _tokenService.GetAccessTokenAsync();
            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await base.SendAsync(request, cancellationToken);
                return response;
            }
            else
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}

public class ApiSettings
{
    public string BaseUrl { get; set; } = String.Empty;
}