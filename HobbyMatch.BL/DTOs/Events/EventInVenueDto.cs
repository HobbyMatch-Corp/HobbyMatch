using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Events
{
    public record EventInVenueDto
    (
        string Id,
        string Title,
        OrganizerDto EventOrganizer,
        string StartTime
    );
}
public static partial class EventExtensions
{
    public static EventInVenueDto ToEventInVenueDto(this Event ev) => new(
            $"{ev.Id}",
            ev.Name,
            new OrganizerDto($"{ev.OrganizerId}", ev.Organizer.UserName ?? ""),
            $"{ev.StartTime}"
        );
}
