using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Friends
{
	internal class FriendRepository: IFriendRepository
	{
		private readonly AppDbContext _context;
		public FriendRepository(AppDbContext context)
		{
			_context = context;
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
}
