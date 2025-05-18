using HobbyMatch.Database.Repositories.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Hobbies
{
    public class HobbyService(HobbyRepository hobbyRepository) : IHobbyService
    {
        private readonly HobbyRepository _hobbyRepository = hobbyRepository;

        public Task<ICollection<Hobby>> GetHobbiesAsync()
        {
            return _hobbyRepository.GetHobbiesAsync();
        }

        public Task<Hobby?> GetHobbyAsync(int id)
        {
            return _hobbyRepository.GetHobbyAsync(id);
        }

        public Task<Hobby?> GetHobbyAsync(string name)
        {
            return _hobbyRepository.GetHobbyAsync(name);
        }
    }
}
