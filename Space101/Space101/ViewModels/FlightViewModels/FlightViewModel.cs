using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.FlightViewModels
{
    public class FlightViewModel
    {
        [Display(Name = "Flight Id")]
        public int FlightId { get; set; }

        private DateTime _fullDate;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Date 
        {
            get
            {
                return _fullDate.Date;
            }
        }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:T}", ApplyFormatInEditMode = true)]
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

        [Display(Name = "VIP")]
        public bool IsVip { get; set; }

        public string Status { get; set; }
        public int FlightPathId { get; set; }

        public ICollection<FlightStatus> FlightStatuses { get; set; }

        public FlightViewModel()
        {
            FlightStatuses = new List<FlightStatus>();
        }

        private FlightViewModel(int flightId, DateTime date, decimal basePrice, string depatureName, string destinationName, decimal distance, int flightPathId, ICollection<FlightStatus> statuses)
        {
            FlightId = flightId;
            _fullDate = date;
            BasePrice = basePrice;
            DepartureName = depatureName;
            DestinationName = destinationName;
            Distance = distance;
            FlightPathId = flightPathId;
        }

        private FlightViewModel(Flight flight, ICollection<FlightStatus> statuses)
        {
            FlightId = flight.FlightId;
            _fullDate = flight.Date;
            BasePrice = flight.BasePrice;
            DepartureName = flight.FlightPath.Departure.Name;
            DestinationName = flight.FlightPath.Destination.Name;
            Distance = flight.FlightPath.Distance;
            FlightPathId = flight.FlightPathId;
            IsVip = flight.IsVIP;
            Status = flight.FlightStatus.StatusName;
            FlightStatuses = statuses;
        }

        public static FlightViewModel CreateFromModel(Flight flight, ICollection<FlightStatus> statuses)
        {
            return new FlightViewModel(flight, statuses);
        }

    }
}