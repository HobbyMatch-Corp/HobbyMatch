using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Domain.Requests;

public record CreateVenueDto(
    string Name,
    string Address,
    int MaxUsers,
    decimal Price,
    Location Location,
    string Description);