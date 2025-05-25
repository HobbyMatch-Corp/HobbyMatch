using HobbyMatch.App.Auth.TokenService;
using HobbyMatch.App.Services.Api;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;


namespace HobbyMatch.App.Auth.CustomAuthStateProvider
{
	public class CustomAuthStateProvider : AuthenticationStateProvider
	{
		private readonly ITokenService _tokenService;
		private readonly IAuthApiService _authApiService;

		public CustomAuthStateProvider(ITokenService tokenService, IAuthApiService authApiService)
		{
			_tokenService = tokenService;
			_authApiService = authApiService;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var claims = _tokenService.GetClaimsFromToken();
			var identity = claims.Any() ? new ClaimsIdentity(claims, "Bearer") : new ClaimsIdentity();

			var user = new ClaimsPrincipal(identity);
			return new AuthenticationState(user);
		}

		public async Task<bool> LoginAsync(string email, string password)
		{
			var authResult = await _authApiService.LoginAsync(email, password);
			if (authResult != null)
			{
				await _tokenService.SetAccessTokenAsync(authResult.JwtToken);
			}
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
			return authResult != null;
		}

		public async Task<bool> LoginAsync(string token)
		{
			await _tokenService.SetAccessTokenAsync(token);
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
			return token != null;
		}
		public async Task Logout()
		{
			await _tokenService.ClearAccessTokenAsync();
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
		}
	}
}
