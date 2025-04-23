using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Events
{
    public interface IEventRepository
    {
        Task<Event?> GetEventByIdAsync(int eventId);

        Task<bool> AddUserToEventAsync(int eventId, User user);

        Task<Event?> AddEvent(Event newEvent);

        Task<bool> RemoveUserFromEventAsync(int eventId, User user);

        Task<List<Event>> GetEventsWithFilter(string? filter);

        Task<List<Event>?> GetSignedUpEvents(string userEmail);

        Task<List<Event>?> GetOrganizedEvents(string organizerEmail);

        Task<List<Event>?> GetSponsoredEvents(string businessClientEmail);
    }
}
