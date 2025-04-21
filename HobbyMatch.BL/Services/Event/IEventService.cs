using HobbyMatch.BL.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.Event
{
	public interface IEventService
	{
		Task<HobbyMatch.Domain.Entities.Event?> CreateEventAsync(CreateEventDto dto, int organizerId);
		Task<HobbyMatch.Domain.Entities.Event?> EditEventAsync(CreateEventDto createDto, int eventId, int id);
	}
}
