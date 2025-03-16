using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Model.Entities
{
    [ComplexType]
    public class Location
    {

        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }
}
