using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.User;

public interface IUserRepository
{
    public Task<Organizer?> GetUserByRefreshTokenAsync(string refreshToken);
}