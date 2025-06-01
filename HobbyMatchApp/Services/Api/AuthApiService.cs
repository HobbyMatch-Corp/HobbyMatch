using HobbyMatch.BL.DTOs.Auth;

namespace HobbyMatch.App.Services.Api
{
	public class AuthApiService : IAuthApiService
	{
        private readonly HttpClientUtils _httpClientUtils;

        public AuthApiService(HttpClientUtils httpClientUtils)
        {
            _httpClientUtils = httpClientUtils;
        }

		public async Task<AuthResultDto?> LoginAsync(string email, string password)
		{
			var request = new LoginRequestDto(email, password);
			return await _httpClientUtils.PostAsyncSafe<LoginRequestDto, AuthResultDto>("auth/login", request, unauthorized: true);
		}

		public async Task<HttpResponseMessage?> RegisterUserAsync(string username, string email, string password)
		{
			var request = new UserRegisterDto(email, password, username);
			return await _httpClientUtils.PostAsyncSafe<UserRegisterDto, HttpResponseMessage>("auth/register/user", request, unauthorized: true);
        }

		public async Task<HttpResponseMessage?> RegisterBusinessClientAsync(string username, string email, string password, string taxId)
		{
			var request = new BusinessRegisterDto(email, password, taxId, username);
            return await _httpClientUtils.PostAsyncSafe<BusinessRegisterDto, HttpResponseMessage>("auth/register/business", request, unauthorized: true);
        }
	}
}
