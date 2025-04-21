using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.Identity;
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
		public async Task<Domain.Entities.Event?> CreateEventAsync(CreateEventRequest dto, int organizerId)
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

		public async Task<Domain.Entities.Event?> EditEventAsync(CreateEventRequest dto, int eventId, int userId)
		{
			var eventToEdit = await _eventRepository.GetEventByIdAsync(eventId);

			if (eventToEdit == null || eventToEdit.OrganizerId != userId)
			{
				return null;
			}

			eventToEdit.Name = dto.Name;
			eventToEdit.Description = dto.Description;
			eventToEdit.StartTime = dto.StartTime;
			eventToEdit.EndTime = dto.EndTime;
			eventToEdit.Location = dto.Location;
			eventToEdit.Price = dto.Price;

			await _eventRepository.UpdateEventAsync(eventToEdit); // Assuming this method exists

			return eventToEdit;
		}

	}
}
