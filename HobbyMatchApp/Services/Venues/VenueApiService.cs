using HobbyMatch.BL.DTOs.Venues;
using Microsoft.AspNetCore.WebUtilities;

namespace HobbyMatch.App.Services.Venues;

public class VenueApiService : IVenueApiService
{
    private readonly HttpClient _httpClient;
    private readonly HttpClient _unauthorizedClient;

    public VenueApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
        _unauthorizedClient = httpClientFactory.CreateClient("AuthClient");
    }

    public async Task<VenueDetailsDto?> GetVenueByIdAsync(int venueId)
    {
        var result = await _unauthorizedClient.GetFromJsonAsync<VenueDetailsDto>(
            $"venues/{venueId}"
        );
        return result;
    }

    public async Task<VenueDetailsDto?> CreateVenueAsync(CreateVenueDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("venues", request);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<VenueDetailsDto>();

        return null;
    }

    public async Task<bool> UpdateVenueAsync(UpdateVenueDto request, int venueId)
    {
        var x = _httpClient.BaseAddress;
        var response = await _httpClient.PutAsJsonAsync($"venues/{venueId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<VenueDto>> GetFilteredVenues(string? filter)
    {
        var uri = QueryHelpers.AddQueryString(
            "venues/filtered",
            new Dictionary<string, string?> { ["filter"] = filter }
        );

        var result = await _unauthorizedClient.GetFromJsonAsync<List<VenueDto>>(uri);
        return result ?? [];
    }

    public async Task<IEnumerable<VenueDto>> GetVenuesAsync()
    {
        var venues = await _unauthorizedClient.GetFromJsonAsync<IEnumerable<VenueDto>>("venues");
        return venues ?? [];
    }

    public async Task<List<VenueDto>> GetClientVenuesAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<VenueDto>>("venues/client");

        return result ?? [];
    }
}
