using HobbyMatch.Database.Repositories.AppUser;
using HobbyMatch.Model.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.AppUser
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

        public async Task UpdateUserAsync(int userId, User user)
        {
            await _appUserRepository.UpdateUserAsync(userId, user);
        }
    }
}
