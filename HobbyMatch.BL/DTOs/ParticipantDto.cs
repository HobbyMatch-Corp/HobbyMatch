using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.DTOs
{
	public record ParticipantDto(string id, string name);


	public static class UserExtensions
	{
		public static ParticipantDto ToDto(this User user) => new(
			user.Email ?? "",
			user.UserName ?? ""
		);
	}
}
