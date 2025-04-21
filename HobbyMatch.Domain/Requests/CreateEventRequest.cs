using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Domain.Requests;

public record CreateEventRequest(string Name, string Description, DateTime StartTime, DateTime EndTime, LocationNullable Location, float Price, int MaxUsers, int MinUsers)
{
	public static Event FromEventRequest(CreateEventRequest eventRequest, User user)
	{
		return new Event
		{
			Name = eventRequest.Name,
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