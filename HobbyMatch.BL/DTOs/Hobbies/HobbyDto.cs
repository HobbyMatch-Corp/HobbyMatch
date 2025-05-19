using HobbyMatch.Domain.Entities;

namespace HobbyMatch.BL.DTOs.Hobbies
{
    public record HobbyDto(string Name);

    public static partial class HobbyExtensions
    {
        public static HobbyDto ToDto(this Hobby hobby) => new HobbyDto(hobby.Name);
    }

    public static partial class HobbyDtoExtensions
    {
        public static Hobby ToEntity(this HobbyDto hobbyDto) => new Hobby() { Name = hobbyDto.Name };
    }
}
