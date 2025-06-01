using HobbyMatch.BL.DTOs.Events;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;

namespace HobbyMatch.App.Services
{
    public class HttpClientUtils(IHttpClientFactory httpClientFactory)
    {
        private HttpClient? _httpClient;
        private HttpClient? _unauthorizedClient;

        public async Task<T?> GetAsyncSafe<T>(string url, bool unauthorized = false)
        {
            try
            {
                var response = await GetClient(unauthorized).GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await ReturnRespone<T>(response);
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
                var response = await GetClient(unauthorized).PostAsJsonAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return await ReturnRespone<Tout>(response);
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
                var response = await GetClient(unauthorized).PutAsJsonAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return await ReturnRespone<Tout>(response);
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

        public async Task<bool> DeleteAsyncSafe(string url, bool unauthorized = false)
        {
            try
            {
                var response = await GetClient(unauthorized).DeleteAsync(url);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyjątek POST: {ex.Message}");
                return default;
            }
        }

        private async Task<T?> ReturnRespone<T>(HttpResponseMessage response)
        {
            if (typeof(T) == typeof(HttpResponseMessage))
                return (T)(object)response!;
            else
                return await response.Content.ReadFromJsonAsync<T>();
        }

        private HttpClient GetClient(bool unauthorized)
        {
            if (unauthorized)
            {
                if (_unauthorizedClient is null)
                    _unauthorizedClient = httpClientFactory.CreateClient("AuthClient");
                return _unauthorizedClient;
            }
            else
            {
                if (_httpClient is null)
                    _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
                return _httpClient;
            }
        }

    }

}
