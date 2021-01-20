using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Space101.Enums;

namespace Space101.Dtos.BatabaseStatisticsDtos
{
    public class FlightStatisticsDto
    {
        public long FlightId { get; set; }
        public double OcupancyRate { get; set; }
        public decimal ExpectedRevenue { get; set; }
        public decimal RevenuePerDistance { get; set; }
        public decimal TicketRevenue { get; set; }

        public FlightStatisticsDto()
        { }

        public FlightStatisticsDto(Flight flight, IEnumerable<Ticket> tickets)
        {
            FlightId = flight.FlightId;
            OcupancyRate = CalculateClosedSeatsPersentage(flight.Seats);
            ExpectedRevenue = CalculateMaximumRevenue(flight.Seats);
            RevenuePerDistance = CalculateRevenuePerDistance(flight.Seats,flight.FlightPath);
            TicketRevenue = CalculateTicketRevenue(tickets);
        }

        private double CalculateClosedSeatsPersentage(IEnumerable<FlightSeat> flightSeats)
        {
            if (flightSeats == null || flightSeats.Count() == 0)
                return (double)InvalidPropertyValues.undefinedValue;

            var result = flightSeats.Where(s => !s.IsAvailable).Count() / (double)flightSeats.Count();
            return Math.Round(result,2,MidpointRounding.AwayFromZero);
        }
        private decimal CalculateMaximumRevenue(IEnumerable<FlightSeat> flightSeats)
        {
            if (flightSeats == null)
                return (decimal)InvalidPropertyValues.undefinedValue;

            var result = flightSeats.Sum(s => s.GetPrice());
            return Math.Round(result,2,MidpointRounding.AwayFromZero);
        }
        private decimal CalculateRevenuePerDistance(IEnumerable<FlightSeat> flightSeats, FlightPath flightpath)
        {
            if (flightSeats == null || flightSeats.Count() == 0)
                return (decimal)InvalidPropertyValues.undefinedValue;

            var flightIncome = flightSeats.Sum(s => s.GetPrice());

            if (flightpath == null || flightpath.Distance == 0)
                return (decimal)InvalidPropertyValues.undefinedValue;

            var result = flightIncome / flightpath.Distance;
            return Math.Round(result,2,MidpointRounding.AwayFromZero);
        }
        private decimal CalculateTicketRevenue(IEnumerable<Ticket> tickets)
        {
            if (tickets == null || tickets.Count() == 0)
                return (decimal)InvalidPropertyValues.undefinedValue;

            var ticketRevenue = tickets.Sum(t => t.Price);

            return Math.Round(ticketRevenue, 2, MidpointRounding.AwayFromZero);
        }

    }
}