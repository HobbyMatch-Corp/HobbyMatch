using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record UserDto(string Id, string Name, string Email, HobbyDto[] Hobbies) : OrganizerDto(Id, Name);
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
