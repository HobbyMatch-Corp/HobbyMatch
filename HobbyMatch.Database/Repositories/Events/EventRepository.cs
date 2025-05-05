using HobbyMatch.Database.Data;
using HobbyMatch.Domain.Entities;
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

        public async Task<bool> AddUserToEventAsync(int eventId, User user)
        {
            var ev = await GetEventByIdAsync(eventId);
            if (ev == null || ev.SignUpList == null) return false;

            if (ev.SignUpList.Any(u => u.Id == user.Id)) return false;

            if (ev.SignUpList.Count >= ev.MaxUsers) return false;

            ev.SignUpList.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveUserFromEventAsync(int eventId, User user)
        {
            var ev = await GetEventByIdAsync(eventId);
            if (ev == null || ev.SignUpList == null) return false;

            var existing = ev.SignUpList.FirstOrDefault(u => u.Id == user.Id);
            if (existing == null) return false;

            ev.SignUpList.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Event>> GetEventsWithFilterAsync(string? filter)
        {
            return await _context.Events
                .Where(e => string.IsNullOrEmpty(filter) || e.Name.Contains(filter))
                .Include(e => e.Venue)
                .Include(e => e.SignUpList)
                .ToListAsync();
        }

        public async Task<List<Event>?> GetSignedUpEventsAsync(string userEmail)
        {
            return await _context.Events
                .Where(e => e.SignUpList.Where(u => u.Email == userEmail).Any())
                .Include(e => e.Organizer)
                .Include(e => e.Venue)
                .ToListAsync();
        }

        public async Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail)
        {
            return await _context.Events
                .Where(e => e.Organizer.Email == organizerEmail)
                .Include(e => e.Organizer)
                .Include(e => e.Venue)
                .ToListAsync();
        }

        public async Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail)
        {
            return await _context.Events
                .Where(e => e.SponsorsPartners.Where(b => b.Email == businessClientEmail).Any())
                .Include(e => e.Organizer)
                .Include(e => e.Venue)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEventAsync(Event eventToEdit)
        {
            _context.Events.Update(eventToEdit);
            await _context.SaveChangesAsync();
        }
    }
}
