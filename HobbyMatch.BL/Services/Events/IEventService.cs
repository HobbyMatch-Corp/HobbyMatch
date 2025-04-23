using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.Services.Events
{
    public interface IEventService
    {
		Task<bool> AddUserToEventAsync(int eventId, User user);
		Task<IEnumerable<Event>> GetEventsWithFilter(string? filter);
		Task<bool> RemoveUserFromEventAsync(int eventId, User user);
        Task<Event?> CreateEventAsync(CreateEventDto dto, int organizerId);

        Task<List<Event>?> GetSignedUpEventsAsync(string userEmail);

        Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail);

        Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail);
    }
}
