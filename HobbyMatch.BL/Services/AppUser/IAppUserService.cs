using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.AppUser
{
    public interface IAppUserService
    {
        public Task<List<User>> GetUsersAsync();

        public Task<User?> GetUserByEmailAsync(string email);

        public Task<User?> GetUserByIdAsync(int id);

        public Task UpdateUserAsync(int userId, User user);
    }
}
