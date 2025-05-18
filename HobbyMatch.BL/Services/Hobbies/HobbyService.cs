using HobbyMatch.Database.Repositories.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Hobbies
{
    public class HobbyService(HobbyRepository hobbyRepository) : IHobbyService
    {
        private readonly HobbyRepository _hobbyRepository = hobbyRepository;

        public Task<ICollection<Hobby>> GetHobbiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Hobby?> GetHobbyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Hobby?> GetHobbyAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
