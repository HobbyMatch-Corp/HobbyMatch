using HobbyMatch.Domain.Entities;

namespace HobbyMatch.App.Services.Api
{
	public interface IOrganizerApiService
	{
		Task<T?> GetMe<T>() where T : Organizer;
		Task<T[]?> GetUsersAsync<T>() where T : Organizer;
		Task<T?> GetUserAsync<T>(int id) where T : Organizer;
		Task<bool> UpdateUserAsync<T, TDto>(int id, TDto editedUser) where T : Organizer;
	}
}
