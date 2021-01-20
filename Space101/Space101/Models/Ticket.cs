using Space101.ViewModels.TicketViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class Ticket
    {
        public long TicketId { get; private set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The Passenger's First Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Passenger's First Name must have 2 to 50 letters")]
        public string FirstName { get; private set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The Passenger's Last Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The Passenger's Last Name must have 2 to 50 letters")]
        public string LastName { get; private set; }

        [Display(Name = "Passenger's Race")]
        [Required(ErrorMessage = "The Passenger's Race is required")]
        [ForeignKey("PassengerRace")]
        public int PassengerRaceId { get; private set; }
        public Race PassengerRace { get; private set; }

        [Required(ErrorMessage = "The Passenger's Seat is required")]
        [ForeignKey("Seat")]
        public long SeatId { get; private set; }
        public FlightSeat Seat { get; private set; }

        [Display(Name = "Home Planet")]
        [Required(ErrorMessage = "The Passenger's Home Planet is required")]
        [ForeignKey("PassengerPlanet")]
        public int PassengerPlanetId { get; private set; }
        public Planet PassengerPlanet { get; private set; }

        [ForeignKey("Flight")]
        public int FlightId { get; private set; }
        public Flight Flight { get; private set; }

        public decimal Price { get; private set; }

        protected Ticket()
        { }

        public Ticket(string firstName, string lastName, int seatId, int raceId, int planetId, int flightId, decimal price)
        {
            FirstName = firstName;
            LastName = lastName;
            SeatId = seatId;
            PassengerRaceId = raceId;
            PassengerPlanetId = planetId;
            FlightId = flightId;
            Price = price;
        }

        public Ticket(TicketFormViewModel ticketFormViewModel)
        {
            FirstName = ticketFormViewModel.FirstName;
            LastName = ticketFormViewModel.LastName;
            SeatId = ticketFormViewModel.SeatId;
            PassengerRaceId = ticketFormViewModel.RaceId;
            PassengerPlanetId = ticketFormViewModel.PlanetId;
            FlightId = ticketFormViewModel.FlightId;
            Price = Math.Round(ticketFormViewModel.Price,2);
        }
    }
}