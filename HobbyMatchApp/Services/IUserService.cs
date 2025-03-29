using HobbyMatch.Model.Entities;

namespace HobbyMatch.App.Services
{
	public interface IUserService
	{
		public Task<User[]?> GetUsersAsync();
		public Task<User?> GetUserAsync(int id);
		public Task<User?> EditUserAsync(int id, User user); // TODO: Decide what to send as content. User object or just the fields that need to be updated. In that case user would be determined by JWT token.
	}
}
