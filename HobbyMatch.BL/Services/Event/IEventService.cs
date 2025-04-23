using HobbyMatch.BL.DTOs.Event;

namespace HobbyMatch.BL.Services.Event
{
    public interface IEventService
	{
		 Task<HobbyMatch.Domain.Entities.Event?> CreateEventAsync(CreateEventDto dto, int organizerId);
	}
}
