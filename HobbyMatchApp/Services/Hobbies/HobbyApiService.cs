using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.BL.DTOs.Hobbies;

namespace HobbyMatch.App.Services.Hobbies
{
    public class HobbyApiService : IHobbyApiService
    {
        private readonly HttpClient _unauthorizedClient;

        public HobbyApiService(IHttpClientFactory httpClientFactory)
        {
            _unauthorizedClient = httpClientFactory.CreateClient("AuthClient");
        }

        public async Task<ICollection<HobbyDto>> GetHobbiesAsync()
        {
            var response = await _unauthorizedClient.GetFromJsonAsync<List<HobbyDto>>("hobbies");
            return response;
        }
    }
}
