using Space101.Controllers;
using Space101.ViewModels.FlightPathViewModels;
using Space101.ViewModels.StarshipViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Space101.ViewModels.FlightViewModels
{
    public class ContainerFlightFormViewModel
    {
        public FlightFormViewModel FlightFormViewModel { get; set; }

        public IEnumerable<LightFlightPathViewModel> FlightPaths { get; set; }
        public IEnumerable<LightStarshipViewModel> Starships { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<PlanetController, ActionResult>> newAction = (c => c.New());
                Expression<Func<PlanetController, ActionResult>> editAction = (c => c.Edit(this.FlightFormViewModel.FlightId));

                var action = (this.FlightFormViewModel.FlightId != 0) ? editAction : newAction;
                var actionName = (action.Body as MethodCallExpression).Method.Name;

                return actionName;
            }
        }

        public ContainerFlightFormViewModel()
        {
            FlightFormViewModel = new FlightFormViewModel();
            FlightPaths = new List<LightFlightPathViewModel>();
            Starships = new List<LightStarshipViewModel>();
        }

        public ContainerFlightFormViewModel(FlightFormViewModel flightFormViewModel, IEnumerable<LightFlightPathViewModel> flightPaths, IEnumerable<LightStarshipViewModel> starships)
        {
            FlightFormViewModel = flightFormViewModel;
            FlightPaths = flightPaths;
            Starships = starships;
        }
    }
}