namespace HobbyMatch.App.Services.Friends
{
	public interface IFriendApiService
	{
		Task<bool> AddFriendAsync(int friendId);
		Task<bool> RemoveFriendAsync(int friendId);
	}
}
