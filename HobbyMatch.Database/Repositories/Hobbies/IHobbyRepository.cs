using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Hobbies
{
    public interface IHobbyRepository
    {
        public Task<ICollection<Hobby>> GetHobbiesAsync();
        public Task<Hobby?> GetHobbyAsync(int id);
        public Task<Hobby?> GetHobbyAsync(string name);
    }
}
