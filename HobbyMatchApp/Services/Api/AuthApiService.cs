using HobbyMatch.BL.Models.Auth;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.App.Services.Api
{
	public class AuthApiService : IAuthApiService
	{
		private readonly HttpClient _httpClient;

		public AuthApiService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("AuthClient");
		}

		public async Task<AuthResult?> LoginAsync(string email, string password)
		{
			AuthResult? authResult = null;
			var request = new LoginRequest(email, password);
			var response = await _httpClient.PostAsJsonAsync("auth/login", request);
			if (response.IsSuccessStatusCode)
			{
				authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
			}
			return authResult;
		}

		public async Task<HttpResponseMessage> RegisterUserAsync(string username, string email, string password)
		{
			var request = new UserRegisterRequest(email, password, username);
			var response = await _httpClient.PostAsJsonAsync("auth/register", request);
			return response;
		}

		public async Task<HttpResponseMessage> RegisterBusinessClientAsync(string username, string email, string password, string taxId)
		{
			var request = new BusinessRegisterRequest(email, password, taxId, username);
			var response = await _httpClient.PostAsJsonAsync("auth/register", request);
			return response;
		}
	}
}
