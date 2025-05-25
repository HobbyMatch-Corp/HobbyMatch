using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        var venue = await _dbContext
            .Venues.Include(v => v.BusinessClient)
            .FirstOrDefaultAsync(v => v.Id == venueId);
        return venue;
    }

    public Task<List<Venue>> GetBusinessClientVenuesAsync(int bussinessClientId)
    {
        return _dbContext
            .Venues.Where(venue => venue.BusinessClientId == bussinessClientId)
            .ToListAsync();
    }

    public async Task<List<Venue>> GetFilteredVenuesAsync(string filter)
    {
        return await _dbContext.Venues.Where(venue => venue.Name.Contains(filter)).ToListAsync();
    }

    public async Task<List<Venue>> GetVenuesAsync()
    {
        return await _dbContext.Venues.ToListAsync();
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
	public async Task<bool> DeleteVenueAsync(Venue venueToDelete)
	{
		_dbContext.Venues.Remove(venueToDelete);
		await SaveChangesAsync();
		return true;
	}
}
