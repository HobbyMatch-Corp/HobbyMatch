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

        public async Task<List<Event>?> GetOrganizedEvents(Organizer organizer)
        {
            return await _eventRepository.GetOrganizedEvents(organizer);
        }

        public async Task<List<Event>?> GetSignedUpEvents(User user)
        {
            return await _eventRepository.GetSignedUpEvents(user);
        }

        public async Task<List<Event>?> GetSponsoredEvents(BusinessClient businessClient)
        {
            return await _eventRepository.GetSponsoredEvents(businessClient);
        }
    }
}
