using HobbyMatch.Domain.Entities;

namespace HobbyMatch.Database.Repositories.Hobbies
{
    internal interface IHobbyRepository
    {
        public Task<ICollection<Hobby>> GetHobbiesAsync();
        public Task<Hobby?> GetHobbyAsync(int id);
    }
}
