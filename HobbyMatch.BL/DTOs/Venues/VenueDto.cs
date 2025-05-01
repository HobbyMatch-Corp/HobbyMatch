using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Venues
{
    public record VenueDto(
    int Id,
    string Name,
    string Address,
    Location Location,
    decimal Price,
    int MaxUsers
    );
}
