﻿using HobbyMatch.BL.DTOs.Auth;

namespace HobbyMatch.App.Services.Api
{
	public class AuthApiService : IAuthApiService
	{
		private readonly HttpClient _httpClient;

		public AuthApiService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("AuthClient");
		}

		public async Task<AuthResultDto?> LoginAsync(string email, string password)
		{
            AuthResultDto? authResult = null;
			var request = new LoginRequestDto(email, password);
			var response = await _httpClient.PostAsJsonAsync("auth/login", request);
			if (response.IsSuccessStatusCode)
			{
				authResult = await response.Content.ReadFromJsonAsync<AuthResultDto>();
			}
			return authResult;
		}

		public async Task<HttpResponseMessage> RegisterUserAsync(string username, string email, string password)
		{
			var request = new UserRegisterDto(email, password, username);
			var response = await _httpClient.PostAsJsonAsync("auth/register", request);
			return response;
		}

		public async Task<HttpResponseMessage> RegisterBusinessClientAsync(string username, string email, string password, string taxId)
		{
			var request = new BusinessRegisterDto(email, password, taxId, username);
			var response = await _httpClient.PostAsJsonAsync("auth/register", request);
			return response;
		}
	}
}
