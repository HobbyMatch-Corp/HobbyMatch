using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.AppUsers
{
    public interface IAppUserRepository
    {
        public Task<List<Domain.Entities.User>> GetUsersAsync();

        public Task<Domain.Entities.User?> GetUserByEmailAsync(string email);

        public Task<Domain.Entities.User?> GetUserByIdAsync(int id);

		public Task<bool> AddFriendToUserAsync(User user, User friend);
		public Task<bool> RemoveFriendFromUserAsync(User user, User friend);
		public Task SaveChangesAsync();
	}
}
