using HobbyMatch.BL.Services.AppUsers;
using HobbyMatch.Database.Repositories.AppUsers;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HobbyMatch.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class FriendsController(IAppUserRepository appUserRepository, UserManager<Organizer> userManager, IAppUserService appUserService) : ControllerBase
	{
		private readonly IAppUserRepository _appUserRepository = appUserRepository;

		private readonly IAppUserService _appUserService = appUserService;
		private readonly UserManager<Organizer> _userManager = userManager;
		[HttpPost("add")]
		[Authorize]
		public async Task<ActionResult> AddFriendToUser([FromBody] int friendId)
		{
			var user = await _userManager.GetUserAsync(User);
			var userType = User.FindFirst(ClaimTypes.Role)?.Value;
			if (user == null || userType != UserType.User.ToString()) // Check if user is actually "User" and not "Business Client"
				return Unauthorized();

			var result = await _appUserService.AddFriendsAsync(friendId,(user as User)!);
			return result ? Ok(result) : BadRequest("Could not add friend");
		}

		[HttpPost("remove")]
		[Authorize]
		public async Task<ActionResult> RemoveFriendFromUser([FromBody] int friendId)
		{
			var user = await _userManager.GetUserAsync(User);
			var userType = User.FindFirst(ClaimTypes.Role)?.Value;
			if (user == null || userType != UserType.User.ToString()) // Check if user is actually "User" and not "Business Client"
				return Unauthorized();

			var result = await _appUserService.RemoveFriendFromUserAsync(friendId, (user as User)!);
			return result ? Ok(result) : BadRequest("Could not remove friend");

		 }
	}
}
