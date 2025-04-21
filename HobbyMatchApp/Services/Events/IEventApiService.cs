using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.App.Services.Events
{
    public interface IEventApiService
    {
        Task<bool?> EventSigninAsync(string eventId);
        Task<bool?> EventSignoutAsync(string eventId);
        Task<EventDto?> CreateEventAsync(CreateEventRequest eventRequest);
        Task<EventDto?> GetEventAsync(int eventId);
        Task<EventDto?> EditEventAsync(CreateEventRequest eventRequest, int eventId);

        Task<List<EventDto>?> GetFilteredEvents(string? filter);
    }
}
