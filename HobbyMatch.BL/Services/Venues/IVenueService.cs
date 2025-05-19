using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.BL.Services.Venues;

public interface IVenueService
{
    public Task<Venue?> GetVenueByIdAsync(int venueId);

    public Task<PaginatedData<Venue>> GetClientVenuesAsync(int businessClientId, PaginationParameters paginationParams);

    public Task<PaginatedData<Venue>> GetFilteredVenuesAsync(string filter, PaginationParameters paginationParams);

    public Task<Venue> CreateVenue(CreateVenueRequest createRequest,int businessClientId);
	public Task<bool> EditVenueAsync(int venueId, UpdateVenueDto updateVenueDto);
}