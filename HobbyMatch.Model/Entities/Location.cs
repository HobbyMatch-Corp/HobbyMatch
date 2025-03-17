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

        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
