
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
	public class TokenStore
	{
		private string? _accessToken;

		public void SetAccessToken(string? token)
		{
			_accessToken = token;
		}

		public string? GetAccessToken()
		{
			return _accessToken;
		}
	}
}
