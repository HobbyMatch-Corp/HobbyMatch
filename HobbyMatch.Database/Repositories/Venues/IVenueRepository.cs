using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Venues;

public interface IVenueRepository
{
    public Task<Venue?> GetVenueByIdAsync(int venueId);

    public Task<List<Venue>> GetBusinessClientVenuesAsync(int bussinessClientId);

    public Task<List<Venue>> GetFilteredVenuesAsync(string filter);

    public Task<List<Venue>> GetVenuesAsync();

    public Task AddVenueAsync(Venue venue);
    public Task SaveChangesAsync();
    public Task<bool> DeleteVenueAsync(Venue venueToDelete);

}
