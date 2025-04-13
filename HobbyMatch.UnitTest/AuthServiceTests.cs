using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Moq.Protected;
namespace HobbyMatch.UnitTest
{
	public class AuthServiceTests
	{
		[Fact]
		public async Task LoginAsync_ReturnsAuthResult_WhenSuccess()
		{
			// Arrange
			var mockHandler = new Mock<HttpMessageHandler>();
			var authResult = new AuthResult { Token = "valid_token" }; // Przykład obiektu AuthResult
			var response = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(JsonSerializer.Serialize(authResult))
			};

			mockHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(response);

			var httpClient = new HttpClient(mockHandler.Object);
			var authService = new AuthService(httpClient); // Przykładowa klasa AuthService, która implementuje LoginAsync

			// Act
			var result = await authService.LoginAsync("test@example.com", "password");

			// Assert
			Assert.NotNull(result);
			Assert.Equal("valid_token", result?.Token);
		}

		[Fact]
		public async Task LoginAsync_ReturnsNull_WhenFailure()
		{
			// Arrange
			var mockHandler = new Mock<HttpMessageHandler>();
			var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

			mockHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(response);

			var httpClient = new HttpClient(mockHandler.Object);
			var authService = new AuthService(httpClient);

			// Act
			var result = await authService.LoginAsync("wrong@example.com", "wrongpassword");

			// Assert
			Assert.Null(result);
		}
	}
}
