using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
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
		public async Task<Domain.Entities.Event?> CreateEventAsync(CreateEventRequest createEventRequest, int organizerId)
		{
			var entity = new Domain.Entities.Event
			{
				Name = createEventRequest.Name,
				Description = createEventRequest.Description,
				StartTime = createEventRequest.StartTime,
				EndTime = createEventRequest.EndTime,
				Location = createEventRequest.Location,
				Price = createEventRequest.Price,
				MaxUsers = createEventRequest.MaxUsers,
				MinUsers = createEventRequest.MinUsers,
				OrganizerId = organizerId
			};

			var result = await _eventRepository.AddEvent(entity);
			return result;
		}

		public async Task<Domain.Entities.Event?> EditEventAsync(CreateEventRequest createEventRequest, int eventId, int organizerId)
		{
			var eventToEdit = await _eventRepository.GetEventByIdAsync(eventId);

			if (eventToEdit == null || eventToEdit.OrganizerId != organizerId)
			{
				return null;
			}

			eventToEdit.Name = createEventRequest.Name;
			eventToEdit.Description = createEventRequest.Description;
			eventToEdit.StartTime = createEventRequest.StartTime;
			eventToEdit.EndTime = createEventRequest.EndTime;
			eventToEdit.Location = createEventRequest.Location;
			eventToEdit.Price = createEventRequest.Price;

			await _eventRepository.UpdateEventAsync(eventToEdit); // Assuming this method exists

			return eventToEdit;
		}

	}
}
