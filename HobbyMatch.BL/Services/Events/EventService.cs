using HobbyMatch.BL.DTOs.Events;
using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.BL.DTOs.Organizers;
using HobbyMatch.BL.Services.Hobbies;
using HobbyMatch.Database.Repositories.Events;
using HobbyMatch.Domain.Entities;
using HobbyMatch.Domain.Requests;

namespace HobbyMatch.BL.Services.Events;

public class EventService(IEventRepository eventRepository, IHobbyService hobbyService) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;
    private readonly IHobbyService _hobbyService = hobbyService;

    public async Task<bool> AddUserToEventAsync(int eventId, User user)
    {
        var ev = await _eventRepository.GetEventByIdAsync(eventId);
        if (ev == null || ev.SignUpList == null) return false;

        if (ev.SignUpList.Any(u => u.Id == user.Id)) return false;

        if (ev.SignUpList.Count >= ev.MaxUsers) return false;

        ev.SignUpList.Add(user);
        await _eventRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CheckIfUserInSignInList(int eventId, User user)
    {
        var ev = await _eventRepository.GetEventByIdAsync(eventId);
        if (ev == null || ev.SignUpList == null) return false;

        if (!ev.SignUpList.Any(u => u.Id == user.Id)) return false;

        return true;
    }

    public async Task<Event?> CreateEventAsync(CreateEventDto dto, int organizerId)
    {
        var hobbies = await _hobbyService.GetHobbiesAsync(dto.Hobbies.ToList());

        var entity = new Event
        {
            Name = dto.Title,
            Description = dto.Description,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Location = dto.Location,
            Price = dto.Price,
            MaxUsers = dto.MaxUsers,
            MinUsers = dto.MinUsers,
            OrganizerId = organizerId,
            RelatedHobbies = hobbies,
        };

        var result = await _eventRepository.AddEvent(entity);
        return result;
    }

    public async Task<Event?> EditEventAsync(CreateEventDto dto, int eventId, int userId)
    {
        var hobbies = await _hobbyService.GetHobbiesAsync(dto.Hobbies.ToList());

        var eventToEdit = await _eventRepository.GetEventByIdAsync(eventId);

        if (eventToEdit == null || eventToEdit.OrganizerId != userId) return null;

        eventToEdit.Name = dto.Title;
        eventToEdit.Description = dto.Description;
        eventToEdit.StartTime = dto.StartTime;
        eventToEdit.EndTime = dto.EndTime;
        eventToEdit.Location = dto.Location;
        eventToEdit.Price = dto.Price;
        eventToEdit.RelatedHobbies = hobbies;

        await _eventRepository.UpdateEventAsync(eventToEdit); // Assuming this method exists

        return eventToEdit;
    }


    public async Task<bool> RemoveUserFromEventAsync(int eventId, User user)
    {
        var ev = await _eventRepository.GetEventByIdAsync(eventId);
        if (ev == null || ev.SignUpList == null) return false;

        var existing = ev.SignUpList.FirstOrDefault(u => u.Id == user.Id);
        if (existing == null) return false;

        ev.SignUpList.Remove(existing);
        await _eventRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Event>> GetEventsWithFilterAsync(string? filter)
    {
        return await _eventRepository.GetEventsWithFilterAsync(filter);
    }

    public async Task<List<Event>?> GetOrganizedEventsAsync(string organizerEmail)
    {
        return await _eventRepository.GetOrganizedEventsAsync(organizerEmail);
    }

    public async Task<List<Event>?> GetSignedUpEventsAsync(string userEmail)
    {
        return await _eventRepository.GetSignedUpEventsAsync(userEmail);
    }

    public async Task<List<Event>?> GetSponsoredEventsAsync(string businessClientEmail)
    {
        return await _eventRepository.GetSponsoredEventsAsync(businessClientEmail);
    }
}