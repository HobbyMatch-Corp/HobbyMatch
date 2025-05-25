using HobbyMatch.BL.DTOs.Venues;

namespace HobbyMatch.App.Services.Venues;

public interface IVenueApiService
{
    public Task<List<VenueDto>> GetClientVenuesAsync();
    public Task<VenueDetailsDto?> GetVenueByIdAsync(int venueId);

    public Task<List<VenueDto>> GetFilteredVenues(string? filter);

    public Task<IEnumerable<VenueDto>> GetVenuesAsync();
    public Task<VenueDetailsDto?> CreateVenueAsync(CreateVenueDto request);
    public Task<bool> UpdateVenueAsync(UpdateVenueDto request, int venueId);
}
