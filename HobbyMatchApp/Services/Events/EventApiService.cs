using HobbyMatch.BL.DTOs.Event;

namespace HobbyMatch.App.Services.Events
{
    public class EventApiService : IEventApiService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _unauthorizedClient;
        public EventApiService(IHttpClientFactory httpClientFactory) 
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
            _unauthorizedClient = httpClientFactory.CreateClient("AuthClient");

        }
        public async Task<bool?> EventSigninAsync(string eventId)
        {
            bool success = false;
            var response = await _httpClient.PostAsJsonAsync("api/events/signin", new EventSignDto(eventId));
            if (response.IsSuccessStatusCode)
            {
                success = await response.Content.ReadFromJsonAsync<bool>();
            }
            return success;
        }

        public async Task<bool?> EventSignoutAsync(string eventId)
        {
            bool success = false;
            var response = await _httpClient.PostAsJsonAsync("api/events/signout", new EventSignDto(eventId));
            if (response.IsSuccessStatusCode)
            {
                success = await response.Content.ReadFromJsonAsync<bool>();
            }
            return success;
        }

        public async Task<List<EventDto>?> GetFilteredEvents(string? filter)
        {
            var response = await _unauthorizedClient.GetFromJsonAsync<List<EventDto>>($"api/events/events?filter={filter}");
            return response;
        }
    }
}
