using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Database.Repositories.AppUser
{
    public interface IAppUserRepository
    {
        public Task<List<Model.Entities.User>> GetUsersAsync();

        public Task<Model.Entities.User?> GetUserByEmailAsync(string email);

        public Task<Model.Entities.User?> GetUserByIdAsync(int id);

        public Task UpdateUserAsync(int id, Model.Entities.User user);
    }
}
