using HobbyMatch.App.Services.Venues;
using HobbyMatch.BL.DTOs.Venues;

namespace HobbyMatch.App.Pagination.SpecializedHandlers.Venues;

public class FilteredVenuesQueryHandler : PaginatedParameterizedQueryHandler<VenueDto, string>
{
    public FilteredVenuesQueryHandler(
        Action onChanged, int pageSize, IVenueApiService venueApiService, string? initialFilter = null) : base(
        venueApiService.GetFilteredVenues, onChanged, pageSize,
        initialFilter)
    {
    }
}