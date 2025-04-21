using HobbyMatch.BL.DTOs.Event;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
using Microsoft.Extensions.Logging;

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

		public async Task<EventDto?> CreateEventAsync(CreateEventRequest eventRequest)
		{
			EventDto? success = null;
			var response = await _httpClient.PostAsJsonAsync("api/events/create", eventRequest);
			if (response.IsSuccessStatusCode)
			{
				success = await response.Content.ReadFromJsonAsync<EventDto>();
			}
			return success;
		}
        public async Task<EventDto?> GetEventAsync(int eventId)
		{
			EventDto? success = null;
			var response = await _httpClient.GetAsync($"api/events/{eventId}");
			if (response.IsSuccessStatusCode)
			{
				success = await response.Content.ReadFromJsonAsync<EventDto>();
			}
			return success;
		}
        public async Task<EventDto?> EditEventAsync(CreateEventRequest eventRequest, int eventId)
		{
			EventDto? success = null;
			var response = await _httpClient.PutAsJsonAsync($"api/events/edit/{eventId}", eventRequest);
			if (response.IsSuccessStatusCode)
			{
				success = await response.Content.ReadFromJsonAsync<EventDto>();
			}
			return success;
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
