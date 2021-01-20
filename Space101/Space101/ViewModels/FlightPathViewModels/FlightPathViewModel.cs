using Space101.Models;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.FlightPathViewModels
{
    public class FlightPathViewModel
    {
        [Display(Name = "Flight Path Id")]
        public int FlightPathId { get; set; }

        [Display(Name = "Departure Planet")]
        public int DepartureId { get; set; }

        [Display(Name = "Departure Planet")]
        public string DepartureName { get; set; }

        [Display(Name = "Destination Planet")]
        public int DestinationId { get; set; }

        [Display(Name = "Destination Planet")]
        public string DestinationName { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Distance { get; set; }

        private FlightPathViewModel()
        { }

        private FlightPathViewModel(int flightPathId, int departureId, string departureName, int destinationId, string destinationName, decimal distance)
        {
            FlightPathId = flightPathId;
            DepartureId = departureId;
            DepartureName = departureName;
            DestinationId = destinationId;
            DestinationName = destinationName;
            Distance = distance;
        }

        private FlightPathViewModel(FlightPath flightPath)
        {
            FlightPathId = flightPath.FlightPathId;
            DepartureId = flightPath.DepartureId;
            DepartureName = flightPath.Departure.Name;
            DestinationId = flightPath.DestinationId;
            DestinationName = flightPath.Destination.Name;
            Distance = flightPath.Distance;
        }

        public static FlightPathViewModel Create(int flightPathId, int departureId, string departureName, int destinationId, string destinationName, decimal distance)
        {
            return new FlightPathViewModel(flightPathId, departureId, departureName, destinationId, destinationName, distance);
        }

        public static FlightPathViewModel CreateFromModel(FlightPath flightPath)
        {
            return new FlightPathViewModel(flightPath);
        }
    }
}