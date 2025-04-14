using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Events
{
    public interface IEventRepository
    {
        Task<Event?> GetEventByIdAsync(int eventId);
        Task<bool> AddUserToEventAsync(int eventId, User user);
        Task<bool> RemoveUserFromEventAsync(int eventId, User user);
        Task<List<Event>> GetEventsWithFilter(string? filter);
    }
}
