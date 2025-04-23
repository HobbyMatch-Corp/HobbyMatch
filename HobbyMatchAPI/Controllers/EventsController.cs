using Azure.Core;
using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.BL.Services.Events;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Enums;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(UserManager<Organizer> userManager, IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService; 
        private readonly UserManager<Organizer> _userManager = userManager;
        // TODO: Think about moving userType check to service and adding "if result is ActionResult actionResult" check 

        [HttpPost("create")]
        [Authorize]
		public async Task<ActionResult<Event?>> EventCreate([FromBody] CreateEventDto createDto)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized();
			var result = await _eventService.CreateEventAsync(createDto, user.Id);
			return result != null ? Ok(result.ToDto()) : BadRequest("Could not create event");
		}

		[HttpPost("signin")]
        [Authorize]
        public async Task<ActionResult> EventSignin([FromBody] EventSignDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
			var userType = User.FindFirst("userType")?.Value;
			if (user == null || userType != UserType.User.ToString()) // Check if user is actually "User" and not "Business Client"
				return Unauthorized();

			var result = await _eventService.AddUserToEventAsync(dto.eventId, (user as User)!);
            return result ? Ok() : BadRequest("Could not sign in to event");
        }
        [HttpPost("signout")]
        [Authorize]
        public async Task<ActionResult> EventSignout([FromBody] EventSignDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
			var userType = User.FindFirst("userType")?.Value;
			if (user == null || userType != UserType.User.ToString()) // Check if user is actually "User" and not "Business Client"
				return Unauthorized();

			var result = await _eventService.RemoveUserFromEventAsync(dto.eventId, (user as User)!);
            return result ? Ok() : BadRequest("Could not sign out from event");
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetFilteredEvents([FromQuery] string? filter)
        {
            var filteredResults = await _eventService.GetEventsWithFilter(filter);
            return Ok(filteredResults.Select(result => result.ToDto()));
        }
    }
}
