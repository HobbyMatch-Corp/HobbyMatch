using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.DTOs.Venues
{
	public record UpdateVenueDto(
		string? Name,
		string? Address,
		int? MaxUsers,
		decimal? Price,
		Location? Location,
		string? Description,
		int? BusinessClientId,
		string? ClientName
	);

	public static partial class VenueExtensions
	{
		public static UpdateVenueDto ToUpdateDto(this Venue venue)
		{
			return new UpdateVenueDto(venue.Name, venue.Address, venue.MaxUsers, venue.Price, venue.Location,
				venue.Description, venue.BusinessClientId, venue.BusinessClient.UserName!);
		}
	}
}
