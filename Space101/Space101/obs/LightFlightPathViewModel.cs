using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.FlightPathViewModels
{
    public class LightFlightPathViewModel
    {
        public int FlightPathId { get; set; }

        public string Name { get; set; }

        public LightFlightPathViewModel(FlightPath flightPath)
        {
            FlightPathId = flightPath.FlightPathId;
            Name = $"{flightPath.Departure.Name} --> {flightPath.Destination.Name}";
        }
    }
}