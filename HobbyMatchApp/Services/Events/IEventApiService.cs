using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.App.Services.Events
{
    public interface IEventApiService
    {
        Task<bool> EventSigninAsync(int eventId);
        Task<bool> EventSignoutAsync(int eventId);
        Task<bool> AmISignedInAsync(int eventId);

		Task<EventDto?> CreateEventAsync(CreateEventRequest eventRequest);
        Task<EventDto?> GetEventAsync(int eventId);
        Task<EventDto?> EditEventAsync(CreateEventRequest eventRequest, int eventId);

        Task<List<EventDto>?> GetFilteredEvents(string? filter);

        Task<List<EventDto>?> GetSignedUpEventsAsync();

        Task<List<EventDto>?> GetOrganizedEventsAsync();

        Task<List<EventDto>?> GetSponsoredEventsAsync();

    }
}
