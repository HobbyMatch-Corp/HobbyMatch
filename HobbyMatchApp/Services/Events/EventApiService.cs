using HobbyMatch.BL.DTOs.Event;

namespace HobbyMatch.App.Services.Events
{
    public class EventApiService : IEventApiService
    {
        private readonly HttpClient _httpClient;
        public EventApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthClient");
        }
        public async Task<bool?> EventSigninAsync(string EventId)
        {
            bool success = false;
            var response = await _httpClient.PostAsJsonAsync("api/eventsignin", new EventSignDto(EventId));
            if (response.IsSuccessStatusCode)
            {
                success = await response.Content.ReadFromJsonAsync<bool>();
            }
            return success;
        }

        public async Task<bool?> EventSignoutAsync(string EventId)
        {
            bool success = false;
            var response = await _httpClient.PostAsJsonAsync("api/eventsignout", new EventSignDto(EventId));
            if (response.IsSuccessStatusCode)
            {
                success = await response.Content.ReadFromJsonAsync<bool>();
            }
            return success;
        }
    }
}
