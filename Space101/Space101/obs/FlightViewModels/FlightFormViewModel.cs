using Space101.Custom_Validations;
using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        public FlightFormViewModel()
        { }

        public FlightFormViewModel(string date, string time, decimal basePrice, int flightPathId)
        {
            Date = date;
            Time = time;
            BasePrice = basePrice;
            FlightPathId = flightPathId;
        }

        public FlightFormViewModel(Flight flight)
        {
            FlightId = flight.FlightId;
            Date = flight.Date.ToString();
            Time = flight.Date.TimeOfDay.ToString();
            BasePrice = flight.BasePrice;
            FlightPathId = flight.FlightPathId;
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}