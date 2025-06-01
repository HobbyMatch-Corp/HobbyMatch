using System.Text.Json.Serialization;

namespace HobbyMatch.Domain.Entities
{
    public class BusinessClient : Organizer
    {
        public string TaxID { get; set; }

        [JsonIgnore]
        public ICollection<Event> SponsoredEvents { get; set; } = new List<Event>();

        [JsonIgnore]
        public ICollection<Venue> Venues { get; set; } = new List<Venue>();
    }
}
