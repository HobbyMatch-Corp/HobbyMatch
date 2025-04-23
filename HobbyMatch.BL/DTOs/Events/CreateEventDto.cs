using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.DTOs.Events
{
	public record CreateEventDto(
		int Id,
		string Name,
		string Description,
		DateTime StartTime,
		DateTime EndTime,
		LocationNullable Location,
		float Price,
		int MaxUsers,
		int MinUsers
	);
}
