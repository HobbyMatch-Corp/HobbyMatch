using HobbyMatch.App.Auth.TokenService;
using HobbyMatch.App.Services;
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
            var claims = await _tokenService.GetClaimsFromTokenAsync();
            var identity = claims.Any() ? new ClaimsIdentity(claims, "Bearer") : new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public async void Login(string email, string password)
        {
            var authResult = await _authApiService.LoginAsync(email, password);
            if (authResult != null)
            {
                await _tokenService.SetAccessTokenAsync(authResult.JwtToken);
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public void Logout()
        {
            _tokenService.ClearAccessTokenAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }
    }
}
