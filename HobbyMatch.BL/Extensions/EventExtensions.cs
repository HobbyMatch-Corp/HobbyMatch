using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.BL.DTOs;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Extensions
{
    public static class EventExtensions
    {
        public static EventDto ToDto(this Event ev) => new(
            ev.Id,
            ev.Name,
            ev.Description,
            ev.StartTime,
            ev.EndTime,
            ev.Location,
            ev.Venue ?? null,
            ev.Price,
            ev.MaxUsers,
            ev.MinUsers,
            ev.OrganizerId,
            ev.Organizer?.UserName ?? "",
            ev.SignUpList?.Select((el) => el.ToDto()).ToArray() ?? []
        );
        public static EventOverviewDto ToOverviewDto(this Event ev) => new(
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
