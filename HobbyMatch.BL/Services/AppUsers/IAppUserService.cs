using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.AppUsers
{
    public interface IAppUserService
    {
        public Task<List<User>> GetUsersAsync();

        public Task<User?> GetUserByEmailAsync(string email);

        public Task<User?> GetUserByIdAsync(int id);

        public Task UpdateUserAsync(int userId, UpdateUserDto userDto);
    }
}
