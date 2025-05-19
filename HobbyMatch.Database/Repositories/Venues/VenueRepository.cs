using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Venues;

public class VenueRepository : IVenueRepository
{
    private readonly AppDbContext _dbContext;

    public VenueRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Venue?> GetVenueByIdAsync(int venueId)
    {
        var venue = await _dbContext.Venues.FindAsync(venueId);
        return venue;
    }

    public Task<PaginatedData<Venue>> GetBusinessClientVenuesAsync(int bussinessClientId,
        PaginationParameters paginationParams)
    {
        return _dbContext.Venues.Where(venue => venue.BusinessClientId == bussinessClientId)
            .ToPaginatedData(paginationParams);
    }

    public Task<PaginatedData<Venue>> GetFilteredVenuesAsync(string filter, PaginationParameters paginationParams)
    {
        return _dbContext.Venues.Where(venue => venue.Name.Contains(filter))
            .ToPaginatedData(paginationParams);
    }

    public async Task AddVenueAsync(Venue venue)
    {
        await _dbContext.AddAsync(venue);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}