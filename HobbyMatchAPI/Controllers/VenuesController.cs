using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.BL.ResultEnums;
using HobbyMatch.BL.Services.Venues;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class VenuesController : ControllerBase
{
    private readonly UserManager<Organizer> _userManager;
    private readonly IVenueService _venueService;

    public VenuesController(IVenueService venueService, UserManager<Organizer> userManager)
    {
        _venueService = venueService;
        _userManager = userManager;
    }

    [HttpGet("{venueId}")]
    public async Task<IActionResult> GetVenueDetails([FromRoute] int venueId)
    {
        var venue = await _venueService.GetVenueByIdAsync(venueId);
        return venue == null ? NotFound() : Ok(venue.ToDetailsDto());
    }
	[HttpDelete("{venueId}")]
	[Authorize]
	public async Task<IActionResult> DeleteEvent([FromRoute] int venueId)
	{
		var user = await _userManager.GetUserAsync(User);
		if (user == null)
			return Unauthorized();

		var result = await _venueService.DeleteVenueAsync(venueId);

		return result switch
		{
			DeleteResult.Success => Ok(),
			DeleteResult.NotFound => NotFound($"Venue with ID {venueId} not found."),
			DeleteResult.Failed => StatusCode(500, "An error occurred while deleting the venue."),
			_ => StatusCode(500, "Unknown error.")
		};
	}

	[Authorize]
    [HttpPut("{venueId}")]
    public async Task<IActionResult> EditVenue(
        [FromRoute] int venueId,
        [FromBody] UpdateVenueDto updateVenueDto
    )
    {
        var result = await _venueService.EditVenueAsync(venueId, updateVenueDto);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("client")]
    public async Task<IActionResult> GetClientVenues()
    {
        var user = await _userManager.GetUserAsync(User);
        var userType = User.FindFirst(ClaimTypes.Role)?.Value;
        if (user == null || userType != UserType.BussinessClient.ToString())
            return Unauthorized();
        var venues = await _venueService.GetClientVenuesAsync(user.Id);
        return Ok(venues.Select(v => v.ToDto()));
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredVenues([FromQuery] [Length(2, 50)] string filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var venues = await _venueService.GetFilteredVenuesAsync(filter);
        return Ok(venues.Select(v => v.ToDto()));
    }

    [HttpGet]
    public async Task<IActionResult> GetVenuesAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var Venues = await _venueService.GetVenuesAsync();
        return Ok(Venues.Select(venue => venue.ToDto()));
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> CreateVenue([FromBody] CreateVenueDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.GetUserAsync(User);
        var userType = User.FindFirst(ClaimTypes.Role)?.Value;
        if (user == null || userType != UserType.BussinessClient.ToString())
            return Unauthorized();
        var venue = await _venueService.CreateVenue(request, user.Id);
        return Ok(venue.ToDetailsDto());
    }
}
