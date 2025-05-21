using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record UpdateUserDto(string userId, string userName, string email, HobbyDto[] hobbies);


	public static partial class UserDtoExtensions
	{
		public static UpdateUserDto ToUpdateDto(this User user) => new(
			user.Id.ToString(),
			user.UserName ?? "",
			user.Email ?? "",
			user.Hobbies.Select(h => h.ToDto()).ToArray()
		);
	}
}
