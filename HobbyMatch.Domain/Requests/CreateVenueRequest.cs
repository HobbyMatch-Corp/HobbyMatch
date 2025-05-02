using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Domain.Requests;

public record CreateVenueRequest(
    int Id,
    string Name,
    string Address,
    int MaxUsers,
    decimal Price,
    Location Location,
    string Description);