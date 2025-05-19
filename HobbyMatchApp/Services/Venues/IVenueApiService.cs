using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.App.Services.Venues;

public interface IVenueApiService
{
    public Task<PaginatedData<VenueDto>> GetClientVenuesAsync(PaginationParameters paginationParameters);
    public Task<VenueDetailsDto?> GetVenueByIdAsync(int venueId);

    public Task<PaginatedData<VenueDto>> GetFilteredVenues(string? filter,
        PaginationParameters paginationParameters);

    public Task<VenueDetailsDto?> CreateVenueAsync(CreateVenueRequest request);
    public Task<bool> UpdateVenueAsync(UpdateVenueDto request, int venueId);
}