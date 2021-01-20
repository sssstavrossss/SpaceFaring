using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class FlightPath
    {
        public int FlightPathId { get; private set; }

        [ForeignKey("Departure")]
        [Index("UniqueRoutes", IsUnique = true, Order = 1)]
        [Display(Name = "Departure Planet")]
        public int DepartureId { get; private set; }

        [ForeignKey("Destination")]
        [Index("UniqueRoutes", IsUnique = true, Order = 2)]
        [Display(Name = "Destination Planet")]
        public int DestinationId { get; private set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Distance { get; private set; }

        public Planet Destination { get; set; }
        public Planet Departure { get; set; }

        protected FlightPath()
        { }

        public FlightPath(Planet departure, Planet destination)
        {
            DepartureId = departure.PlanetID;
            DestinationId = destination.PlanetID;
            Distance = FindDistance(departure,destination);
        }

        public static decimal FindDistance(Planet departure, Planet destination)
        {
            decimal distance = (Decimal)Math.Sqrt(Math.Pow(Math.Abs((double)departure.CoordX - (double)destination.CoordX), 2) + Math.Pow(Math.Abs((double)departure.CoordY - (double)destination.CoordY), 2));
            return distance;
        }

    }
}