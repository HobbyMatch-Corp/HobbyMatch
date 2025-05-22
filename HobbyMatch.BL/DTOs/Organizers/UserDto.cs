using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record UserDto(string userId, string userName, string email, HobbyDto[] hobbies) : OrganizerDto(userId, userName, email);
	public static partial class UserDtoExtensions
	{
		public static UserDto ToDto(this User user) => new(
			user.Id.ToString(),
			user.UserName ?? "",
			user.Email ?? "",
			user.Hobbies.Select(h => h.ToDto()).ToArray()
		);
	}
}
