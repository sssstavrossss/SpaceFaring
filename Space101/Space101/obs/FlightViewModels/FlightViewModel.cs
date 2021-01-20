using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.FlightViewModels
{
    public class FlightViewModel
    {
        public int FlightId { get; set; }

        private DateTime _fullDate;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date 
        {
            get
            {
                return _fullDate.Date;
            }
        }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string Time 
        {
            get
            {
                return _fullDate.TimeOfDay.ToString();
            }
        }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal BasePrice { get; set; }

        public string DepartureName { get; set; }

        public string DestinationName { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Distance { get; set; }

        public int FlightPathId { get; set; }

        public FlightViewModel()
        { }

        private FlightViewModel(int flightId, DateTime date, decimal basePrice, string depatureName, string destinationName, decimal distance, int flightPathId)
        {
            FlightId = flightId;
            _fullDate = date;
            BasePrice = basePrice;
            DepartureName = depatureName;
            DestinationName = destinationName;
            Distance = distance;
            FlightPathId = flightPathId;
        }

        private FlightViewModel(Flight flight)
        {
            FlightId = flight.FlightId;
            _fullDate = flight.Date;
            BasePrice = flight.BasePrice;
            DepartureName = flight.FlightPath.Departure.Name;
            DestinationName = flight.FlightPath.Destination.Name;
            Distance = flight.FlightPath.Distance;
            FlightPathId = flight.FlightPathId;
        }

        public static FlightViewModel CreateFromModel(Flight flight)
        {
            return new FlightViewModel(flight);
        }

    }
}