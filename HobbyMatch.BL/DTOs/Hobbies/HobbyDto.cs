using HobbyMatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.BL.DTOs.Hobbies
{
    public record HobbyDto(string name);

    public static partial class HobbyExtensions
    {
        public static HobbyDto ToDto(this Hobby hobby) => new HobbyDto(hobby.Name);
    }
}
