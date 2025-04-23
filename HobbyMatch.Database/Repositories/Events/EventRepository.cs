using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Repositories.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }
		public async Task<Event?> AddEvent(Event newEvent)
		{
			var createdEvent = await _context.Events.AddAsync(newEvent);
			await _context.SaveChangesAsync();
			return createdEvent.Entity;
		}

		public async Task<Event?> GetEventByIdAsync(int eventId)
        {
            return await _context.Events
                .Include(e => e.SignUpList)
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<List<Event>> GetEventsWithFilter(string? filter)
        {
            return await _context.Events
                .Where(e => string.IsNullOrEmpty(filter) || e.Name.Contains(filter))
                .Include(e => e.Organizer)
                .ToListAsync();
        }

		public async Task SaveChangesAsync()
		{
            await _context.SaveChangesAsync();
		}
	}
}
