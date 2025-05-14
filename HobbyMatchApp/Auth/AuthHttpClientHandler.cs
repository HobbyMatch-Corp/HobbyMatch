using HobbyMatch.App.Auth.CustomAuthStateProvider;
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
        private readonly TokenStore _tokenStore;
        private readonly CustomAuthStateProvider.CustomAuthStateProvider _authStateProvider;
        public AuthHttpClientHandler(TokenStore tokenStore, CustomAuthStateProvider.CustomAuthStateProvider authStateProvider)
        {
			_tokenStore = tokenStore;
			_authStateProvider = authStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
			var token = _tokenStore.GetAccessToken();
			if (token != null)
            {
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await base.SendAsync(request, cancellationToken);
                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden) {
					await _authStateProvider.Logout();
                }
                return response;
            }
            else
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}

public class ApiSettings
{
    public string BaseUrl { get; set; } = null!;
}