using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Events;

public record EventOverviewDto(
    int Id,
    string Name,
    DateTime StartTime,
    DateTime EndTime,
    LocationNullable Location,
    float Price,
    int MaxUsers,
    ParticipantDto[]? Participants
);

public static partial class EventExtensions
{
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