using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Domain.Requests;
using Microsoft.AspNetCore.WebUtilities;

namespace HobbyMatch.App.Services.Venues;

public class VenueApiService : IVenueApiService
{
    private readonly HttpClient _httpClient;
    private readonly HttpClient _unauthorizedClient;
    // TODO: Change it later
    private readonly string _baseUrlForUnauthenticatedClient = "/api/v1";

    public VenueApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
        _unauthorizedClient = httpClientFactory.CreateClient("AuthClient");
    }

    public async Task<PaginatedData<VenueDto>> GetClientVenuesAsync(PaginationParameters paginationParameters)
    {
        var uri = QueryHelpers.AddQueryString("venues/client", new Dictionary<string, string?>
        {
            ["PageNumber"] = paginationParameters.PageNumber.ToString(),
            ["PageSize"] = paginationParameters.PageSize.ToString()
        });

        var result = await _httpClient.GetFromJsonAsync<PaginatedData<VenueDto>>(uri);

        return result ?? PaginatedData<VenueDto>.Empty();
    }

    public async Task<VenueDetailsDto?> GetVenueByIdAsync(int venueId)
    {
        var result = await _unauthorizedClient.GetFromJsonAsync<VenueDetailsDto>($"{_baseUrlForUnauthenticatedClient }/venues/{venueId}");
        return result;
    }

    public async Task<VenueDetailsDto?> CreateVenueAsync(CreateVenueRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/venues/create", request);
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<VenueDetailsDto>();

        return null;
    }
    public async Task<bool> UpdateVenueAsync(UpdateVenueDto request, int venueId)
    {
        var response = await _httpClient.PutAsJsonAsync($"/venues/edit/{venueId}", request);
        return response.IsSuccessStatusCode;
    }

    public async Task<PaginatedData<VenueDto>> GetFilteredVenues(string? filter,
        PaginationParameters paginationParameters)
    {
        var uri = QueryHelpers.AddQueryString("venues/filtered", new Dictionary<string, string?>
        {
            ["filter"] = filter,
            ["PageNumber"] = paginationParameters.PageNumber.ToString(),
            ["PageSize"] = paginationParameters.PageSize.ToString()
        });

        var result = await _unauthorizedClient.GetFromJsonAsync<PaginatedData<VenueDto>>(uri);
        return result ?? PaginatedData<VenueDto>.Empty();
    }
}