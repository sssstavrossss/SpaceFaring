using Space101.Models;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.FlightPathViewModels
{
    public class FlightPathFormViewModel
    {
        public int FlightPathId { get; set; }

        [Display(Name = "Departure Planet")]
        [Required(ErrorMessage = "The Departure Planet is required")]
        public int DepartureId { get; set; }

        [Display(Name = "Departure Planet")]
        public string DepartureName { get; set; }

        [Display(Name = "Destination Planet")]
        [Required(ErrorMessage = "The Destination Planet is required")]
        public int DestinationId { get; set; }

        [Display(Name = "Destination Planet")]
        public string DestinationName { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Distance { get; set; }

        public FlightPathFormViewModel()
        { }

        private FlightPathFormViewModel(int flightPathId,int departureId, string departureName, int destinationId, string destinationName)
        {
            FlightPathId = flightPathId;
            DepartureId = departureId;
            DepartureName = departureName;
            DestinationId = destinationId;
            DestinationName = destinationName;
        }

        private FlightPathFormViewModel(FlightPath flightPath)
        {
            FlightPathId = flightPath.FlightPathId;
            DepartureId = flightPath.DepartureId;
            DepartureName = flightPath.Departure.Name;
            DestinationId = flightPath.DestinationId;
            DestinationName = flightPath.Destination.Name;
            Distance = flightPath.Distance;
        }

        public static FlightPathFormViewModel Create(int flightPathId, int departureId, string departureName, int destinationId, string destinationName, decimal distance)
        {
            return new FlightPathFormViewModel(flightPathId, departureId, departureName, destinationId, destinationName);
        }

        public static FlightPathFormViewModel CreateFromModel(FlightPath flightPath)
        {
            return new FlightPathFormViewModel(flightPath);
        }
    }
}