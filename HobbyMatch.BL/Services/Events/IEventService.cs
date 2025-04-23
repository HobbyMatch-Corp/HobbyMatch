using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Events
{
    public interface IEventService
    {
        Task<Event?> CreateEventAsync(CreateEventDto dto, int organizerId);

        Task<List<Event>?> GetSignedUpEvents(User user);

        Task<List<Event>?> GetOrganizedEvents(Organizer organizer);

        Task<List<Event>?> GetSponsoredEvents(BusinessClient businessClient);
    }
}
