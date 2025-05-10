using HobbyMatch.App.Auth.CustomAuthStateProvider;
using HobbyMatch.App.Auth.TokenService;
using HobbyMatch.App.Services.Api;
using Microsoft.AspNetCore.Components.Authorization;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace UnitTests.AuthTests
{
	public class CustomAuthStateProviderTests
	{
		private Mock<ITokenService> _tokenServiceMock;
		private Mock<IAuthApiService> _authApiServiceMock;
		private CustomAuthStateProvider _customAuthStateProvider;
		public CustomAuthStateProviderTests()
		{
			_tokenServiceMock = new Mock<ITokenService>();
			_authApiServiceMock = new Mock<IAuthApiService>();
			_customAuthStateProvider = new CustomAuthStateProvider(_tokenServiceMock.Object, _authApiServiceMock.Object);
		}

		[Fact]
		public async Task GetAuthenticationStateAsync_ShouldReturnAuthenticationState_WhenTokenPresent()
		{
			// Arrange
			var responseClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, "testuser"),
				new Claim(ClaimTypes.Email, "test@example.com"),
				new Claim(ClaimTypes.Role, "Admin")
			};

			var expectedAuthState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(responseClaims, "Bearer") ));

			_tokenServiceMock
				.Setup(s => s.GetClaimsFromToken())
				.Returns(responseClaims);

			// Act
			var result = await _customAuthStateProvider.GetAuthenticationStateAsync();
			// Assert
			Assert.NotNull(result);
			Assert.True(result.User.Identity.IsAuthenticated);
			Assert.Equal("testuser", result.User.Identity.Name);
			Assert.Contains(result.User.Claims, c => c.Type == ClaimTypes.Email && c.Value == "test@example.com");
			Assert.Contains(result.User.Claims, c => c.Type == ClaimTypes.Role && c.Value == "Admin");
		}
	}
}
