using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Venues;

public record VenueDetailsDto(
    string Id,
    string Name,
    string Description,
    string Address,
    Location Location,
    OrganizerDto Owner,
    List<EventInVenueDto> Events
);

public static partial class VenueExtensions
{
    public static VenueDetailsDto ToDetailsDto(this Venue venue)
    {
        return new VenueDetailsDto($"{venue.Id}", venue.Name, venue.Description, venue.Address, venue.Location,
            new OrganizerDto($"{venue.BusinessClientId}", venue.BusinessClient.UserName ?? ""), venue.Events.Select(v => v.ToEventInVenueDto()).ToList());
    }
}