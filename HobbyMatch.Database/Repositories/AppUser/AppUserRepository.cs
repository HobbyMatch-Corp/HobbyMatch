using HobbyMatch.Database.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Database.Repositories.AppUser
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
            return await _dbContext.AppUsers.FindAsync(id);
        }

        public Task<List<Domain.Entities.User>> GetUsersAsync()
        {
            return _dbContext.AppUsers.ToListAsync();
        }

        public async Task UpdateUserAsync(int userId, Domain.Entities.User user)
        {
            var dbUser = await GetUserByIdAsync(userId);

            if (dbUser != null)
            {
                dbUser.Email = user.Email;
                dbUser.UserName = user.UserName;
            }
        }
    }
}
