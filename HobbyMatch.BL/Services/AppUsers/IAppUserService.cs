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

		public Task<bool> AddFriendsAsync(int friendId, User user);
        public Task<bool> AddFriendToUserAsync(User friend, User user);

		public Task<bool> RemoveFriendsAsync(int friendId, User user);

        public Task<bool> RemoveFriendFromUserAsync(User friend, User user);
	}
}
