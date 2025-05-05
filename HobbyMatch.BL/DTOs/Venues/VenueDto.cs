namespace HobbyMatch.BL.DTOs.Venues;

public record VenueDto(
    int Id,
    string Name,
    string Address,
    int MaxUsers);
