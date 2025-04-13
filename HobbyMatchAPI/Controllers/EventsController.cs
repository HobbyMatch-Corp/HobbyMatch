using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(IEventRepository eventRepository, UserManager<User> userManager) : ControllerBase
    {
        private readonly IEventRepository _eventRepository = eventRepository;
        private readonly UserManager<User> _userManager = userManager;

        [HttpPost("eventsignin")]
        [Authorize]
        public async Task<ActionResult> EventSignin([FromBody] EventSignDto dto)
        {
            if (!int.TryParse(dto.eventId, out int eventId)) return BadRequest("Invalid EventId");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await _eventRepository.AddUserToEventAsync(eventId, user);
            return result ? Ok() : BadRequest("Could not sign in to event");
        }
        [HttpPost("eventsignout")]
        [Authorize]
        public async Task<ActionResult> EventSignout([FromBody] EventSignDto dto)
        {
            if (!int.TryParse(dto.eventId, out int eventId)) return BadRequest("Invalid EventId");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var result = await _eventRepository.RemoveUserFromEventAsync(eventId, user);
            return result ? Ok() : BadRequest("Could not sign out from event");
        }
    }
}
