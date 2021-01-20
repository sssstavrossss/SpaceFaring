using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.TicketViewModels
{
    public class PrintTicketViewModel
    {
        public long TicketId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TravelClassId { get; set; }
        public long FlightSeatId { get; set; }
        public decimal Price { get; set; }
        public string DeparturePlanetName { get; set; }
        public string DestinationPlanetName { get; set; }
        public DateTime FlightDate { get; set; }
        public string StarshipModel { get; set; }

        public PrintTicketViewModel() { }

        public PrintTicketViewModel(Ticket ticket)
        {
            TicketId = ticket.TicketId;
            FirstName = ticket.FirstName;
            LastName = ticket.LastName;
            Price = ticket.Price;
            FlightSeatId = ticket.SeatId;
            TravelClassId = ticket.Seat.TravelClassId;
            DeparturePlanetName = ticket.Flight.FlightPath.Departure.Name;
            DestinationPlanetName = ticket.Flight.FlightPath.Destination.Name;
            StarshipModel = ticket.Flight.Starship.Model;
            FlightDate = ticket.Flight.Date;
        }

        public static PrintTicketViewModel CreateTicket(Ticket ticket)
        {
            return new PrintTicketViewModel(ticket);
        }
    }
}