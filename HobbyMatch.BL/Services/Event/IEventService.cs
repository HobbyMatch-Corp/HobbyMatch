using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Event
{
    public interface IEventService
    {
        Task<HobbyMatch.Domain.Entities.Event?> CreateEventAsync(CreateEventDto dto, int organizerId);

        Task<List<Domain.Entities.Event>?> GetSignedUpEvents(User user);

        Task<List<Domain.Entities.Event>?> GetOrganizedEvents(Organizer organizer);

        Task<List<Domain.Entities.Event>?> GetSponsoredEvents(Domain.Entities. BusinessClient businessClient);
    }
}
