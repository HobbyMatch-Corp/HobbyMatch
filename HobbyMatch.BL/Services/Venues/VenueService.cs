using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.BL.ResultEnums;
using HobbyMatch.Database.Repositories.Venues;
using HobbyMatch.Domain.Entities;

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

    public async Task<List<Venue>> GetClientVenuesAsync(int businessClientId)
    {
        return await _venueRepository.GetBusinessClientVenuesAsync(businessClientId);
    }

    public async Task<List<Venue>> GetFilteredVenuesAsync(string filter)
    {
        return await _venueRepository.GetFilteredVenuesAsync(filter);
    }

    public async Task<List<Venue>> GetVenuesAsync()
    {
        return await _venueRepository.GetVenuesAsync();
    }

    public async Task<Venue> CreateVenue(CreateVenueDto createRequest, int businessClientId)
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

    public async Task<bool> EditVenueAsync(int venueId, UpdateVenueDto updateVenueDto)
    {
        var venue = await _venueRepository.GetVenueByIdAsync(venueId);
        if (venue == null)
            return false;

        venue.Name = updateVenueDto.Name;
        venue.Description = updateVenueDto.Description;

        await _venueRepository.SaveChangesAsync();

        return true;
    }

	public async Task<DeleteResult> DeleteVenueAsync(int eventId)
	{
		var eventToDelete = await _venueRepository.GetVenueByIdAsync(eventId);
		if (eventToDelete == null)
			return DeleteResult.NotFound;

		var deleted = await _venueRepository.DeleteVenueAsync(eventToDelete);
		return deleted ? DeleteResult.Success : DeleteResult.Failed;
	}
}
