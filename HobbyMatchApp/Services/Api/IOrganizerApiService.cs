using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.App.Services.Api
{
	public interface IOrganizerApiService
	{
		Task<T?> GetMe<T>() where T : OrganizerDto;
		Task<T[]?> GetUsersAsync<T>() where T : OrganizerDto;
		Task<T?> GetUserAsync<T>(int id) where T : OrganizerDto;
		Task<bool> UpdateUserAsync<T, TDto>(string id, TDto editedUser) where T : OrganizerDto;
	}
}
