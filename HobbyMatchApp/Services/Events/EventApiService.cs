using HobbyMatch.BL.DTOs.Events;

namespace HobbyMatch.App.Services.Events
{
    public class EventApiService : IEventApiService
    {
        private readonly HttpClientUtils _httpClientUtils;
        public EventApiService(HttpClientUtils httpClientUtils) 
        {
            _httpClientUtils = httpClientUtils;
		}

		public async Task<EventDto?> CreateEventAsync(CreateEventDto eventRequest)
		{
			return await _httpClientUtils.PostAsyncSafe<CreateEventDto, EventDto>("events", eventRequest); ;
		}

        public async Task<EventDto?> GetEventAsync(int eventId)
		{
			return await _httpClientUtils.GetAsyncSafe<EventDto>($"events/{eventId}");
        }

        public async Task<EventDto?> EditEventAsync(CreateEventDto eventRequest, int eventId)
		{
			return await _httpClientUtils.PutAsyncSafe<CreateEventDto, EventDto>($"events/{eventId}", eventRequest);
        }

		public async Task<bool> EventSigninAsync(int eventId)
        {
            return await _httpClientUtils.PostAsyncSafe<object, bool>($"events/{eventId}/enroll", new { }); ;
        }

        public async Task<bool> EventSignoutAsync(int eventId)
        {
            return await _httpClientUtils.PostAsyncSafe<object, bool>($"events/{eventId}/withdraw", new { }); ;
        }

		public async Task<bool> AmISignedInAsync(int eventId)
		{
            return await _httpClientUtils.GetAsyncSafe<bool>($"events/{eventId}/amISignedIn");
        }

		public async Task<List<EventOverviewDto>?> GetFilteredEvents(string? filter)
        {
            return await _httpClientUtils.GetAsyncSafe<List<EventOverviewDto>>($"events/?filter={filter}", unauthorized: true);
        }

        public async Task<List<EventDto>?> GetOrganizedEventsAsync()
        {
            return await _httpClientUtils.GetAsyncSafe<List<EventDto>?>("events/organizedEvents");
        }

        public async Task<List<EventDto>?> GetSignedUpEventsAsync()
        {
            return await _httpClientUtils.GetAsyncSafe<List<EventDto>?>("events/signedUpEvents");
        }

        public async Task<List<EventDto>?> GetSponsoredEventsAsync()
        {
            return await _httpClientUtils.GetAsyncSafe<List<EventDto>?>("events/sponsoredEvents");
        }
    }
}
