using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyMatch.Domain.Entities
{
    [ComplexType]
    public class Location
    {

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
