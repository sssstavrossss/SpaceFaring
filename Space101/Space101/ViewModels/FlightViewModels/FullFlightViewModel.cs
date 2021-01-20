using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Space101.ViewModels.FlightViewModels
{
    public class FullFlightViewModel
    {
        public int FlightId { get; private set; }
        public DateTime Date { get; private set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal BasePrice { get; private set; }

        public FlightPath FlightPath { get; private set; }
        public FlightStatus FlightStatus { get; private set; }
        public Starship Starship { get; private set; }
        public bool IsVIP { get; private set; }

        public List<FlightSeat> Seats { get; private set; }

        public FullFlightViewModel()
        {
            Seats = new List<FlightSeat>();
        }

        public FullFlightViewModel(Flight flight)
        {
            FlightId = flight.FlightId;
            Date = flight.Date;
            BasePrice = flight.BasePrice;
            FlightPath = flight.FlightPath;
            FlightStatus = flight.FlightStatus;
            Starship = flight.Starship;
            IsVIP = flight.IsVIP;
            Seats = flight.Seats.ToList();
        }
    }
}