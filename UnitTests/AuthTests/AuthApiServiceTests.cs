using Moq;
using System.Net;
using System.Text.Json;
using Moq.Protected;
using HobbyMatch.App.Services.Api;
using HobbyMatch.BL.DTOs.Auth;
namespace UnitTests.AuthTests
{
	public class AuthApiServiceTests
	{
		private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
		private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
		private readonly AuthApiService _authApiService;

		public AuthApiServiceTests()
		{
			_httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
			_httpClientFactoryMock = new Mock<IHttpClientFactory>();

			var client = new HttpClient(_httpMessageHandlerMock.Object);
			client.BaseAddress = new Uri("https://localhost:5001");
			_httpClientFactoryMock.Setup(factory => factory.CreateClient("AuthClient")).Returns(client);

			_authApiService = new AuthApiService(_httpClientFactoryMock.Object);
		}

		[Fact]
		public async Task LoginAsync_ShouldReturnAuthResult_WhenLoginIsSuccessful()
		{
			// Arrange
			var email = "test@example.com";
			var password = "password";
			var expectedAuthResult = new AuthResultDto("test", DateTime.Now, "test", DateTime.Now);

			var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(JsonSerializer.Serialize(expectedAuthResult))
			};

			_httpMessageHandlerMock
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(responseMessage);

			// Act
			var authResult = await _authApiService.LoginAsync(email, password);

			// Assert
			Assert.NotNull(authResult);
			Assert.Equal(expectedAuthResult.JwtToken, authResult.JwtToken);
		}

		[Fact]
		public async Task LoginAsync_ShouldReturnNull_WhenLoginFails()
		{
			// Arrange
			var email = "test@example.com";
			var password = "password";
			var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);

			_httpMessageHandlerMock
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(responseMessage);

			// Act
			var authResult = await _authApiService.LoginAsync(email, password);

			// Assert
			Assert.Null(authResult);
		}

		[Fact]
		public async Task RegisterUserAsync_ShouldReturnSuccessResponse_WhenRegisterIsSuccessful()
		{
			// Arrange
			var username = "newuser";
			var email = "newuser@example.com";
			var password = "newpassword";
			var responseMessage = new HttpResponseMessage(HttpStatusCode.Created);

			_httpMessageHandlerMock
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(responseMessage);

			// Act
			var response = await _authApiService.RegisterUserAsync(username, email, password);

			// Assert
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}

		[Fact]
		public async Task RegisterBusinessClientAsync_ShouldReturnSuccessResponse_WhenRegisterIsSuccessful()
		{
			// Arrange
			var username = "businessclient";
			var email = "business@example.com";
			var password = "businesspassword";
			var taxId = "123456789";
			var responseMessage = new HttpResponseMessage(HttpStatusCode.Created);

			_httpMessageHandlerMock
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(responseMessage);

			// Act
			var response = await _authApiService.RegisterBusinessClientAsync(username, email, password, taxId);

			// Assert
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}
	}
}