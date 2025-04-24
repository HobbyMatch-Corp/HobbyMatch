using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.Domain.Requests;

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
			var response = await _httpClient.PostAsJsonAsync("events/create", eventRequest);
			if (response.IsSuccessStatusCode)
			{
				success = await response.Content.ReadFromJsonAsync<EventDto>();
			}
			return success;
		}
        public async Task<EventDto?> GetEventAsync(int eventId)
		{
			EventDto? success = null;
			var response = await _httpClient.GetAsync($"events/{eventId}");
			if (response.IsSuccessStatusCode)
			{
				success = await response.Content.ReadFromJsonAsync<EventDto>();
			}
			return success;
		}
        public async Task<EventDto?> EditEventAsync(CreateEventRequest eventRequest, int eventId)
		{
			EventDto? success = null;
			var response = await _httpClient.PutAsJsonAsync($"events/edit/{eventId}", eventRequest);
			if (response.IsSuccessStatusCode)
			{
				success = await response.Content.ReadFromJsonAsync<EventDto>();
			}
			return success;
		}

		public async Task<bool?> EventSigninAsync(int eventId)
        {
            bool success = false;
            var response = await _httpClient.PostAsJsonAsync("events/signin", new EventSignDto(eventId));
            if (response.IsSuccessStatusCode)
            {
                success = await response.Content.ReadFromJsonAsync<bool>();
            }
            return success;
        }

        public async Task<bool?> EventSignoutAsync(int eventId)
        {
            bool success = false;
            var response = await _httpClient.PostAsJsonAsync("events/signout", new EventSignDto(eventId));
            if (response.IsSuccessStatusCode)
            {
                success = await response.Content.ReadFromJsonAsync<bool>();
            }
            return success;
        }

        public async Task<List<EventDto>?> GetFilteredEvents(string? filter)
        {
            var response = await _unauthorizedClient.GetFromJsonAsync<List<EventDto>>($"events/events?filter={filter}");
            return response;
        }

        public async Task<List<EventDto>?> GetOrganizedEventsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EventDto>?>("events/organizedEvents");
        }

        public async Task<List<EventDto>?> GetSignedUpEventsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EventDto>?>("events/signedUpEvents");
        }

        public async Task<List<EventDto>?> GetSponsoredEventsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EventDto>?>("events/sponsoredEvents");
        }
    }
}
