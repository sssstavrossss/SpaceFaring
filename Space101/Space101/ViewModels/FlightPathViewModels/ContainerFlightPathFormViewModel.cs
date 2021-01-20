using Space101.ViewModels.PlanetViewModels;
using System.Collections.Generic;

namespace Space101.ViewModels.FlightPathViewModels
{
    public class ContainerFlightPathFormViewModel
    {
        public FlightPathFormViewModel FlightPathFormViewModel { get; set; }

        public IEnumerable<LightPlanetViewModel> DeparturePlanets { get; set; }
        public IEnumerable<LightPlanetViewModel> DestinationPlanets { get; set; }

        public ContainerFlightPathFormViewModel()
        {
            FlightPathFormViewModel = new FlightPathFormViewModel();
            DeparturePlanets = new List<LightPlanetViewModel>();
            DestinationPlanets = new List<LightPlanetViewModel>();
        }

        public ContainerFlightPathFormViewModel(FlightPathFormViewModel flightPathViewModel, IEnumerable<LightPlanetViewModel> departurePlanets = null, IEnumerable<LightPlanetViewModel> destinationPlanets = null)
        {
            FlightPathFormViewModel = flightPathViewModel;
            DeparturePlanets = departurePlanets ?? new List<LightPlanetViewModel>();
            DestinationPlanets = destinationPlanets ?? new List<LightPlanetViewModel>();
        }
    }
}