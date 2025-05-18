using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Hobbies
{
    public interface IHobbyService
    {
        public Task<ICollection<Hobby>> GetHobbiesAsync();
        public Task<Hobby?> GetHobbyAsync(int id);
    }
}
