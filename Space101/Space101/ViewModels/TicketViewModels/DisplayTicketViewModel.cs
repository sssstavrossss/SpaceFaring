using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.TicketViewModels
{
    public class DisplayTicketViewModel
    {
        [Display(Name = "Ticket Id")]
        public long TicketId { get; set; }

        [Display(Name = "Flight Path")]
        public string Path { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        [Display(Name = "Flight Id")]
        public long FlightId { get; set; }

        [Display(Name = "Seat Id")]
        public long SeatId { get; set; }

        [Display(Name = "Class")]
        public string TravelClass { get; set; }

        [Display(Name = "Firstname")]
        public string PassengerName { get; set; }

        [Display(Name = "Lastname")]
        public string PassengerLastName { get; set; }
        public string Race { get; set; }

        [Display(Name = "Homeplanet")]
        public string Planet { get; set; }
        public string Price { get; set; }

        public DisplayTicketViewModel(Ticket ticket)
        {
            TicketId = ticket.TicketId;
            Path = $"{ticket.Flight.FlightPath.Departure.Name} ➔ {ticket.Flight.FlightPath.Destination.Name}";
            Date = ticket.Flight.Date.ToString("d");
            Time = ticket.Flight.Date.TimeOfDay.ToString("T");
            FlightId = ticket.FlightId;
            SeatId = ticket.SeatId;
            TravelClass = ticket.Seat.TravelClassId;
            PassengerName = ticket.FirstName;
            PassengerLastName = ticket.LastName;
            Race = ticket.PassengerRace.Name;
            Planet = ticket.PassengerPlanet.Name;
            Price = ticket.Price.ToString("N2");
        }
    }
}