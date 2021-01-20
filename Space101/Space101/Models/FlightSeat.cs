using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class FlightSeat
    {
        public long FlightSeatId { get; private set; }

        public int FlightId { get; private set; }
        public Flight Flight { get; private set; }

        [ForeignKey("TravelClass")]
        public string TravelClassId { get; private set; }
        public TravelClass TravelClass { get; private set; }

        public bool IsAvailable { get; private set; }

        public string Code
        {
            get
            {
                return $"F{FlightId}S{FlightSeatId}";
            }
        }

        protected FlightSeat()
        { }

        public FlightSeat(Flight flight, TravelClass travelClass)
        {
            FlightId = flight.FlightId;
            TravelClassId = travelClass.ClassId;
            IsAvailable = true;
        }

        public decimal GetPrice()
        {
            return Flight.BasePrice * (decimal)TravelClass.BasePriceRate;
        }

        public void HoldSeat()
        {
            IsAvailable = false;
        }
        public void ReleaseSeat()
        {
            IsAvailable = true;
        }
    }
}