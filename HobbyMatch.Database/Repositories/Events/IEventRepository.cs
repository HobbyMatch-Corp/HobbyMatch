using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Events
{
    public interface IEventRepository
    {
        Task<Event?> GetEventByIdAsync(int eventId);
        Task<Event?> AddEvent(Event newEvent);
        Task<List<Event>> GetEventsWithFilter(string? filter);
		Task SaveChangesAsync();
	}
}
