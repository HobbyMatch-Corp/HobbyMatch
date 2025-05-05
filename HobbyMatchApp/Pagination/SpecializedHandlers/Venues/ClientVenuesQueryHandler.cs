using HobbyMatch.App.Services.Venues;
using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.Database.Common.Pagination;

namespace HobbyMatch.App.Pagination.SpecializedHandlers.Venues;

public class ClientVenuesQueryHandler : PaginatedQueryHandler<VenueDto>
{
    private readonly IVenueApiService _venueApiService;

    public ClientVenuesQueryHandler(Action onChanged, int pageSize, IVenueApiService venueApiService) : base(onChanged,
        pageSize)
    {
        _venueApiService = venueApiService;
    }

    protected override async Task<PaginatedData<VenueDto>> FetchPageAsync(PaginationParameters parameters)
    {
        return await _venueApiService.GetClientVenuesAsync(parameters);
    }
}