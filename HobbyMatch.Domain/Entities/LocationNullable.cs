﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Domain.Entities
{
	[ComplexType]
	public class LocationNullable
	{
		public double? Longitude { get; set; }
		public double? Latitude { get; set; }

		public override string ToString()
		{
			return $"Longitude {Longitude}, Latitude {Latitude}";
		}
		public LocationNullable() { }
		public LocationNullable(Location location)
		{
			Longitude = location.Longitude;
			Latitude = location.Latitude;
		}
	}
}
