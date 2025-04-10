using HobbyMatch.Model.Entities;
using System.Text.Json.Serialization;

namespace HobbyMatch.Domain.Entities
{
    public class BusinessClient : Organizer
    {
        public string TaxID { get; set; }

        [JsonIgnore]
        public ICollection<Event>? SponsoredEvents { get; set; }
    }
}
