using Space101.Models;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.TicketViewModels
{
    public class TicketFormViewModel
    {
        public long TicketId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The Passenger's First Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Passenger's First Name must have 2 to 50 letters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The Passenger's Last Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Passenger's Last Name must have 2 to 50 letters")]
        public string LastName { get; set; }

        [Display(Name = "Passenger's Race")]
        [Required(ErrorMessage = "The Passenger's Race is required")]
        public int RaceId { get; set; }

        [Required(ErrorMessage = "The Passenger's Seat is required")]
        public long SeatId { get; set; }
        public FlightSeat Seat { get; set; }

        [Display(Name = "Home Planet")]
        [Required(ErrorMessage = "The Passenger's Home Planet is required")]
        public int PlanetId { get; set; }

        public int FlightId { get; set; }
        public decimal Price { get; set; }

        public TicketFormViewModel()
        { }

        public TicketFormViewModel(string firstName, string lastName, int seatId, int raceId, int planetId, int flightId, decimal price)
        {
            FirstName = firstName;
            LastName = lastName;
            SeatId = seatId;
            RaceId = raceId;
            PlanetId = planetId;
            FlightId = flightId;
            Price = price;
        }

        public TicketFormViewModel(Ticket ticket)
        {
            FirstName = ticket.FirstName;
            LastName = ticket.LastName;
            RaceId = ticket.PassengerRaceId;
            PlanetId = ticket.PassengerPlanetId;
            FlightId = ticket.FlightId;
            SeatId = ticket.SeatId;
        }

        public TicketFormViewModel(int flightId, FlightSeat seat )
        {
            FlightId = flightId;
            SeatId = seat.FlightSeatId;
            Seat = seat;
            Price = seat.GetPrice();
        }

        public decimal GetPrice()
        {
            return Seat.GetPrice();
        }
    }
}