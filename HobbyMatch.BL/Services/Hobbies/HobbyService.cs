using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.Database.Repositories.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Hobbies
{
    public class HobbyService(IHobbyRepository hobbyRepository) : IHobbyService
    {
        private readonly IHobbyRepository _hobbyRepository = hobbyRepository;

        public Task<ICollection<Hobby>> GetHobbiesAsync()
        {
            return _hobbyRepository.GetHobbiesAsync();
        }

        public async Task<ICollection<Hobby>> GetHobbiesAsync(List<HobbyDto> hobbyDtos)
        {
            var hobbies = new List<Hobby>();
            foreach (var dto in hobbyDtos)
            {
                var hobby = await GetHobbyAsync(dto.Name);
                if (hobby is not null)
                    hobbies.Add(hobby);
            }

            return hobbies;
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
