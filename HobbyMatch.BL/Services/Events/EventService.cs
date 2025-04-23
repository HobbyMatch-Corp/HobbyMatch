using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Events
{
    public class EventService(IEventRepository eventRepository) : IEventService
	{
		private readonly IEventRepository _eventRepository = eventRepository;
		public async Task<Event?> CreateEventAsync(CreateEventDto dto, int organizerId)
		{
			var entity = new Event
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

        public async Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail)
        {
            return await _eventRepository.GetOrganizedEventsAsync(organizerEmail);
        }

        public async Task<List<Event>?> GetSignedUpEventsAsync(string userEmail)
        {
            return await _eventRepository.GetSignedUpEventsAsync(userEmail);
        }

        public async Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail)
        {
            return await _eventRepository.GetSponsoredEventsAsync(businessClientEmail);
        }
    }
}
