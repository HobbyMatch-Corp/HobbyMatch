using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Events;

public record EventDto(
    int Id,
    string Name,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    LocationNullable Location,
    Venue? Venue,
    float Price,
    int MaxUsers,
    int MinUsers,
    int? OrganizerId,
    string OrganizerName,
    ParticipantDto[]? Participants
);

public static class EventExtensions
{
    public static EventDto ToDto(this Domain.Entities.Event ev) => new(
        ev.Id,
        ev.Name,
        ev.Description,
        ev.StartTime,
        ev.EndTime,
        ev.Location, 
        ev.Venue,
        ev.Price,
        ev.MaxUsers,
        ev.MinUsers,
        ev.OrganizerId,
        ev.Organizer != null? ev.Organizer.UserName : "",
        ev.SignUpList?.Select((el) => el.ToDto()).ToArray() ?? []
    );
}