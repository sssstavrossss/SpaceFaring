using Space101.DAL;
using Space101.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Space101.Repositories
{
    public class TicketRepository
    {
        private readonly ApplicationDbContext context;

        public TicketRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Ticket> GetAllTickets()
        {
            return context.Tickets
                .Include(t => t.Flight.FlightPath.Departure)
                .Include(t => t.Flight.FlightPath.Destination)
                .Include(t => t.PassengerRace)
                .Include(t => t.PassengerPlanet)
                .Include(t => t.Seat.TravelClass)
                .ToList();
        }

        public List<Ticket> GetTicketsOfFlight(long id)
        {
            return context.Tickets
                .Include(t => t.Flight.FlightPath.Departure)
                .Include(t => t.Flight.FlightPath.Destination)
                .Include(t => t.PassengerRace)
                .Include(t => t.PassengerPlanet)
                .Include(t => t.Seat.TravelClass)
                .Where(t => t.FlightId == id)
                .ToList();
        }

        public List<Ticket> GetAllTicketsOfMultipleFlights(params int[] flightIds)
        {
            return context.Tickets
                .Include(t => t.Flight.FlightPath.Departure)
                .Include(t => t.Flight.FlightPath.Destination)
                .Include(t => t.PassengerRace)
                .Include(t => t.PassengerPlanet)
                .Include(t => t.Seat.TravelClass)
                .Where(t => flightIds.Contains(t.FlightId))
                .ToList();
        }

        public List<IGrouping<int, Ticket>> GetAllTicketsPerFlight()
        {
            return context.Tickets
                .Include(t => t.Flight.FlightPath.Departure)
                .Include(t => t.Flight.FlightPath.Destination)
                .Include(t => t.PassengerRace)
                .Include(t => t.PassengerPlanet)
                .Include(t => t.Seat.TravelClass)
                .GroupBy(t => t.FlightId, t => t)
                .ToList();
        }

        public Ticket GetTicketById(long id)
        {
            return context.Tickets
               .Include(t => t.Flight.FlightPath.Departure)
               .Include(t => t.Flight.FlightPath.Destination)
               .Include(t => t.PassengerRace)
               .Include(t => t.PassengerPlanet)
               .Include(t => t.Seat.TravelClass)
               .SingleOrDefault(t => t.TicketId == id);
        }

        public List<Order> GetTicketsOrder(params long[] ticketIds)
        {
            return context.TicketOrders
                .Include(to => to.Order.Tickets)
                .Where(to => ticketIds.Contains(to.TicketId))
                .Select(to => to.Order).Include(o=>o.Tickets)
                .ToList();
        }

        public List<Ticket> GetTicketsByRace(params int[] raceIds)
        {
            return context.Tickets
                .Include(t => t.Flight.FlightPath.Departure)
                .Include(t => t.Flight.FlightPath.Destination)
                .Include(t => t.PassengerRace)
                .Include(t => t.PassengerPlanet)
                .Include(t => t.Seat.TravelClass)
                .Where(t => raceIds.Contains(t.PassengerRaceId))
                .ToList();
        }

        public List<Ticket> GetTicketsByPlanet(params int[] planetIds)
        {
            return context.Tickets
                .Include(t => t.Flight.FlightPath.Departure)
                .Include(t => t.Flight.FlightPath.Destination)
                .Include(t => t.PassengerRace)
                .Include(t => t.PassengerPlanet)
                .Include(t => t.Seat.TravelClass)
                .Where(t => planetIds.Contains(t.PassengerPlanetId))
                .ToList();
        }

        public long GetTicketsCount()
        {
            return context.Tickets.Count();
        }

        public void AddMultiple(List<Ticket> tickets)
        {
            context.Tickets.AddRange(tickets);
        }

        public void RemoveTicket(Ticket ticket)
        {
            context.Tickets.Remove(ticket);
        }

        public void RemoveMultipleTicket(IEnumerable<Ticket> tickets)
        {
            foreach (var ticket in tickets)
            {
                context.Tickets.Remove(ticket);
            }
        }
    }
}