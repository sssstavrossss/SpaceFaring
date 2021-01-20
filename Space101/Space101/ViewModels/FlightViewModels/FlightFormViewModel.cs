using Space101.Custom_Validations;
using Space101.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.FlightViewModels
{
    public class FlightFormViewModel
    {
        public int FlightId { get; set; }

        [Required(ErrorMessage = "The Date is Required")]
        [FutureDate]
        public string Date { get; set; }

        [Required(ErrorMessage = "The Time is Required")]
        [ValidTime]
        public string Time { get; set; }

        [Display(Name = "Base Price")]
        [Range(0.0, double.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal BasePrice { get; set; }

        public int FlightPathId { get; set; }
        public int StarshipId { get; set; }

        [Display(Name = "VIP Flight")]
        public bool IsVIP { get; set; }

        public FlightFormViewModel()
        { }

        public FlightFormViewModel(string date, string time, decimal basePrice, int flightPathId, int starshipId, bool isVip)
        {
            Date = date;
            Time = time;
            BasePrice = basePrice;
            FlightPathId = flightPathId;
            StarshipId = starshipId;
            IsVIP = isVip;
        }

        public FlightFormViewModel(Flight flight)
        {
            FlightId = flight.FlightId;
            Date = flight.Date.ToString();
            Time = flight.Date.TimeOfDay.ToString();
            BasePrice = flight.BasePrice;
            FlightPathId = flight.FlightPathId;
            StarshipId = flight.StarshipId;
            IsVIP = flight.IsVIP;
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}