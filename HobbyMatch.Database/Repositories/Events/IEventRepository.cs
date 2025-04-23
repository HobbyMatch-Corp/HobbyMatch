using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Events
{
    public interface IEventRepository
    {
        Task<Event?> GetEventByIdAsync(int eventId);

        Task<bool> AddUserToEventAsync(int eventId, User user);

        Task<Event?> AddEvent(Event newEvent);

        Task<bool> RemoveUserFromEventAsync(int eventId, User user);

        Task<List<Event>> GetEventsWithFilterAsync(string? filter);

        Task<List<Event>?> GetSignedUpEventsAsync(string userEmail);

        Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail);

        Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail);
    }
}
