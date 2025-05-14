
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HobbyMatch.App.Auth.TokenService
{
    /// <summary>
    /// This class is responsible for managing the access token. 
    /// Use it to set (on Login or Refresh), get (on Api Requests), and clear (on Logout) the access token.
    /// Use it to get claims from the access token.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private TokenStore _tokenStore;
        private string _accessTokenKey = "accessToken";

        public TokenService(ProtectedLocalStorage localStorage, TokenStore tokenStore)
        {
            _localStorage = localStorage;
            _tokenStore = tokenStore;
        }

        public async Task SetAccessTokenAsync(string token)
        {
            _tokenStore.SetAccessToken(token);
			await _localStorage.SetAsync(_accessTokenKey, token);
        }

        public async Task LoadTokenFromLocalStorage()
        {
			try
			{
				var result = await _localStorage.GetAsync<string>(_accessTokenKey);
				if (result.Success)
				{
					_tokenStore.SetAccessToken(result.Value);
				}
			}
			// Apparently protected local storage throws an exception, bcs of double render (once on server side), but should work anyways on client
			catch (System.InvalidOperationException)
			{
			}

		}

        public async Task ClearAccessTokenAsync()
        {
            try
            {
				await _localStorage.DeleteAsync(_accessTokenKey);
				_tokenStore.SetAccessToken(null);
            }
            catch (System.InvalidOperationException)  // Blazor may throw this error while rendering on server side
            {            }
        }

        public IEnumerable<Claim> GetClaimsFromToken()
        {
            var token = _tokenStore.GetAccessToken();
            if (string.IsNullOrEmpty(token))
                return Enumerable.Empty<Claim>();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.Claims;
        }
    }
}
