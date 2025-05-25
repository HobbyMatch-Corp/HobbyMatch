using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.BL.Services.Venues;
using HobbyMatch.Database.Common.Pagination;
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

    [Authorize]
	[HttpPut("{venueId}")]
	public async Task<IActionResult> EditVenue([FromRoute] int venueId, [FromBody] UpdateVenueDto updateVenueDto)
	{
		var result = await _venueService.EditVenueAsync(venueId, updateVenueDto);
		return Ok(result);
	}

	[Authorize]
    [HttpGet("client")]
    public async Task<IActionResult> GetClientVenues(
        [FromQuery] PaginationParameters paginationParams)
    {
        var user = await _userManager.GetUserAsync(User);
        var userType = User.FindFirst(ClaimTypes.Role)?.Value;
        if (user == null ||
            userType != UserType.BussinessClient
                .ToString())
            return Unauthorized();
        var paginatedVenues = await _venueService.GetClientVenuesAsync(user.Id, paginationParams);
        return Ok(paginatedVenues.MapItems(venue => venue.ToDto()));
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredVenues([FromQuery] [Length(2, 50)] string filter,
        [FromQuery] PaginationParameters paginationParams)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var paginatedVenues = await _venueService.GetFilteredVenuesAsync(filter, paginationParams);
        return Ok(paginatedVenues.MapItems(venue => venue.ToDto()));
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> CreateVenue([FromBody] CreateVenueDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.GetUserAsync(User);
        var userType = User.FindFirst(ClaimTypes.Role)?.Value;
        if (user == null ||
            userType != UserType.BussinessClient
                .ToString())
            return Unauthorized();
        var venue = await _venueService.CreateVenue(request, user.Id);
        return Ok(venue.ToDetailsDto());
    }
}