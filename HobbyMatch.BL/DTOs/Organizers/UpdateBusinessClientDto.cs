using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record UpdateBusinessClientDto(string UserName, string Email, string TaxId);


	public static class BusinessClientDtoExtensions
	{
		public static UpdateBusinessClientDto ToDto(this BusinessClient user) => new(
			user.UserName ?? "",
			user.Email ?? "",
			user.TaxID ?? ""
		);
	}
}
