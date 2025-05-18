using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Hobbies
{
    public class HobbyService : IHobbyService
    {

        public Task<ICollection<Hobby>> GetHobbiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Hobby?> GetHobbyAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
