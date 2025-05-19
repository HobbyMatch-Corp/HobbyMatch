using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record UpdateUserDto(string UserName, string Email, HobbyDto[] Hobbies);


	public static class UserDtoExtensions
	{
		public static UpdateUserDto ToDto(this User user) => new(
			user.UserName ?? "",
			user.Email ?? "",
			user.Hobbies.Select(h => h.ToDto()).ToArray()
		);
	}
}
