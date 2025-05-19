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

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.AppUsers
                .Include(x => x.Hobbies)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbContext.AppUsers
                .Include(x => x.Hobbies)
                .FirstOrDefaultAsync(x => x.Id == id);
   //         var x = await _dbContext.AppUsers.ToListAsync();
			//return await _dbContext.AppUsers.FindAsync(id);
        }

        public async Task<List<User>> GetUsersAsync()
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
                dbUser.Hobbies = user.Hobbies;
            }
            await _dbContext.SaveChangesAsync();
        }
		public async Task<bool> AddFriendToUserAsync(User user, User friend)
		{
			user.Friends.Add(friend);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> RemoveFriendFromUserAsync(User user, User friend)
		{
			user.Friends.Remove(friend);
			await _dbContext.SaveChangesAsync();
			return true;
		}
	}
}
