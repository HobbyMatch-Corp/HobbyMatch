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

        public Task<Model.Entities.User?> GetUserBeEmailAsync(string email);

        public Task<Model.Entities.User?> GetUserBeIdAsync(int id);

        public Task UpdateUserAsync(string email, Model.Entities.User user);
    }
}
