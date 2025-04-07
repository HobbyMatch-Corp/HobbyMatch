
using HobbyMatch.BL.Models.Auth;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

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
			var response = await _httpClient.PostAsJsonAsync("/api/auth/login", new LoginRequest( email, password ));
			if (response.IsSuccessStatusCode)
			{
				authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
			}
			return authResult;
		}

		public async Task<HttpResponseMessage> RegisterUserAsync(string username, string email, string password)
		{
			var request = new UserRegisterRequest(email, password, username);
			var response = await _httpClient.PostAsJsonAsync("/api/auth/register", new { username, email, password });
			return response;
		}
		public async Task<HttpResponseMessage> RegisterBusinessClientAsync(string username, string email, string password, string taxId)
		{
			var request = new BusinessRegisterRequest(email, password, taxId, username);
			var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);
			return response;
		}
	}
}
