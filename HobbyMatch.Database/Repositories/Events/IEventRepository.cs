using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain;

namespace HobbyMatch.Database.Repositories.Events
{
    public interface IEventRepository
    {
        Task<Event?> GetEventWithUsersAsync(int eventId);
        Task<bool> AddUserToEventAsync(int eventId, User user);
        Task<bool> RemoveUserFromEventAsync(int eventId, User user);
    }
}
