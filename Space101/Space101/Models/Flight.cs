using Space101.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Space101.Models
{
    public class Flight
    {
        public int FlightId { get; private set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; private set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "The Base Price must be a number greater than 0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal BasePrice { get; private set; }

        public int FlightPathId { get; private set; }
        public FlightPath FlightPath { get; private set; }

        public byte FlightStatusId { get; private set; }
        public FlightStatus FlightStatus { get; private set; }

        public int StarshipId { get; private set; }
        public Starship Starship { get; private set; }

        public bool IsVIP { get; private set; }

        public ICollection<FlightSeat> Seats { get; private set; }

        public ICollection<UserFavorite> UserFavorites { get; private set; }

        protected Flight()
        {
            Seats = new List<FlightSeat>();
            UserFavorites = new List<UserFavorite>();
        }

        public Flight(DateTime date, decimal basePrice, int flightPathId, Starship starship, bool isVIP)
        {
            Date = date;
            BasePrice = basePrice;
            FlightPathId = flightPathId;
            StarshipId = starship.StarshipId;
            FlightStatusId = (int)FlightStatusEnum.Canceled;
            IsVIP = isVIP;
            InitializeSeats(starship.PassengerCapacity);
            UserFavorites = new List<UserFavorite>();
        }

        private void InitializeSeats(int numberOfSeats)
        {
            Seats = new List<FlightSeat>();

            if (IsVIP)
            {
                for (int i = 0; i < numberOfSeats; i++)
                {
                    Seats.Add(new FlightSeat(this, new TravelClass("A")));
                }
            }
            else if (numberOfSeats < 50)
            {
                for (int i = 0; i < numberOfSeats; i++)
                {
                    Seats.Add(new FlightSeat(this, new TravelClass("D")));
                }
            }
            else
            {
                for (int i = 0; i < numberOfSeats * 5 / 100; i++)
                {
                    Seats.Add(new FlightSeat(this, new TravelClass("A")));
                }
                for (int i = numberOfSeats * 5 / 100; i < numberOfSeats * 15 / 100; i++)
                {
                    Seats.Add(new FlightSeat(this, new TravelClass("B")));
                }
                for (int i = numberOfSeats * 15 / 100; i < numberOfSeats * 30 / 100; i++)
                {
                    Seats.Add(new FlightSeat(this, new TravelClass("C")));
                }
                for (int i = numberOfSeats * 30 / 100; i < numberOfSeats * 80 / 100; i++)
                {
                    Seats.Add(new FlightSeat(this, new TravelClass("D")));
                }
                for (int i = numberOfSeats * 80 / 100; i < numberOfSeats; i++)
                {
                    Seats.Add(new FlightSeat(this, new TravelClass("E")));
                }
            }
        }

        public void ChangeFlightStatus(int statusId)
        {
            byte status = FlightStatusId;

            if (!byte.TryParse(statusId.ToString(), out status))
                return;

            FlightStatusId = status;
        }

        public void Update(DateTime? date, decimal? basePrice, int? flightPathId, Starship starship, bool? isVIP)
        {
            Date = date ?? Date;
            BasePrice = basePrice ?? BasePrice;
            FlightPathId = flightPathId ?? FlightPathId;
            IsVIP = isVIP ?? IsVIP;
            if(starship != null && starship.StarshipId != StarshipId)
            {
                StarshipId = starship.StarshipId;
                InitializeSeats(starship.PassengerCapacity);
            }
        }

    }
}