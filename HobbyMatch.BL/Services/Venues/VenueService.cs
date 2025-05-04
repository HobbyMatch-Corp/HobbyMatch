using HobbyMatch.Database.Common.Pagination;
using HobbyMatch.Database.Repositories.Venues;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.BL.Services.Venues;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;

    public VenueService(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task<Venue?> GetVenueByIdAsync(int venueId)
    {
        return await _venueRepository.GetVenueByIdAsync(venueId);
    }

    public async Task<PaginatedData<Venue>> GetClientVenuesAsync(
        int businessClientId,
        PaginationParameters paginationParams
    )
    {
        return await _venueRepository.GetBusinessClientVenuesAsync(
            businessClientId,
            paginationParams
        );
    }

    public async Task<PaginatedData<Venue>> GetFilteredVenuesAsync(
        string filter,
        PaginationParameters paginationParams
    )
    {
        return await _venueRepository.GetFilteredVenuesAsync(filter, paginationParams);
    }

    public async Task<Venue> CreateVenue(CreateVenueRequest createRequest, int businessClientId)
    {
        var venue = new Venue
        {
            Name = createRequest.Name,
            Description = createRequest.Description,
            Location = createRequest.Location,
            Address = createRequest.Address,
            BusinessClientId = businessClientId,
            MaxUsers = createRequest.MaxUsers,
            Price = createRequest.Price,
        };

        await _venueRepository.AddVenueAsync(venue);

        return venue;
    }
}
