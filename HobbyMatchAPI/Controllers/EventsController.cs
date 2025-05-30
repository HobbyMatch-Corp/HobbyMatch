﻿using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.BL.ResultEnums;
using HobbyMatch.BL.Services.Events;
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
    public class EventsController(UserManager<Organizer> userManager, IEventService eventService) : ControllerBase
    {
		private readonly IEventService _eventService = eventService; 
        private readonly UserManager<Organizer> _userManager = userManager;

		[HttpPost("")]
		[Authorize]
		public async Task<ActionResult<Event?>> EventCreate([FromBody] CreateEventDto createRequest)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized();
			var result = await _eventService.CreateEventAsync(createRequest, user.Id);
			return result != null ? Ok(result.ToDto()) : BadRequest("Could not create event");
		}
		[HttpPut("{eventId}")]
		[Authorize]
		public async Task<ActionResult<Event?>> EventEdit([FromBody] CreateEventDto createRequest, [FromRoute] int eventId)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null) return Unauthorized();
			var result = await _eventService.EditEventAsync(createRequest, eventId, user.Id);
			return result != null ? Ok(result.ToDto()) : BadRequest("Could not create event");
		}

		[HttpDelete("{eventId}")]
		[Authorize]
		public async Task<IActionResult> DeleteEvent([FromRoute] int eventId)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return Unauthorized();

			var result = await _eventService.DeleteEventAsync(eventId);

			return result switch
			{
				DeleteResult.Success => Ok(),
				DeleteResult.NotFound => NotFound($"Event with ID {eventId} not found."),
				DeleteResult.Failed => StatusCode(500, "An error occurred while deleting the event."),
				_ => StatusCode(500, "Unknown error.")
			};
		}

		[HttpGet("{eventId}")]
        //[Authorize]
        public async Task<IActionResult> EventGetById([FromRoute] int eventId)
		{
			var result = await _eventService.GetEventByIdAsync(eventId);
			return result != null ? Ok(result.ToDto()) : BadRequest("Could not get event");
		}

		[HttpGet("{eventId}/AmISignedIn")]
        [Authorize]
        public async Task<ActionResult> EventSigninStateCheck([FromRoute] int eventId)
        {
            var user = await _userManager.GetUserAsync(User);
			var userType = User.FindFirst(ClaimTypes.Role)?.Value;
			if (user == null || userType != UserType.User.ToString()) // Check if user is actually "User" and not "Business Client"
				return Unauthorized();

			var result = await _eventService.CheckIfUserInSignInList(eventId, (user as User)!);
            return Ok(result);
        }
		[HttpPost("{eventId}/enroll")]
		[Authorize]
		public async Task<ActionResult> EventSignin([FromRoute] int eventId)
		{
			var user = await _userManager.GetUserAsync(User);
			var userType = User.FindFirst(ClaimTypes.Role)?.Value;
			if (user == null || userType != UserType.User.ToString()) // Check if user is actually "User" and not "Business Client"
				return Unauthorized();

			var result = await _eventService.AddUserToEventAsync(eventId, (user as User)!);
			return result ? Ok(result) : BadRequest("Could not sign in to event");
		}

		[HttpPost("{eventId}/withdraw")]
		[Authorize]
		public async Task<ActionResult> EventSignout([FromRoute] int eventId)
		{
            var user = await _userManager.GetUserAsync(User);
			var userType = User.FindFirst(ClaimTypes.Role)?.Value;
			if (user == null || userType != UserType.User.ToString()) // Check if user is actually "User" and not "Business Client"
				return Unauthorized();

			var result = await _eventService.RemoveUserFromEventAsync(eventId, (user as User)!);
            return result ? Ok(result) : BadRequest("Could not sign out from event");
        }

        [HttpGet("")]
        public async Task<IActionResult> GetFilteredEvents([FromQuery] string? filter)
        {
            var filteredResults = await _eventService.GetEventsWithFilterAsync(filter);
            return Ok(filteredResults.Select(result => result.ToOverviewDto()));
        }

        [HttpGet("signedUpEvents")]
        [Authorize]
        public async Task<IActionResult> GetSignedUpEvents()
        {
            var emailJwt = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (emailJwt is null) return BadRequest("No email found in claims.");

            var signedUpEvents = await _eventService.GetSignedUpEventsAsync(emailJwt);
            if (signedUpEvents is null) return BadRequest("Wrong email.");

            return Ok(signedUpEvents.Select(result => result.ToDto()));
        }

        [HttpGet("organizedEvents")]
        [Authorize]
        public async Task<IActionResult> GetOrganizedEvents()
        {
            var emailJwt = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (emailJwt is null) return BadRequest("No email found in claims.");

            var organizedEvents = await _eventService.GetOrganizedEventsAsync(emailJwt);
            if (organizedEvents is null) return BadRequest("Wrong email.");

            return Ok(organizedEvents.Select(result => result.ToDto()));
        }

        [HttpGet("sponsoredEvents")]
        [Authorize]
        public async Task<IActionResult> GetSponsoredEvents()
        {
            var emailJwt = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (emailJwt is null) return BadRequest("No email found in claims.");

            var sponsoredEvents = await _eventService.GetSponsoredEventsAsync(emailJwt);
            if (sponsoredEvents is null) return BadRequest("Wrong email.");

            return Ok(sponsoredEvents.Select(result => result.ToDto()));
        }
    }
}
