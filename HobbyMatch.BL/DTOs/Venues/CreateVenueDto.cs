using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Venues;

public record CreateVenueDto(
    string Name,
    string Description,
    string Address,
    Location Location
    );