using HobbyMatch.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.AppUser
{
    public interface IAppUserService
    {
        public Task<List<User>> GetUsersAsync();

        public Task<User?> GetUserByEmailAsync(string email);

        public Task<User?> GetUserByIdAsync(int id);

        public Task UpdateUserAsync(string email, User user);
    }
}
