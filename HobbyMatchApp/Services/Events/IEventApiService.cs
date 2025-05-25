using HobbyMatch.BL.DTOs.Events;

namespace HobbyMatch.App.Services.Events
{
    public interface IEventApiService
    {
        Task<bool> EventSigninAsync(int eventId);
        Task<bool> EventSignoutAsync(int eventId);
        Task<bool> AmISignedInAsync(int eventId);

		Task<EventDto?> CreateEventAsync(CreateEventDto eventRequest);
        Task<EventDto?> GetEventAsync(int eventId);
        Task<EventDto?> EditEventAsync(CreateEventDto eventRequest, int eventId);

        Task<List<EventOverviewDto>?> GetFilteredEvents(string? filter);

        Task<List<EventDto>?> GetSignedUpEventsAsync();

        Task<List<EventDto>?> GetOrganizedEventsAsync();

        Task<List<EventDto>?> GetSponsoredEventsAsync();

    }
}
