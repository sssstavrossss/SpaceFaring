using Space101.Models;
using Space101.ViewModels.FlightPathViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.FlightViewModels
{
    public class ContainerFlightFormViewModel
    {
        public FlightFormViewModel FlightFormViewModel { get; set; }
        public IEnumerable<LightFlightPathViewModel> FlightPaths { get; set; }

        public ContainerFlightFormViewModel()
        {
            FlightFormViewModel = new FlightFormViewModel();
            FlightPaths = new List<LightFlightPathViewModel>();
        }

        public ContainerFlightFormViewModel(FlightFormViewModel flightFormViewModel, IEnumerable<LightFlightPathViewModel> flightPaths)
        {
            FlightFormViewModel = flightFormViewModel;
            FlightPaths = flightPaths;
        }
    }
}