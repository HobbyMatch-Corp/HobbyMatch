using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Repositories.AppUsers
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _dbContext;

        public AppUserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Domain.Entities.User?> GetUserByIdAsync(int id)
        {
            var x = await _dbContext.AppUsers.ToListAsync();
			return await _dbContext.AppUsers.FindAsync(id);
        }

        public async Task<List<Domain.Entities.User>> GetUsersAsync()
        {
            return await _dbContext.AppUsers.ToListAsync();
		}

        public async Task UpdateUserAsync(int userId, User user)
        {
            var dbUser = await GetUserByIdAsync(userId);

            if (dbUser != null)
            {
                dbUser.Email = user.Email;
                dbUser.UserName = user.UserName;
            }
            await _dbContext.SaveChangesAsync();
        }
		public async Task<bool> AddFriendToUserAsync(User user, User friend)
		{
			if (user.Friends.Any(u => u.Id == friend.Id)) return false;
			user.Friends.Add(friend);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> RemoveFriendFromUserAsync(User user, User friend)
		{
			var existing = user.Friends.FirstOrDefault(u => u.Id == friend.Id);
			if (existing == null) return false;
			user.Friends.Remove(existing);
			await _dbContext.SaveChangesAsync();
			return true;
		}
	}
}
