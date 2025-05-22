using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.DTOs.Venues
{
	// TODO: When internet available, check documentation for requirements
	public record UpdateVenueDto(
		string Name,
		string Description
	);

	public static partial class VenueExtensions
	{
		public static UpdateVenueDto ToUpdateDto(this Venue venue)
		{
			return new UpdateVenueDto(venue.Name,
				venue.Description);
		}
	}
}
