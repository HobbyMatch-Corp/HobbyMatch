using HobbyMatch.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HobbyMatch.Model.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int OrganizerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        public Location Location { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public float Price { get; set; }
        public int MaxUsers { get; set; }
        public int MinUsers { get; set; }

        public Organizer Organizer { get; set; }

        [JsonIgnore]
        public ICollection<User>? SignUpList { get; set; }

        [JsonIgnore]
        public ICollection<BusinessClient>? SponsorsPartners { get; set; }
    }
}
