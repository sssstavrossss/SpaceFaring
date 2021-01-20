using Space101.DAL;
using Space101.Dtos.BatabaseStatisticsDtos;
using Space101.Helper_Models;
using Space101.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class DatabaseStatisticsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly ApplicationUserRepository ApplicationUserRepository;
        private readonly ClimateRepository climateRepository;
        private readonly TerrainRepository terrainRepository;
        private readonly TicketRepository ticketRepository;
        private readonly FlightRepository flightRepository;
        private readonly OrderRepository orderRepository;
        private readonly RaceRepository raceRepository;
        private readonly RaceClassificationRepository raceClassificationRepository;
        private readonly StarshipRepository starshipRepository;
        private readonly FlightPathRepository flightPathRepository;
        private readonly PlanetRepository planetRepository;

        public DatabaseStatisticsController()
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

        [Route("api/DatabaseStatistics/PlanetsPerClimate")]
        public IHttpActionResult GetPlanetsPerClimate()
        {
            var climates = climateRepository.GetClimatesWithPlanets();
            var viewModel = climates.Select(c => new PlanetsPerClimateDto() { ClimateName = c.Name, NumberOfPlanets = c.ClimateZones.Count, DisplayColor = c.DisplayColor })
                .ToList();

            return Ok(viewModel);
        }

        [Route("api/DatabaseStatistics/PlanetsPerTerrain")]
        public IHttpActionResult GetPlanetsPerTerrain()
        {
            var terrains = terrainRepository.GetTerrainsWithPlanets();
            var viewModel = terrains.Select(t => new PlanetsPerTerrainDto() { TerrainName = t.Name, NumberOfPlanets = t.SurfaceMorphologies.Count, DisplayColor = t.DisplayColor })
                .ToList();
            return Ok(viewModel);
        }

        [Route("api/DatabaseStatistics/FlightsPerStatus")]
        public IHttpActionResult GetFlightsPerStatus()
        {
            var flights = flightRepository.GetFlightStatusesWithFlights();
            var viewModel = flights.Select(fs => new FlightsPerStatusDto() { StatusName = fs.StatusName, FlightsNumber = fs.Flights.Count })
                .ToList();
            return Ok(viewModel);
        }

        [Route("api/DatabaseStatistics/FlightsStatistics")]
        public IHttpActionResult GetFlightsStatistics()
        {
            var flightsDB = flightRepository.GetFullFlightsWithSeats();
            var ticketsPerFlight = ticketRepository.GetAllTicketsPerFlight();
            var dtoList = new List<FlightStatisticsDto>();

            foreach (var flight in flightsDB)
            {
                var flightTickets = ticketsPerFlight.SingleOrDefault(t => t.Key == flight.FlightId);
                
                dtoList.Add(new FlightStatisticsDto(flight, flightTickets));
            }

            return Ok(dtoList);
        }

        [Route("api/DatabaseStatistics/FlightsPerDate")]
        public IHttpActionResult GetFlightsPerDate()
        {
            var flightsDB = flightRepository.GetFlights();
            var dateGroups = flightsDB.GroupBy(f => f.Date.Date, f => f).ToList();
            var dtoList = new List<FlightsPerDateDto>();

            foreach (var date in dateGroups)
            {
                dtoList.Add(new FlightsPerDateDto(date.Key, date.Count()));
            }
            return Ok(dtoList);
        }

        [Route("api/DatabaseStatistics/DatabaseNumbers")]
        public IHttpActionResult GetDatabaseNumbers()
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

            var dto = new DatabaseNumbersDto()
            {
                DatabaseTimeStamp = DateTime.Now.ToString(),
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
            return Ok(dto);
        }

        [AllowAnonymous]
        [Route("api/DatabaseStatistics/GetUserFrontPage")]
        public IHttpActionResult GetUserFrontPage()
        {
            var planets = planetRepository.GetPlanetsCount();
            var flights = flightRepository.GetFlightsCount();
            var races = raceRepository.GetRacesCount(); 
            
            var dto = new DatabaseNumbersDto()
            {
                Planets = planets,
                Flights = flights,
                Races = races
            };

            return Ok(dto);
        }

    }
}
