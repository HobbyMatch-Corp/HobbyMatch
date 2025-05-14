using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Venues;

public record VenueDto(
    int Id,
    string Name,
    string Address,
    int MaxUsers
    );

public static partial class VenueExtensions
{

    public static VenueDto ToDto(this Venue venue)
    {
        return new VenueDto(venue.Id, venue.Name, venue.Address, venue.MaxUsers);
    }
}
