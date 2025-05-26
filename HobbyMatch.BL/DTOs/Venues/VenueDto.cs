using HobbyMatch.Domain.Entities;
using HobbyMatch.BL.DTOs.Organizers;

namespace HobbyMatch.BL.DTOs.Venues;

public record VenueDto(
    string Id,
    string Name,
    string Description,
    string Address,
    Location Location,
    OrganizerDto Owner
    );

public static partial class VenueExtensions
{

    public static VenueDto ToDto(this Venue venue)
    {
        return new VenueDto($"{venue.Id}", venue.Name, venue.Description, venue.Address, venue.Location, new OrganizerDto($"{venue.BusinessClientId}", venue.BusinessClient.UserName ?? ""));
    }
}
