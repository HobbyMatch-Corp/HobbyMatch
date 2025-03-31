using HobbyMatch.App.Auth.TokenService;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;


namespace HobbyMatch.App.Auth.CustomAuthStateProvider
{
	public class CustomAuthStateProvider : AuthenticationStateProvider
	{
		private readonly ITokenService _tokenService;
		public CustomAuthStateProvider(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var claims = await _tokenService.GetClaimsFromTokenAsync();
			var identity = claims.Any() ? new ClaimsIdentity(claims, "Bearer") : new ClaimsIdentity();

			var user = new ClaimsPrincipal(identity);

			return new AuthenticationState(user);
		}
	}
}
