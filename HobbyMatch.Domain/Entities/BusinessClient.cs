using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Domain.Entities
{
    public class BusinessClient : Organizer
    {
        public string TaxID { get; set; }
    }
}
