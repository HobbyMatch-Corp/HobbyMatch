using System.Security.Claims;
using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.BL.Services.AppUsers;
using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers
{
    [Route("api/v1/[controller]")]
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
            var emailJWT = User.FindFirst(ClaimTypes.Email)?.Value;

            var user = await _appUserService.GetUserByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(emailJWT) || user.Email != emailJWT)
                return BadRequest();

            return Ok(user.ToDto());
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMeAsync()
        {
            var emailJWT = User.FindFirst(ClaimTypes.Email)?.Value;
            if (emailJWT == null)
                return BadRequest();
            var user = await _appUserService.GetUserByEmailAsync(emailJWT);
            if (user == null)
                return BadRequest();

            return Ok(user.ToDto());
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] UpdateUserDto user)
        {
            var emailJWT = User.FindFirst(ClaimTypes.Email)?.Value;

            var userDb = await _appUserService.GetUserByIdAsync(userId);
            if (userDb == null || string.IsNullOrEmpty(emailJWT) || userDb.Email != emailJWT)
                return BadRequest();

            var result = await _appUserService.UpdateUserAsync(userId, user);

            return Ok(result);
        }
    }
}
