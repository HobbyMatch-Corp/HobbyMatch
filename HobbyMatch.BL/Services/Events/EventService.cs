using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Database.Repositories.Events;

namespace HobbyMatch.BL.Services.Events
{
    public class EventService(IEventRepository eventRepository) : IEventService
	{
		private readonly IEventRepository _eventRepository = eventRepository;
		public async Task<Domain.Entities.Event?> CreateEventAsync(CreateEventDto dto, int organizerId)
		{
			var entity = new Domain.Entities.Event
			{
				Name = dto.Name,
				Description = dto.Description,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Location = dto.Location,
				Price = dto.Price,
				MaxUsers = dto.MaxUsers,
				MinUsers = dto.MinUsers,
				OrganizerId = organizerId
			};

			var result = await _eventRepository.AddEvent(entity);
			return result;
		}
	}
}
