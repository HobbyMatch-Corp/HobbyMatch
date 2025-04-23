using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Events
{
    public interface IEventService
    {
        Task<Event?> CreateEventAsync(CreateEventDto dto, int organizerId);

        Task<List<Event>?> GetSignedUpEventsAsync(string userEmail);

        Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail);

        Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail);
    }
}
