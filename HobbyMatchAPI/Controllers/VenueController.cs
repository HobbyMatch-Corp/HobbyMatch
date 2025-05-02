using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.BL.Services.Venues;
using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Enums;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HobbyMatch.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class VenueController : ControllerBase
{
    private readonly UserManager<Organizer> _userManager;
    private readonly IVenueService _venueService;

    public VenueController(IVenueService venueService, UserManager<Organizer> userManager)
    {
        _venueService = venueService;
        _userManager = userManager;
    }

    [HttpGet("{venueId}")]
    public async Task<IActionResult> GetVenueDetails([FromRoute] int venueId)
    {
        var venue = await _venueService.GetVenueByIdAsync(venueId);
        return venue == null ? NotFound() : Ok(venue.ToDto());
    }

    [Authorize]
    [HttpGet("client")]
    public async Task<IActionResult> GetClientVenues(
        [FromBody] PaginationParameters paginationParams)
    {
        var user = await _userManager.GetUserAsync(User);
        var userType = User.FindFirst("userType")?.Value;
        if (user == null ||
            userType != UserType.BussinessClient
                .ToString())
            return Unauthorized();
        var paginatedVenues = await _venueService.GetClientVenuesAsync(user.Id, paginationParams);
        return Ok(paginatedVenues.MapItems(venue => venue.ToDto()));
    }

    [HttpGet("filtered/{filter}")]
    public async Task<IActionResult> GetFilteredVenues([FromQuery] string filter,
        [FromBody] PaginationParameters paginationParams)
    {
        var paginatedVenues = await _venueService.GetFilteredVenuesAsync(filter, paginationParams);
        return Ok(paginatedVenues.MapItems(venue => venue.ToDto()));
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateVenue([FromBody] CreateVenueRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.GetUserAsync(User);
        var userType = User.FindFirst("userType")?.Value;
        if (user == null ||
            userType != UserType.BussinessClient
                .ToString())
            return Unauthorized();
        var venue = await _venueService.CreateVenue(request, user.Id);
        return Ok(venue);
    }
}