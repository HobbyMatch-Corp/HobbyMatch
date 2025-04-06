
using HobbyMatch.BL.Models.Auth;

namespace HobbyMatch.App.Services
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
			var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { email, password });
			if (response.IsSuccessStatusCode)
			{
				authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
			}
			return authResult;
		}

		public Task<string?> RegisterAsync(string email, string password)
		{
			throw new NotImplementedException();
		}
	}
}
