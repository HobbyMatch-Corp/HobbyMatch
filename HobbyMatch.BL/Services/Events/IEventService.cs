using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Events
{
    public interface IEventService
    {
        Task<Event?> CreateEventAsync(CreateEventDto dto, int organizerId);

        Task<List<Event>?> GetSignedUpEvents(string userEmail);

        Task<List<Event>?> GetOrganizedEvents(string organizerEmail);

        Task<List<Event>?> GetSponsoredEvents(string businessClientEmail);
    }
}
