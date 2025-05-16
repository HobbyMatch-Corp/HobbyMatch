namespace HobbyMatch.App.Services.Friends
{
	public class FriendApiService : IFriendApiService
	{
		private readonly HttpClient _httpClient;
		public FriendApiService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
		}
		public async Task<bool> AddFriendAsync(int friendId)
		{
			bool success = false;
			var response = await _httpClient.PostAsJsonAsync("friends/add", friendId);
			if (response.IsSuccessStatusCode)
			{
				success = await response.Content.ReadFromJsonAsync<bool>();
			}
			return success;
		}

		public async Task<bool> RemoveFriendAsync(int friendId)
		{
			bool success = false;
			var response = await _httpClient.PostAsJsonAsync("friends/remove", friendId);
			if (response.IsSuccessStatusCode)
			{
				success = await response.Content.ReadFromJsonAsync<bool>();
			}
			return success;
		}
	}
}
