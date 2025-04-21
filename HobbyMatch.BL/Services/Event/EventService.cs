using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.Event
{
	public class EventService(IEventRepository eventRepository) : IEventService
	{
		private readonly IEventRepository _eventRepository = eventRepository;

		public async Task<bool> AddUserToEventAsync(int eventId, User user)
		{
			return await _eventRepository.AddUserToEventAsync(eventId, user);
		}

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

		public async Task<IEnumerable<HobbyMatch.Domain.Entities.Event>> GetEventsWithFilter(string? filter)
		{
			return await _eventRepository.GetEventsWithFilter(filter);
		}

		public async Task<bool> RemoveUserFromEventAsync(int eventId, User user)
		{
			return  await _eventRepository.RemoveUserFromEventAsync(eventId, user);
		}
	}
}
