using AutoMapper;
using Space101.DAL;
using Space101.Dtos;
using Space101.Enums;
using Space101.Helper_Models;
using Space101.Hub_Services;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using Space101.ViewModels.FlightPathViewModels;
using Space101.ViewModels.FlightViewModels;
using Space101.ViewModels.StarshipViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class FlightController : Controller
    {
        private ApplicationDbContext context;
        private FlightRepository flightRepository;
        private FlightSeatRepository flightSeatRepository;
        private FlightStatusRepository flightStatusRepository;
        private FlightPathRepository flightPathRepository;
        private StarshipRepository starshipRepository;
        private UnitOfWork unitOfWork;

        public FlightController()
        {
            context = new ApplicationDbContext();
            flightRepository = new FlightRepository(context);
            flightSeatRepository = new FlightSeatRepository(context);
            flightStatusRepository = new FlightStatusRepository(context);
            flightPathRepository = new FlightPathRepository(context);
            starshipRepository = new StarshipRepository(context);
            unitOfWork = new UnitOfWork(context);
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: Flight
        [AllowAnonymous]
        public ActionResult Index()
        {
            var flights = flightRepository.GetFlights();
            var flightStatuses = flightStatusRepository.GetFlightStatuses();

            var viewModel = new List<FlightViewModel>();

            flights.ForEach(f => viewModel.Add(FlightViewModel.CreateFromModel(f, flightStatuses)));

            return View(viewModel);
        }

        public ActionResult New()
        {
            var lightFlightPaths = InitializeFlightPaths();
            var lightStarships = InitializeStarships();

            var viewModel = new ContainerFlightFormViewModel(new FlightFormViewModel(), lightFlightPaths, lightStarships);

            return View("FlightForm", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var flight = flightRepository.GetSingleFlight(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flight == null)
                return HttpNotFound();

            var lightFlightPaths = InitializeFlightPaths();
            var lightStarships = InitializeStarships();

            var viewModel = new ContainerFlightFormViewModel(new FlightFormViewModel(flight), lightFlightPaths, lightStarships);

            return View("FlightForm", viewModel);
        }

        private List<LightFlightPathViewModel> InitializeFlightPaths()
        {
            var flightPaths = flightPathRepository.GetFlightPaths();

            var viewmodels = new List<LightFlightPathViewModel>();
            flightPaths.ForEach(fp => viewmodels.Add(new LightFlightPathViewModel(fp)));

            return viewmodels;
        }

        private List<LightStarshipViewModel> InitializeStarships()
        {
            var starships = starshipRepository.GetStarships();

            var viewmodels = new List<LightStarshipViewModel>();
            starships.ForEach(s => viewmodels.Add(new LightStarshipViewModel(s)));

            return viewmodels;
        }

        public ActionResult Save(FlightFormViewModel flightFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var lightFlightPaths = InitializeFlightPaths();
                var lightStarships = InitializeStarships();

                var viewModel = new ContainerFlightFormViewModel(flightFormViewModel, lightFlightPaths, lightStarships);
                return View("FlightForm", viewModel);
            }

            Flight flightToDB;

            if (flightFormViewModel.FlightId == 0)
            {
                var starship = starshipRepository.GetSingleStarship(flightFormViewModel.StarshipId);
                flightToDB = new Flight(flightFormViewModel.GetDateTime(), flightFormViewModel.BasePrice, flightFormViewModel.FlightPathId, starship, flightFormViewModel.IsVIP);

                flightRepository.Add(flightToDB);
            }
            else
            {
                flightToDB = flightRepository.GetSingleFlight(flightFormViewModel.FlightId);

                if (flightToDB == null)
                    return HttpNotFound();

                var starship = starshipRepository.GetSingleStarship(flightFormViewModel.StarshipId);
                flightToDB.Update(flightFormViewModel.GetDateTime(), flightFormViewModel.BasePrice, null, starship, null);
            }

            unitOfWork.Complete();

            InformViewsForCreateOrEdit(flightToDB, flightFormViewModel.FlightId);

            return RedirectToAction("Index", "Flight");
        }

        private void InformViewsForCreateOrEdit(Flight flightToDB, int viewModelsId)
        {
            //Get the flight from database with all its relations needed for a flightDto
            var flightToViews = flightRepository.GetSingleFlight(flightToDB.FlightId);

            var flightService = new FlightHubService();
            var flightStatuses = flightStatusRepository.GetFlightStatuses();

            var flightDto = Mapper.Map<Flight, FlightDto>(flightToDB);
            flightDto.FlightStatuses = flightStatuses.Select(s => Mapper.Map<FlightStatus, FlightStatusDto>(s)).ToList();
            flightDto.ConvertDate();

            if (viewModelsId == 0)
                flightService.FlightCreated(flightDto);
            else
                flightService.FlightUpdated(flightDto);

        }

        public ActionResult ManageSeats(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var flight = flightRepository.GetFullFlightWithSeats(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flight == null)
                return HttpNotFound();

            var viewModel = new FullFlightViewModel(flight);

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult ChooseSeats(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var flight = flightRepository.GetFullFlightWithSeats(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flight == null)
                return HttpNotFound();

            var viewModel = new FullFlightViewModel(flight);

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult CheckSeatAvailability(int? FlightId, long[] flightSeatIds)
        {
            if (!FlightId.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var flight = flightRepository.GetFullFlightWithSeats(FlightId ?? (int)InvalidPropertyValues.undefinedValue);

            if (flight == null)
                return HttpNotFound();

            if (flightSeatIds == null)
            {
                var viewModel = new FullFlightViewModel(flight);
                ModelState.AddModelError("MinimunSeats", "You must choose at least one seat.");
                return View("ChooseSeats", viewModel);
            }
               
            var flightSeats = flightSeatRepository.GetAvailableFlightSeatsById(flightSeatIds);

            if(flightSeats.Count != flightSeatIds.Length)
            {
                var viewModel = new FullFlightViewModel(flight);
                ModelState.AddModelError("UnavailableSeats", "One or more of the seats you choose are unavailable.");
                return View("ChooseSeats", viewModel);
            }

            Session["flight"] = flight;
            Session["flightSeats"] = flightSeats;

            //The refresh interval is used for preventing the user refresh the order page
            TempData["RefreshInterval"] = -1;

            return RedirectToAction("Create", "Order");
        }

    }
}