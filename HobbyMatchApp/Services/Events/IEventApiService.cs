using HobbyMatch.BL.DTOs.Event;

namespace HobbyMatch.App.Services.Events
{
    public interface IEventApiService
    {
        Task<bool?> EventSigninAsync(string eventId);
        Task<bool?> EventSignoutAsync(string eventId);

        Task<List<EventDto>?> GetFilteredEvents(string? filter);
    }
}
