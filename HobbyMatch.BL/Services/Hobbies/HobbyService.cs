using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.Database.Repositories.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Hobbies
{
    public class HobbyService(IHobbyRepository hobbyRepository) : IHobbyService
    {
        private readonly IHobbyRepository _hobbyRepository = hobbyRepository;

        public async Task<ICollection<Hobby>> GetHobbiesAsync()
        {
            return await _hobbyRepository.GetHobbiesAsync();
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

        public async Task<Hobby?> GetHobbyAsync(int id)
        {
            return await _hobbyRepository.GetHobbyAsync(id);
        }

        public async Task<Hobby?> GetHobbyAsync(string name)
        {
            return await _hobbyRepository.GetHobbyAsync(name);
        }
    }
}
