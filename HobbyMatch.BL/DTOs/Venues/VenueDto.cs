using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Venues;

public record VenueDto(
    int Id,
    string Name,
    string Address,
    int MaxUsers,
    decimal Price,
    Location Location,
    string Description,
    int BusinessClientId
);

public static class VenueExtensions
{
    public static VenueDto ToDto(this Venue venue)
    {
        return new VenueDto(venue.Id, venue.Name, venue.Address, venue.MaxUsers, venue.Price, venue.Location,
            venue.Description, venue.BusinessClientId);
    }
}