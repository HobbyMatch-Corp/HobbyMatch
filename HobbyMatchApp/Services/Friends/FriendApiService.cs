namespace HobbyMatch.App.Services.Friends
{
    public class FriendApiService : IFriendApiService
    {
        private readonly HttpClientUtils _httpClientUtils;
        public FriendApiService(HttpClientUtils httpClientUtils)
        {
            _httpClientUtils = httpClientUtils;
        }

        public async Task<bool> AddFriendAsync(int friendId)
        {
            return await _httpClientUtils.PostAsyncSafe<int, bool>("friends/add", friendId);
        }

        public async Task<bool> RemoveFriendAsync(int friendId)
        {
            return await _httpClientUtils.PostAsyncSafe<int, bool>("friends/remove", friendId);
        }
    }
}
