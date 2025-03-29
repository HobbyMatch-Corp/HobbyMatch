using HobbyMatch.Model.Entities;

namespace HobbyMatch.App.Services
{
	public class UserService : IUserService
	{
		private readonly HttpClient _httpClient;
		public UserService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
		}
		public async Task<User[]?> GetUsersAsync()
		{
			User[]? user = null;
			var response = await _httpClient.GetAsync("users");
			if (response.IsSuccessStatusCode)
			{
				user = await response.Content.ReadFromJsonAsync<User[]>();
			}
			return user;
		}
		public async Task<User?> GetUserAsync(int id)
		{
			User? user = null;
			var response = await _httpClient.GetAsync("users/" + id);
			if (response.IsSuccessStatusCode)
			{
				user = await response.Content.ReadFromJsonAsync<User>();
			}
			return user;
		}

		public async Task<User?> EditUserAsync(int id, User editedUser)
		{
			User? user = null;
			var response = await _httpClient.PostAsJsonAsync("users/" + id, editedUser);
			if (response.IsSuccessStatusCode)
			{
				user = await response.Content.ReadFromJsonAsync<User>();
			}
			return user;
		}
	}
}
