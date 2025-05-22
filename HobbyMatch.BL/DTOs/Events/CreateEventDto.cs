using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Events;

public record CreateEventDto(string Title, string Description, DateTime StartTime, DateTime EndTime, LocationNullable Location, float Price, int MaxUsers, int MinUsers, HobbyDto[] Hobbies)
{
	public static Event FromEventRequest(CreateEventDto eventRequest, User user)
	{
		return new Event
		{
			Name = eventRequest.Title,
			Description = eventRequest.Description,
			StartTime = eventRequest.StartTime,
			EndTime = eventRequest.EndTime,
			Location = eventRequest.Location,
			Price = eventRequest.Price,
			MaxUsers = eventRequest.MaxUsers,
			MinUsers = eventRequest.MinUsers,
			OrganizerId = user.Id,
		};
	}
}