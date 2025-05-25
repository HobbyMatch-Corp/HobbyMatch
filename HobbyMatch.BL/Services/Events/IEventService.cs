using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Events
{
    public interface IEventService
    {
		Task<bool> AddUserToEventAsync(int eventId, User user);
		Task<bool> CheckIfUserInSignInList(int eventId, User user);
		Task<IEnumerable<Event>> GetEventsWithFilterAsync(string? filter);
		Task<bool> RemoveUserFromEventAsync(int eventId, User user);
        Task<Event?> CreateEventAsync(CreateEventDto dto, int organizerId);

        Task<List<Event>?> GetSignedUpEventsAsync(string userEmail);

        Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail);

        Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail);
		Task<Event?> EditEventAsync(CreateEventDto createRequest, int eventId, int id);
    }
}
