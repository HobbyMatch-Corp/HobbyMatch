using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Venues;

public record VenueDetailsDto(
    int Id,
    string Name,
    string Address,
    int MaxUsers,
    decimal Price,
    Location Location,
    string Description,
    int BusinessClientId,
    string ClientName
);