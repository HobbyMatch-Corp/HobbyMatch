using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.BL.Services.Hobbies;
using HobbyMatch.Database.Repositories.AppUsers;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HobbyMatch.BL.Services.AppUsers
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IHobbyService _hobbyService;

        public AppUserService(IAppUserRepository appUserRepository, IHobbyService hobbyService)
        {
            _appUserRepository = appUserRepository;
            _hobbyService = hobbyService;
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

        public async Task<User> UpdateUserAsync(int userId, UpdateUserDto userDto)
        {
            var hobbies = await _hobbyService.GetHobbiesAsync(userDto.hobbies.ToList());

            var dbUser = await _appUserRepository.GetUserByIdAsync(userId);

            if (dbUser != null)
            {
                dbUser.Email = userDto.email;
                dbUser.UserName = userDto.userName;
                dbUser.Hobbies = hobbies;
            }
            else
            {
                throw new DbUpdateException("User to be updated was not found");
            }
            await _appUserRepository.SaveChangesAsync();

            return dbUser;
        }

        public async Task<bool> AddFriendsAsync(int friendId, User user)
        {
            var friend = await _appUserRepository.GetUserByIdAsync(friendId);
            if (friend == null)
                return false;
            if (
                user.Friends.Any(u => u.Id == friend.Id) || friend.Friends.Any(u => u.Id == user.Id)
            )
                return false;
            if (!await _appUserRepository.AddFriendToUserAsync(user, friend))
                return false;
            return await _appUserRepository.AddFriendToUserAsync(friend, user);
        }

        public async Task<bool> RemoveFriendsAsync(int friendId, User user)
        {
            var friend = user.Friends.FirstOrDefault(u => u.Id == friendId);
            if (friend == null)
                return false;
            if (!await _appUserRepository.RemoveFriendFromUserAsync(user, friend))
                return false;
            return await _appUserRepository.RemoveFriendFromUserAsync(friend, user);
        }
    }
}
