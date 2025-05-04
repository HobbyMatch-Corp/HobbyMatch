using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.BL.DTOs;

namespace HobbyMatch.BL.Extensions
{
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
            ev.Organizer != null ? ev.Organizer.UserName : "",
            ev.SignUpList?.Select((el) => el.ToDto()).ToArray() ?? []
        );
        public static EventOverviewDto ToOverviewDto(this Domain.Entities.Event ev) => new(
            ev.Id,
            ev.Name,
            ev.StartTime,
            ev.EndTime,
            ev.Location,
            ev.Price,
            ev.MaxUsers,
            ev.SignUpList?.Select((el) => el.ToDto()).ToArray() ?? []
        );
    }
}
