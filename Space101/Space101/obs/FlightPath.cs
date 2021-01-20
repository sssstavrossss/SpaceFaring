using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        //{
        //    get
        //    {
        //        //(|xa^2-xb^2|^2-|ya^2-yb^2|^2)^(1/2)
        //        //var distance = (Decimal)Math.Sqrt(Math.Pow(Math.Abs(2d - 1d), 2) - Math.Pow(Math.Abs(3d - 4d), 2));

        //        return distance; 
        //    }
        //}

        public Planet Destination { get; set; }
        public Planet Departure { get; set; }

        protected FlightPath()
        { }

        public FlightPath(int departureId, int destinationId, decimal distance)
        {
            DepartureId = departureId;
            DestinationId = destinationId;
            Distance = distance;
        }

        public void Update(decimal distance)
        {
            Distance = distance;
        }

    }
}