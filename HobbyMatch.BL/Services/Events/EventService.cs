using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.Events
{
	public class EventService(IEventRepository eventRepository) : IEventService
	{
		private readonly IEventRepository _eventRepository = eventRepository;

		public async Task<bool> AddUserToEventAsync(int eventId, User user)
		{
			var ev = await _eventRepository.GetEventByIdAsync(eventId);
			if (ev == null || ev.SignUpList == null) return false;

			if (ev.SignUpList.Any(u => u.Id == user.Id)) return false;

			if (ev.SignUpList.Count >= ev.MaxUsers) return false;

			ev.SignUpList.Add(user);
			await _eventRepository.SaveChangesAsync();
			return true;
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
			var ev = await _eventRepository.GetEventByIdAsync(eventId);
			if (ev == null || ev.SignUpList == null) return false;

			var existing = ev.SignUpList.FirstOrDefault(u => u.Id == user.Id);
			if (existing == null) return false;

			ev.SignUpList.Remove(existing);
			await _eventRepository.SaveChangesAsync();
			return true;
		}
	}
}
