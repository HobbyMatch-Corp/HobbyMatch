using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HobbyMatch.Domain.Entities
{
    public class Venue
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Venue name must be 5-50 characters"), MinLength(5)]
        public string Name { get; set; }

        public string Address { get; set; }

        public int MaxUsers { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public Location Location { get; set; }

        [MaxLength(100, ErrorMessage = "Venue description must be max 100 characters")]
        public string Description { get; set; }

        public int BusinessClientId { get; set; }

        public BusinessClient BusinessClient { get; set; }

        [JsonIgnore]
        public ICollection<Event> Events { get; set; } = new List<Event>();

    }
}
