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

        public async Task<Model.Entities.User?> GetUserBeEmailAsync(string email)
        {
            return await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Model.Entities.User?> GetUserBeIdAsync(int id)
        {
            return await _dbContext.AppUsers.FindAsync(id);
        }

        public Task<List<Model.Entities.User>> GetUsersAsync()
        {
            return _dbContext.AppUsers.ToListAsync();
        }

        public async Task UpdateUserAsync(string email, Model.Entities.User user)
        {
            var dbUser = await _dbContext.AppUsers.FirstOrDefaultAsync(u => u.Email == email);

            if (dbUser != null)
            {
                dbUser.Email = user.Email;
                dbUser.UserName = user.UserName;
            }
        }
    }
}
