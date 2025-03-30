using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Organizer?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        return user;
    }
}