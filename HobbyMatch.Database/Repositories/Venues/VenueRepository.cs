using HobbyMatch.Database.Common.Pagination;
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
		var venue = await _dbContext.Venues.Include(v => v.BusinessClient).FirstOrDefaultAsync(v => v.Id == venueId);
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
}