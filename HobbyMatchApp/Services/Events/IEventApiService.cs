using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.App.Services.Events
{
    public interface IEventApiService
    {
        Task<bool?> EventSigninAsync(string eventId);

        Task<bool?> EventSignoutAsync(string eventId);

        Task<EventDto?> CreateEventAsync(CreateEventRequest eventRequest);

        Task<List<EventDto>?> GetFilteredEvents(string? filter);

        a
    }
}
