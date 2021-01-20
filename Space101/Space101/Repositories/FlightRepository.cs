using Space101.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Space101.Models;
using Space101.Enums;

namespace Space101.Repositories
{
    public class FlightRepository
    {
        private ApplicationDbContext context;

        public FlightRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Flight> GetFlights()
        {
            return context.Flights
                .Include(f => f.FlightPath.Destination)
                .Include(f => f.FlightPath.Departure)
                .Include(f => f.FlightStatus)
                .Include(f => f.Starship)
                .ToList();
        }

        public List<Flight> GetFullFlightsWithSeats()
        {
            //The next code goes twice in database but returns fasters the request
            //To remove it add it in comments
            var flights = context.Flights
               .Include(f => f.FlightPath.Destination)
               .Include(f => f.FlightPath.Departure)
               .Include(f => f.FlightStatus)
               .Include(f => f.Starship)
               .ToList();

            var flightSeats = context.FlightSeats.Include(fs => fs.TravelClass).ToList();

            foreach (var flight in flights)
            {
                foreach (var seat in flightSeats)
                {
                    if (seat.FlightId == flight.FlightId)
                    {
                        flight.Seats.Add(seat);
                    }
                }
            }
            return flights;

            //The next code goes once in database but stalls the request
            //To use it remove comments
            //return context.Flights
            //   .Include(f => f.FlightPath.Destination)
            //   .Include(f => f.FlightPath.Departure)
            //   .Include(f => f.FlightStatus)
            //   .Include(f => f.Starship)
            //   .Include(f => f.Seats.Select(s => s.TravelClass))
            //   .ToList();
        }

        public Flight GetSingleFlight(int id)
        {
            return context.Flights
                .Include(f => f.FlightPath.Destination)
                .Include(f => f.FlightPath.Departure)
                .Include(f => f.FlightStatus)
                .Include(f => f.Starship)
                .SingleOrDefault(f => f.FlightId == id);
        }

        public Flight GetFullFlightWithSeats(int id)
        {
            return context.Flights
                .Include(f => f.FlightPath.Destination)
                .Include(f => f.FlightPath.Departure)
                .Include(f => f.FlightStatus)
                .Include(f => f.Starship)
                .Include(f => f.Seats.Select(s => s.TravelClass))
                .SingleOrDefault(f => f.FlightId == id);
        }

        public List<Flight> GetOnScheduleFlights()
        {
            return context.Flights
                .Include(f => f.FlightPath.Destination)
                .Include(f => f.FlightPath.Departure)
                .Include(f => f.FlightStatus)
                .Include(f => f.Starship)
                .Include(f => f.UserFavorites)
                .Where(f => f.FlightStatus.FlightStatusId == (int)FlightStatusEnum.OnScedule || f.FlightStatus.FlightStatusId == (int)FlightStatusEnum.Delayed)
                .ToList();
        }

        public List<Flight> GetFlightsByStarship(int id)
        {
            return context.Flights
                .Include(f => f.FlightPath.Destination)
                .Include(f => f.FlightPath.Departure)
                .Include(f => f.FlightStatus)
                .Include(f => f.Starship)
                .Include(f => f.Seats.Select(s => s.TravelClass))
               .Where(f => f.StarshipId == id)
               .ToList();
        }

        public List<Flight> GetFlightsByFlightPath(params int[] flightPathIds)
        {
            return context.Flights
                .Include(f => f.FlightPath.Destination)
                .Include(f => f.FlightPath.Departure)
                .Include(f => f.FlightStatus)
                .Include(f => f.Starship)
                .Include(f => f.Seats.Select(s => s.TravelClass))
               .Where(f => flightPathIds.Contains(f.FlightPathId))
               .ToList();
        }

        public List<FlightStatus> GetFlightStatusesWithFlights()
        {
            return context.FlightStatuses
               .Include(fs => fs.Flights)
               .ToList();
        }

        public long GetFlightsCount()
        {
            return context.Flights.Count();
        }

        public void Add(Flight flight)
        {
            context.Flights.Add(flight);
        }

        public void Remove(Flight flight)
        {
            context.Flights.Remove(flight);
        }

        public void RemoveMultipleFlights(IEnumerable<Flight> flights)
        {
            foreach (var flight in flights)
            {
                context.Flights.Remove(flight);
            }
        }

    }
}