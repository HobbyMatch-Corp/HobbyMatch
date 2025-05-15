using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record UpdateUserDto(string UserName, string Email);


	public static class UserDtoExtensions
	{
		public static UpdateUserDto ToUpdateDto(this User user) => new(
			user.UserName ?? "",
			user.Email ?? ""
		);
	}
}
