using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HobbyMatch.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Event name must be 5-50 characters"), MinLength(5)]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Event description must be max 100 characters")]
        public string Description { get; set; }
        public int OrganizerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        public LocationNullable Location { get; set; }
        public int? VenueId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public float Price { get; set; }
        public int MaxUsers { get; set; }
        public int MinUsers { get; set; }

        public Organizer Organizer { get; set; }

        public Venue? Venue { get; set; }

        [JsonIgnore]
        public ICollection<User>? SignUpList { get; set; }

        [JsonIgnore]
        public ICollection<BusinessClient> SponsorsPartners { get; set; }

        [JsonIgnore]
        public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
    }
}
