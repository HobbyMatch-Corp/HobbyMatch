using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record BusinessClientDto(string userId, string userName, string email, string taxId) : OrganizerDto(userId, userName, email);
	public static partial class BusinessClientDtoExtensions
	{
		public static BusinessClientDto ToDto(this BusinessClient user) => new(
			user.Id.ToString(),
			user.UserName ?? "",
			user.Email ?? "",
			user.TaxID ?? ""
		);
	}
}
