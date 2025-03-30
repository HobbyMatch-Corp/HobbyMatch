using HobbyMatch.Domain.Entities;
using System.Diagnostics.Metrics;

namespace HobbyMatch.App.Services
{
	public interface IOrganizerApiService
	{
		 Task<T[]?> GetUsersAsync<T>() where T : Organizer;
		 Task<T?> GetUserAsync<T>(int id) where T : Organizer;
		 Task<T?> EditUserAsync<T>(int id, T editedUser) where T : Organizer; // TODO: Decide what to send as content. User object or just the fields that need to be updated. In that case user would be determined by JWT token.
	}
}
