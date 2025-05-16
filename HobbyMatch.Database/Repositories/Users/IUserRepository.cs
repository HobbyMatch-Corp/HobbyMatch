using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Users;

public interface IUserRepository
{
    public Task<Organizer?> GetUserByRefreshTokenAsync(string refreshToken);
    public Task<Organizer?> GetUserByIdAsync(int id);
	Task<bool> AddFriendToUserAsync(User user, User friend);
	Task<bool> RemoveFriendFromUserAsync(User user, User friend);
}