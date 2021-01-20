using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class FlightPathDto
    {
        public int FlightPathId { get; set; }
        public decimal Distance { get; set; }
        public PlanetFlightDto Destination { get; set; }
        public PlanetFlightDto Departure { get; set; }
        public FlightPathDto() { }
    }
}