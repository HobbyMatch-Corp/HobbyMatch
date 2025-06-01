using HobbyMatch.BL.DTOs.Venues;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record BusinessClientDto(string Id, string Name, string Email, string taxId, List<VenueDto> Venues) : OrganizerDto(Id, Name);
	public static partial class BusinessClientDtoExtensions
	{
		public static BusinessClientDto ToDto(this BusinessClient user) => new(
			user.Id.ToString(),
			user.UserName ?? "",
			user.Email ?? "",
			user.TaxID ?? "",
            user.Venues.Select(v => v.ToDto()).ToList()
        );
	}
}
