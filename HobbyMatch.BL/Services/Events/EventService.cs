using HobbyMatch.BL.DTOs.Event;
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

        public async Task<List<Event>?> GetOrganizedEvents(string organizerEmail)
        {
            return await _eventRepository.GetOrganizedEvents(organizerEmail);
        }

        public async Task<List<Event>?> GetSignedUpEvents(string userEmail)
        {
            return await _eventRepository.GetSignedUpEvents(userEmail);
        }

        public async Task<List<Event>?> GetSponsoredEvents(string businessClientEmail)
        {
            return await _eventRepository.GetSponsoredEvents(businessClientEmail);
        }
    }
}
