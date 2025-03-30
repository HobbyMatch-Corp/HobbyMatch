using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Database.Repositories.AppUser
{
    public interface IAppUserRepository
    {
        public Task<List<Domain.Entities.User>> GetUsersAsync();

        public Task<Domain.Entities.User?> GetUserByEmailAsync(string email);

        public Task<Domain.Entities.User?> GetUserByIdAsync(int id);

        public Task UpdateUserAsync(int id, Domain.Entities.User user);
    }
}
