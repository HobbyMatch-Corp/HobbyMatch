﻿using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Organizers
{
	public record UpdateBusinessClientDto(string UserName, string Email, string TaxId);


	public static partial class BusinessClientDtoExtensions
	{
		public static UpdateBusinessClientDto ToUpdateDto(this BusinessClient user) => new(
			user.UserName ?? "",
			user.Email ?? "",
			user.TaxID ?? ""
		);
	}
}
