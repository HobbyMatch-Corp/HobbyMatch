using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.BL.ResultEnums;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Venues;

public interface IVenueService
{
    public Task<Venue?> GetVenueByIdAsync(int venueId);

    public Task<List<Venue>> GetClientVenuesAsync(int businessClientId);

    public Task<List<Venue>> GetFilteredVenuesAsync(string filter);

    public Task<List<Venue>> GetVenuesAsync();
    public Task<Venue> CreateVenue(CreateVenueDto createRequest, int businessClientId);
    public Task<bool> EditVenueAsync(int venueId, UpdateVenueDto updateVenueDto);
	Task<DeleteResult> DeleteVenueAsync(int eventId);

}
