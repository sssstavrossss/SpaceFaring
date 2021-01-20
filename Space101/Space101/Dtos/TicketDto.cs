using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class TicketDto
    {
        public long TicketId { get; set; }
        public string Path { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public long SeatId { get; set; }
        public long FlightId { get; set; }
        public string TravelClass { get; set; }
        public string PassengerName { get; set; }
        public string PassengerLastName { get; set; }
        public string Race { get; set; }
        public string Planet { get; set; }
        public string Price { get; set; }

        public TicketDto(Ticket ticket)
        {
            TicketId = ticket.TicketId;
            Path = $"{ticket.Flight.FlightPath.Departure.Name} ➔ {ticket.Flight.FlightPath.Destination.Name}";
            Date = ticket.Flight.Date.ToString("d");
            Time = ticket.Flight.Date.TimeOfDay.ToString("T");
            SeatId = ticket.SeatId;
            FlightId = ticket.FlightId;
            TravelClass = ticket.Seat.TravelClassId;
            PassengerName = ticket.FirstName;
            PassengerLastName = ticket.LastName;
            Race = ticket.PassengerRace.Name;
            Planet = ticket.PassengerPlanet.Name;
            Price = ticket.Price.ToString("N2");
        }

    }
}