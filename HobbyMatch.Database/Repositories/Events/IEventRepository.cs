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


        Task<List<Event>> GetEventsWithFilterAsync(string? filter);

        Task<List<Event>?> GetSignedUpEventsAsync(string userEmail);

        Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail);

        Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail);
    }
}
