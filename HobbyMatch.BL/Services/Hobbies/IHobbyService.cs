using HobbyMatch.BL.DTOs.Hobbies;
using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.Services.Hobbies
{
    public interface IHobbyService
    {
        public Task<ICollection<Hobby>> GetHobbiesAsync();
        public Task<Hobby?> GetHobbyAsync(int id);
        public Task<Hobby?> GetHobbyAsync(string name);
        public Task<ICollection<Hobby>> GetHobbiesAsync(List<HobbyDto> hobbyDtos);
    }
}
