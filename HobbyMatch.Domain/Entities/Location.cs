using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyMatch.Model.Entities
{
    [ComplexType]
    public class Location
    {

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
