using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.Events
{
	public interface IEventService
	{
		Task<HobbyMatch.Domain.Entities.Event?> CreateEventAsync(CreateEventRequest createRequest, int organizerId);
		Task<HobbyMatch.Domain.Entities.Event?> EditEventAsync(CreateEventRequest createRequest, int eventId, int id);
	}
}
