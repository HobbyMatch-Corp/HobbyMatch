﻿using HobbyMatch.BL.Services.AppUser;
using HobbyMatch.Database.Data;
using HobbyMatch.Database.Repositories.AppUser;
using HobbyMatch.Database.Repositories.User;
using HobbyMatch.Domain.Entities;
using HobbyMatchAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace HobbyMatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public UsersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _appUserService.GetUsersAsync());
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync(int userId)
        {
            var emailJWT = User.FindFirst("email")?.Value;

            var user = await _appUserService.GetUserByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(emailJWT) || user.Email != emailJWT) return BadRequest();

            return Ok(user);
        }

        [Authorize]
        [HttpPost("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] User user)
        {
            var emailJWT = User.FindFirst("email")?.Value;

            var userDb = await _appUserService.GetUserByIdAsync(userId);
            if (userDb == null || string.IsNullOrEmpty(emailJWT) || userDb.Email != emailJWT) return BadRequest();

            await _appUserService.UpdateUserAsync(userId, user);

            return Ok();
        }


    }
}
