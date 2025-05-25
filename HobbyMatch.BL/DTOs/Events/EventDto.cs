using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Events;

public record EventDto(
	int Id,
	string Title,
	string Description,
	DateTime StartTime,
	DateTime EndTime,
	LocationNullable Location,
	Venue? Venue,
	float Price,
	int MaxUsers,
	int MinUsers,
	OrganizerDto Organizer,
	ParticipantDto[]? Participants,
	HobbyDto[] Hobbies
);

public static partial class EventExtensions
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
            new OrganizerDto(
            ev.OrganizerId.ToString(),
            ev.Organizer?.UserName ?? ""),
            ev.SignUpList?.Select((el) => el.ToDto()).ToArray() ?? [],
            ev.Hobbies.Select(h => h.ToDto()).ToArray()
        );
}