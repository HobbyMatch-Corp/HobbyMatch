using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Event;

public record EventDto(
    int Id,
    string Name,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    Location Location,
    float Price,
    int MaxUsers,
    int MinUsers,
    int OrganizerId,
    string OrganizerName
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
        ev.Price,
        ev.MaxUsers,
        ev.MinUsers,
        ev.OrganizerId,
        ev.Organizer.UserName ?? ""
    );
}