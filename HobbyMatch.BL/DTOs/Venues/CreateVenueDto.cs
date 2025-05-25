using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Venues;

public record CreateVenueDto(
    string Name,
    string Address,
    int MaxUsers,
    decimal Price,
    Location Location,
    string Description);