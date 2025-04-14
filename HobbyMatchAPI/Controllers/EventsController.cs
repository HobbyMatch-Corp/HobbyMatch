using Azure.Core;
using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.BL.Services.Event;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(IEventRepository eventRepository, UserManager<Organizer> userManager, IEventService eventService) : ControllerBase
    {
        private readonly IEventRepository _eventRepository = eventRepository; 
        private readonly IEventService _eventService = eventService; 
        //TODO Fix this logic : UserManager is only for the identity
        //TODO class which is Organizer and it must be checked whether the organizer is actually a user or a business client
        private readonly UserManager<Organizer> _userManager = userManager;

        [HttpPost("create")]
        [Authorize]
		public async Task<ActionResult<Event?>> EventCreate([FromBody] CreateEventDto createDto)
		{
			Console.WriteLine($"IsAuthenticated: {User.Identity?.IsAuthenticated}");
			Console.WriteLine($"Authentication Type: {User.Identity?.AuthenticationType}");
			Console.WriteLine($"Name: {User.Identity?.Name}");

			foreach (var claim in User.Claims)
			{
				Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
			}
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized();
			var result = await _eventService.CreateEventAsync(createDto, user.Id);
			return result != null ? Ok(result) : BadRequest("Could not create event");
		}

		[HttpPost("signin")]
        [Authorize]
        public async Task<ActionResult> EventSignin([FromBody] EventSignDto dto)
        {
            if (!int.TryParse(dto.eventId, out int eventId)) return BadRequest("Invalid EventId");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            //TODO Fix this logic 
            var result = await _eventRepository.AddUserToEventAsync(eventId, (user as User)!);
            return result ? Ok() : BadRequest("Could not sign in to event");
        }
        [HttpPost("signout")]
        [Authorize]
        public async Task<ActionResult> EventSignout([FromBody] EventSignDto dto)
        {
            if (!int.TryParse(dto.eventId, out int eventId)) return BadRequest("Invalid EventId");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            //TODO Fix this logic 
            var result = await _eventRepository.RemoveUserFromEventAsync(eventId, (user as User)!);
            return result ? Ok() : BadRequest("Could not sign out from event");
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetFilteredEvents([FromQuery] string? filter)
        {
            var filteredResults = await _eventRepository.GetEventsWithFilter(filter);
            return Ok(filteredResults.Select(result => result.ToDto()));
        }
    }
}
