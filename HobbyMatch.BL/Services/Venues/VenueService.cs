using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.Database.Common.Pagination;
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
		{
			return false;
		}

		venue.Name = updateVenueDto.Name;
		venue.Description = updateVenueDto.Description;


		await _venueRepository.SaveChangesAsync();

		return true;
	}
}
