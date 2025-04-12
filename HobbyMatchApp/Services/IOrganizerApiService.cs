using HobbyMatch.Domain.Entities;

namespace HobbyMatch.App.Services
{
    public interface IOrganizerApiService
    {
        Task<T[]?> GetUsersAsync<T>() where T : BusinessClient;
        Task<T?> GetUserAsync<T>(int id) where T : BusinessClient;
        Task<T?> EditUserAsync<T>(int id, T editedUser) where T : BusinessClient; // TODO: Decide what to send as content. User object or just the fields that need to be updated. In that case user would be determined by JWT token.
    }
}
