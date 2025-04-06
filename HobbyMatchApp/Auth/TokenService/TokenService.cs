
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
		private string? _accessToken;
		private string _accessTokenName = "accessToken";


		public TokenService(ProtectedLocalStorage localStorage)
		{
			_localStorage = localStorage;
		}

		public async Task SetAccessTokenAsync(string token)
		{
			_accessToken = token;
			await _localStorage.SetAsync(_accessTokenName, token);
		}

		public async Task<string?> GetAccessTokenAsync()
		{
			if (!string.IsNullOrEmpty(_accessToken))
				return _accessToken;

			try 
			{
				var result = await _localStorage.GetAsync<string>("accessToken");
				if (result.Success)
				{
					_accessToken = result.Value;
					return _accessToken;
				}
			}
			catch (Exception) // Apparently protected local storage throws an exception, bcs of double render (once on server side), but should work anyways on client
			{
				return null;
			}
			

			return null;
		}

		public async Task ClearAccessTokenAsync()
		{
			_accessToken = null;
			await _localStorage.DeleteAsync(_accessTokenName);
		}

		public async Task<IEnumerable<Claim>> GetClaimsFromTokenAsync()
		{
			var token = await GetAccessTokenAsync();
			if (string.IsNullOrEmpty(token))
				return Enumerable.Empty<Claim>();

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			return jwtToken.Claims;
		}
	}
}
