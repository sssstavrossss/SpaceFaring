using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Space101.DAL;
using Space101.Models;

namespace Space101.Repositories
{
    public class FlightPathRepository
    {
        private ApplicationDbContext context;

        public FlightPathRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<FlightPath> GetFlightPaths()
        {
            return context.FlightPaths
                .Include(fp => fp.Departure)
                .Include(fp => fp.Destination)
                .ToList();
        }

        public List<FlightPath> GetFlightPathsByIdforDestination(int id)
        {
            return context.FlightPaths
                .Include(fp => fp.Destination)
                .Include(fp => fp.Departure)
                .Where(fp => fp.DestinationId == id)
                .ToList();
        }

        public List<FlightPath> GetFlightPathsByIdforDeparture(int id)
        {
            return context.FlightPaths
                .Include(fp => fp.Departure)
                .Include(fp => fp.Destination)
                .Where(fp => fp.DepartureId == id)
                .ToList();
        }

        public FlightPath GetSingleFlightPath(int id)
        {
            return context.FlightPaths
                .Include(fp => fp.Departure)
                .Include(fp => fp.Destination)
                .SingleOrDefault(fp => fp.FlightPathId == id);
        }

        public FlightPath GetFlightPathByUniqueIndex(int destinationId, int departureId)
        {
            return context.FlightPaths
                .SingleOrDefault(fp => fp.DestinationId == destinationId && fp.DepartureId == departureId);
        }

        public List<FlightPath> GetPlanetFlightPaths(int planetId)
        {
            return context.FlightPaths
                .Where(fp => fp.DepartureId == planetId || fp.DestinationId == planetId)
                .ToList();
        }

        public int GetFlightPathsCount()
        {
            return context.FlightPaths.Count();
        }

        public void Add(FlightPath flightPath)
        {
            context.FlightPaths.Add(flightPath);
        }

        public void Remove(FlightPath flightPath)
        {
            context.FlightPaths.Remove(flightPath);
        }

        public void RemoveRange(List<FlightPath> flightPaths)
        {
            context.FlightPaths.RemoveRange(flightPaths);
        }






    }
}