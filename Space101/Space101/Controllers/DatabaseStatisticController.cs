using Space101.DAL;
using Space101.Helper_Models;
using Space101.Repositories;
using Space101.ViewModels.DataBaseStatisticsViewModels;
using System;
using System.Web.Mvc;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class DatabaseStatisticController : Controller
    {
        private ApplicationDbContext context;
        private ApplicationUserRepository ApplicationUserRepository;
        private ClimateRepository climateRepository;
        private TerrainRepository terrainRepository;
        private TicketRepository ticketRepository;
        private FlightRepository flightRepository;
        private OrderRepository orderRepository;
        private RaceRepository raceRepository;
        private RaceClassificationRepository raceClassificationRepository;
        private StarshipRepository starshipRepository;
        private FlightPathRepository flightPathRepository;
        private PlanetRepository planetRepository;

        public DatabaseStatisticController()
        {
            context = new ApplicationDbContext();
            ApplicationUserRepository = new ApplicationUserRepository(context);
            climateRepository = new ClimateRepository(context);
            terrainRepository = new TerrainRepository(context);
            ticketRepository = new TicketRepository(context);
            flightRepository = new FlightRepository(context);
            orderRepository = new OrderRepository(context);
            raceRepository = new RaceRepository(context);
            raceClassificationRepository = new RaceClassificationRepository(context);
            starshipRepository = new StarshipRepository(context);
            flightPathRepository = new FlightPathRepository(context);
            planetRepository = new PlanetRepository(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: DatabaseStatistic
        public ActionResult Index()
        {
            var planets = planetRepository.GetPlanetsCount();
            var flights = flightRepository.GetFlightsCount();
            var flightPaths = flightPathRepository.GetFlightPathsCount();
            var starships = starshipRepository.GetStarshipsCount();
            var climates = climateRepository.GetClimatesCount();
            var terrains = terrainRepository.GetTerrainsCount();
            var races = raceRepository.GetRacesCount();
            var raceClassifications = raceClassificationRepository.GetRaceClassificationCount();
            var users = ApplicationUserRepository.GetApplicationUsersCount();
            var orders = orderRepository.GetOrdersCount();
            var tickets = ticketRepository.GetTicketsCount();
            var revenue = orderRepository.GetTotalOrderRevenue();

            var viewModel = new DatabaseNumbersViewModel()
            {
                DatabaseTimeStamp = DateTime.Now,
                Planets = planets,
                Flights = flights,
                Flightpaths = flightPaths,
                Starships = starships,
                Climates = climates,
                Terrains = terrains,
                Races = races,
                RaceClassification = raceClassifications,
                Users = users,
                Orders = orders,
                Tickets = tickets,
                Revenue = Math.Round(revenue, 2, MidpointRounding.AwayFromZero)
            };

            return View(viewModel);
        }
    }
}