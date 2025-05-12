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

public static partial class VenueExtensions
{
    public static VenueDetailsDto ToDetailsDto(this Venue venue)
    {
        return new VenueDetailsDto(venue.Id, venue.Name, venue.Address, venue.MaxUsers, venue.Price, venue.Location,
            venue.Description, venue.BusinessClientId, venue.BusinessClient.UserName!);
    }
}