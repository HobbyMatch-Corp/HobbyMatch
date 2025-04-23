using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.BL.Services.Events;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
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
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized();
			var result = await _eventService.CreateEventAsync(createDto, user.Id);
			return result != null ? Ok(result.ToDto()) : BadRequest("Could not create event");
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
            var filteredResults = await _eventRepository.GetEventsWithFilterAsync(filter);
            return Ok(filteredResults.Select(result => result.ToDto()));
        }

        [HttpGet("signedUpEvents")]
        [Authorize]
        public async Task<IActionResult> GetSignedUpEvents()
        {
            var emailJwt = User.FindFirst("email")?.Value;
            if (emailJwt is null) return BadRequest("No email found in claims.");

            var signedUpEvents = await _eventRepository.GetSignedUpEventsAsync(emailJwt);
            if (signedUpEvents is null) return BadRequest("Wrong email.");

            return Ok(signedUpEvents.Select(result => result.ToDto()));
        }

        [HttpGet("organizedEvents")]
        [Authorize]
        public async Task<IActionResult> GetOrganizedEvents()
        {
            var emailJwt = User.FindFirst("email")?.Value;
            if (emailJwt is null) return BadRequest("No email found in claims.");

            var organizedEvents = await _eventRepository.GetOrganizedEventsAsync(emailJwt);
            if (organizedEvents is null) return BadRequest("Wrong email.");

            return Ok(organizedEvents.Select(result => result.ToDto()));
        }

        [HttpGet("sponsoredEvents")]
        [Authorize]
        public async Task<IActionResult> GetSponsoredEvents()
        {
            var emailJwt = User.FindFirst("email")?.Value;
            if (emailJwt is null) return BadRequest("No email found in claims.");

            var sponsoredEvents = await _eventRepository.GetSponsoredEventsAsync(emailJwt);
            if (sponsoredEvents is null) return BadRequest("Wrong email.");

            return Ok(sponsoredEvents.Select(result => result.ToDto()));
        }
    }
}
