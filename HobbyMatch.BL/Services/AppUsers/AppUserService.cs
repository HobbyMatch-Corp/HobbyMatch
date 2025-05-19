using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Database.Repositories.AppUsers;
using HobbyMatch.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HobbyMatch.BL.Services.AppUsers
{

    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUserService(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _appUserRepository.GetUserByEmailAsync(email);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _appUserRepository.GetUserByIdAsync(id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _appUserRepository.GetUsersAsync();
        }

        public async Task UpdateUserAsync(int userId, UpdateUserDto userDto)
        {
			var user = new User
			{
				Email = userDto.Email,
				UserName = userDto.UserName
			};
			await _appUserRepository.UpdateUserAsync(userId, user);
        }
        public async Task<bool> AddFriendsAsync(int friendId, User user)
        {
			var friend = await _appUserRepository.GetUserByIdAsync(friendId);
			if (friend == null) return false;
            if (!await AddFriendToUserAsync(friend, user)) return false;
            return await AddFriendToUserAsync(user, friend);
		}
		public async Task<bool> AddFriendToUserAsync(User friend, User user)
		{
			if (user.Friends.Any(u => u.Id == friend.Id)) return false;
            return await _appUserRepository.AddFriendToUserAsync(user, friend);
		}
		public async Task<bool> RemoveFriendsAsync(int friendId, User user)
		{
			var friend = user.Friends.FirstOrDefault(u => u.Id == friendId);
			if (friend == null) return false;
			if (!await RemoveFriendFromUserAsync(friend, user)) return false;
			return await RemoveFriendFromUserAsync(user, friend);
		}

		public async Task<bool> RemoveFriendFromUserAsync(User friend, User user)
		{
            return await _appUserRepository.RemoveFriendFromUserAsync(user, friend);
		}
	}
}
