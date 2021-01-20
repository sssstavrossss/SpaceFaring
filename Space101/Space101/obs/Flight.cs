using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public class Flight
    {
        public int FlightId { get; private set; }

        public DateTime Date { get; private set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal BasePrice { get; private set; }

        public int FlightPathId { get; private set; }
        public FlightPath FlightPath { get; private set; }

        protected Flight()
        { }

        public Flight(DateTime date, decimal basePrice, int flightPathId)
        {
            Date = date;
            BasePrice = basePrice;
            FlightPathId = flightPathId;
        }

        public void Update(DateTime? date, decimal? basePrice, int? flightPathId)
        {
            Date = date ?? Date;
            BasePrice = basePrice ?? BasePrice;
            FlightPathId = flightPathId ?? FlightPathId;
        }

    }
}