using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Repositories.Users;

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
	public async Task<Organizer?> GetUserByIdAsync(int id)
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
		return user;
	}
	public async Task<bool> AddFriendToUserAsync(User user, User friend)
	{
		if (user.Friends.Any(u => u.Id == friend.Id)) return false;
		user.Friends.Add(friend);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> RemoveFriendFromUserAsync(User user, User friend)
	{
		var existing = user.Friends.FirstOrDefault(u => u.Id == friend.Id);
		if (existing == null) return false;
		user.Friends.Remove(existing);
		await _context.SaveChangesAsync();
		return true;
	}
}