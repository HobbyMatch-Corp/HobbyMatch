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

        public async Task<List<Event>> GetEventsWithFilterAsync(string? filter)
        {
            return await _context.Events
                .Where(e => string.IsNullOrEmpty(filter) || e.Name.Contains(filter))
                .Include(e => e.Organizer)
                .ToListAsync();
        }

        public async Task<List<Event>?> GetSignedUpEventsAsync(string userEmail)
        {
            var dbUser = await _context.AppUsers
                .Where(u => u.Email == userEmail)
                .Include(u => u.SignedUpEvents)
                .FirstOrDefaultAsync();

            if (dbUser == null)
                return null;

            return dbUser.SignedUpEvents.ToList();
        }

        public async Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail)
        {
            var dbOrganizer = await _context.Users
                .Where(org => org.Email == organizerEmail)
                .Include (org => org.OrganizedEvents)
                .FirstOrDefaultAsync();

            if (dbOrganizer == null)
                return null;

            return dbOrganizer.OrganizedEvents.ToList();
        }

        public async Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail)
        {
            var dbBusinessClient = await _context.BusinessClients
                .Where(bc => bc.Email == businessClientEmail)
                .Include(bc => bc.SponsoredEvents)
                .FirstOrDefaultAsync();

            if (dbBusinessClient == null)
                return null;

            return dbBusinessClient.SponsoredEvents.ToList();
        }
    }

		public async Task SaveChangesAsync()
		{
            await _context.SaveChangesAsync();
		}
	}
}
