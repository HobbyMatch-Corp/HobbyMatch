using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.App.Services.Events
{
    public interface IEventApiService
    {
        Task<bool?> EventSigninAsync(int eventId);
        Task<bool?> EventSignoutAsync(int eventId);
        Task<EventDto?> CreateEventAsync(CreateEventRequest eventRequest);

        Task<List<EventDto>?> GetFilteredEvents(string? filter);

        Task<List<EventDto>?> GetSignedUpEventsAsync();

        Task<List<EventDto>?> GetOrganizedEventsAsync();

        Task<List<EventDto>?> GetSponsoredEventsAsync();

    }
}
