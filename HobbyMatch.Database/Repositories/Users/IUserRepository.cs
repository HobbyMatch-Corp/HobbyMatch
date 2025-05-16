using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Users;

public interface IUserRepository
{
    public Task<Organizer?> GetUserByRefreshTokenAsync(string refreshToken);
}