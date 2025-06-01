using HobbyMatch.BL.DTOs.Events;
using System.Net.Http;

namespace HobbyMatch.App.Services
{
    public class HttpClientUtils(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
        private readonly HttpClient _unauthorizedClient = httpClientFactory.CreateClient("AuthClient");

        public async Task<T?> GetAsyncSafe<T>(string url, bool unauthorized = false)
        {
            try
            {
                var response = await (unauthorized ? _unauthorizedClient : _httpClient).GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }

                var errorText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Błąd: {(int)response.StatusCode} - {errorText}");

                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyjątek przy GET: {ex.Message}");
                return default;
            }
        }

        public async Task<Tout?> PostAsyncSafe<Tint, Tout>(string url, Tint data, bool unauthorized = false)
        {
            try
            {
                var response = await (unauthorized ? _unauthorizedClient : _httpClient).PostAsJsonAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Tout>();
                }

                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Błąd POST: {(int)response.StatusCode} - {error}");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyjątek POST: {ex.Message}");
                return default;
            }
        }

        public async Task<Tout?> PutAsyncSafe<Tint, Tout>(string url, Tint data, bool unauthorized = false)
        {
            try
            {
                var response = await (unauthorized ? _unauthorizedClient : _httpClient).PutAsJsonAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Tout>();
                }

                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Błąd POST: {(int)response.StatusCode} - {error}");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyjątek POST: {ex.Message}");
                return default;
            }
        }

    }

}
