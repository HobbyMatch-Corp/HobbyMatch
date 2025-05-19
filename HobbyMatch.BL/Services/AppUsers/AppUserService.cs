using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.BL.Services.Hobbies;
using HobbyMatch.Database.Repositories.AppUsers;
using HobbyMatch.Domain.Entities;

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

        public async Task UpdateUserAsync(int userId, UpdateUserDto userDto)
        {
            var hobbies = await _hobbyService.GetHobbiesAsync(userDto.Hobbies.ToList());

			var user = new User
			{
				Email = userDto.Email,
				UserName = userDto.UserName,
                Hobbies = hobbies
            };
			await _appUserRepository.UpdateUserAsync(userId, user);
        }
    }
}
