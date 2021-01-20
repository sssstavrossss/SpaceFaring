using Space101.Enums;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using Space101.ViewModels.FlightPathViewModels;
using Space101.ViewModels.PlanetViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Space101.DAL;
using Space101.Helper_Models;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class FlightPathController : Controller
    {
        private ApplicationDbContext context;
        private FlightPathRepository flightPathRepository;
        private PlanetRepository planetRepository;
        private UnitOfWork unitOfWork;

        public FlightPathController()
        {
            context = new ApplicationDbContext();
            flightPathRepository = new FlightPathRepository(context);
            planetRepository = new PlanetRepository(context);
            unitOfWork = new UnitOfWork(context);
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: FlightPath
        [AllowAnonymous]
        public ActionResult Index()
        {
            var flightPaths = flightPathRepository.GetFlightPaths();

            var flightPathViewModels = new List<FlightPathViewModel>();

            flightPaths.ForEach(fp => flightPathViewModels.Add(FlightPathViewModel.CreateFromModel(fp)));

            return View(flightPathViewModels);
        }

        public ActionResult New()
        {
            var flightPathFormViewModel = InitializeNewContainerFormViewModel();

            return View("FlightPathForm", flightPathFormViewModel);
        }

        [HttpPost]
        public ActionResult Save(FlightPathFormViewModel flightPathFormViewModel)
        {
            bool exists = flightPathRepository.GetFlightPathByUniqueIndex(flightPathFormViewModel.DestinationId, flightPathFormViewModel.DepartureId) != null;
            if (exists && flightPathFormViewModel.FlightPathId == 0) //It applies only for New FlightPath not Edit
            {
                var containerViewModel = InitializeNewContainerFormViewModel(flightPathFormViewModel);

                ModelState.AddModelError("FlightPath.DestinationId", "This Destination is already part of this flight path");
                return View("FlightPathForm", containerViewModel);
            }

            if (!ModelState.IsValid)
            {
                var containerViewModel = InitializeNewContainerFormViewModel(flightPathFormViewModel);
                return View("FlightPathForm", containerViewModel);
            }

            if (flightPathFormViewModel.FlightPathId == 0)
            {
                var planets = planetRepository.GetPlanetsByIdRaw(flightPathFormViewModel.DepartureId, flightPathFormViewModel.DestinationId);
                if(planets.Count != 2)
                    return HttpNotFound();
                var flightPath = new FlightPath(planets.Single(p => p.PlanetID == flightPathFormViewModel.DepartureId),planets.Single(p => p.PlanetID == flightPathFormViewModel.DestinationId));
                flightPathRepository.Add(flightPath);
            }

            unitOfWork.Complete();

            return RedirectToAction("Index", "FlightPath");
        }
        private ContainerFlightPathFormViewModel InitializeNewContainerFormViewModel(FlightPathFormViewModel viewModel = null)
        {
            var allPlanets = planetRepository.GetPlanetsRaw();

            var availableDepartures = new List<LightPlanetViewModel>();

            allPlanets.ForEach(p => availableDepartures.Add(LightPlanetViewModel.CreateFromModel(p)));

            var flightPathFormViewModel = viewModel ?? new FlightPathFormViewModel();
            return new ViewModels.FlightPathViewModels.ContainerFlightPathFormViewModel(flightPathFormViewModel, availableDepartures);
        }
    }
}