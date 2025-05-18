using HobbyMatch.BL.DTOs.Hobbies;

namespace HobbyMatch.App.Services.Hobbies
{
    public interface IHobbyApiService
    {
        public Task<ICollection<HobbyDto>> GetHobbiesAsync();
    }
}
