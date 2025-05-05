using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Venues;

public static class VenueExtensions
{
    public static VenueDetailsDto ToDetailsDto(this Venue venue)
    {
        return new VenueDetailsDto(venue.Id, venue.Name, venue.Address, venue.MaxUsers, venue.Price, venue.Location,
            venue.Description, venue.BusinessClientId, venue.BusinessClient.UserName!);
    }

    public static VenueDto ToDto(this Venue venue)
    {
        return new VenueDto(venue.Id, venue.Name, venue.Address, venue.MaxUsers);
    }
}