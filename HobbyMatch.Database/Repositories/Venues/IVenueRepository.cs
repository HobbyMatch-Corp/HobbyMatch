using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Venues;

public interface IVenueRepository
{
    public Task<Venue?> GetVenueByIdAsync(int venueId);

    public Task<PaginatedData<Venue>> GetBusinessClientVenuesAsync(int bussinessClientId,
        PaginationParameters paginationParams);

    public Task<PaginatedData<Venue>> GetFilteredVenuesAsync(string filter,
        PaginationParameters paginationParams);

    public Task AddVenueAsync(Venue venue);
}