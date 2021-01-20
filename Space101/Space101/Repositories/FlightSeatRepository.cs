using Space101.DAL;
using Space101.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Space101.Repositories
{
    public class FlightSeatRepository
    {
        private ApplicationDbContext context;

        public FlightSeatRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<FlightSeat> GetFlightSeats(int flightId)
        {
            return context.FlightSeats
                .Include(fs => fs.TravelClass)
                .Where(fs => fs.FlightId == flightId)
                .ToList();
        }

        public List<FlightSeat> GetAvailableFlightSeatsById(params long[] flightSeatIds)
        {
            return context.FlightSeats
                .Where(fs => flightSeatIds.ToList().Contains(fs.FlightSeatId) && fs.IsAvailable)
                .ToList();
        }

        public List<FlightSeat> GetFlightSeatsById(params long[] flightSeatIds)
        {
            return context.FlightSeats
                .Where(fs => flightSeatIds.ToList().Contains(fs.FlightSeatId))
                .ToList();
        }

        public FlightSeat GetFlightSeat(int flightId, int seatId)
        {
            return context.FlightSeats
                .SingleOrDefault(fs => fs.FlightId == flightId && fs.FlightSeatId == seatId);
        }

        public void ModifySeats(params FlightSeat[] seats)
        {

            foreach (var seat in seats)
            {
                context.Entry(seat).State = EntityState.Modified;
            }
        }

        public void RemoveSeat(FlightSeat seat)
        {
            context.FlightSeats.Remove(seat);
        }

        public void RemoveMultipleSeats(List<FlightSeat> seats)
        {
            foreach (var seat in seats)
            {
                context.FlightSeats.Remove(seat);
            }
        }

    }
}