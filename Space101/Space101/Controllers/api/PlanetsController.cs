using Space101.Models;
using System.Linq;
using System.Net;
using System.Web.Http;
using Space101.Repositories;
using Space101.Persistence;
using Space101.Enums;
using Space101.DAL;
using Space101.Services;
using System.Web.Hosting;
using Space101.Dtos;
using AutoMapper;
using Space101.Helper_Models;
using System.Collections.Generic;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class PlanetsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly PlanetRepository planetRepository;
        private readonly FlightPathRepository flightPathRepository;
        private readonly FilePathRepository filePathRepository;
        private readonly FlightRepository flightRepository;
        private readonly TicketRepository ticketRepository;
        private readonly OrderRepository orderRepository;
        private readonly FileManager fileManager;
        private readonly UnitOfWork unitOfWork;

        public PlanetsController()
        {
            context = new ApplicationDbContext();
            planetRepository = new PlanetRepository(context);
            flightPathRepository = new FlightPathRepository(context);
            filePathRepository = new FilePathRepository(context);
            flightRepository = new FlightRepository(context);
            ticketRepository = new TicketRepository(context);
            orderRepository = new OrderRepository(context);
            fileManager = new FileManager(HostingEnvironment.MapPath("~/App_Assets/"));
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //Delete /api/planets/id
        [HttpDelete]
        public IHttpActionResult DeletePlanet(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var planetDB = planetRepository.GetFullDataPlanet(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (planetDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var flightPaths = flightPathRepository.GetPlanetFlightPaths(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flightPaths.Any())
                return BadRequest("The Planet is part of a Flightpath!\nTo delete it contact your Database Manager.");

            var tickets = ticketRepository.GetTicketsByPlanet(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (tickets.Any())
                return BadRequest("The Planet is used by a Ticket!\nTo delete it contact your Database Manager.");

            foreach (var item in planetDB.Assets)
            {
                fileManager.DeleteFile(item);
                filePathRepository.RemoveFilepath(item.FilePath);
            }

            fileManager.DeleteFolders(ModelType.Planet, planetDB.PlanetID.ToString());

            planetRepository.Remove(planetDB);

            unitOfWork.Complete();

            return Ok();
        }

        [Route("api/planets/hardDelete/{id}")]
        [HttpDelete]
        public IHttpActionResult HardDeletePlanet(int? id)
        {
            if (!User.IsInRole(AvailableRoles.DatabaseManager))
                return BadRequest("You have not access to delete the planet.\nPlease contact your Database Manager.");

            //The hardDeletePlanet Action will remove every record in database that relates with the planet
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var planetDB = planetRepository.GetFullDataPlanet(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (planetDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //No cascade delete in flightpaths
            var flightPathsToDelete = flightPathRepository.GetPlanetFlightPaths(id ?? (int)InvalidPropertyValues.undefinedValue);

            //Flights of the planet must be removed
            var flights = GetFlightsForFlightPaths(flightPathsToDelete);

            //Tickets of the flights and of homeplanet passengers must be removed
            var tickets = ticketRepository.GetTicketsByPlanet(id ?? (int)InvalidPropertyValues.undefinedValue);
            List<Ticket> flightTickets = GetTicketsForFlights(flights);
            tickets.AddRange(flightTickets);

            //Orders of the flights must be removed
            ClearOrders(tickets);
            ticketRepository.RemoveMultipleTicket(tickets);
            flightRepository.RemoveMultipleFlights(flights);

            foreach (var item in planetDB.Assets)
            {
                fileManager.DeleteFile(item);
                filePathRepository.RemoveFilepath(item.FilePath);
            }

            fileManager.DeleteFolders(ModelType.Planet, planetDB.PlanetID.ToString());

            flightPathRepository.RemoveRange(flightPathsToDelete);
            planetRepository.Remove(planetDB);

            unitOfWork.Complete();

            return Ok();
        }
        private List<Flight> GetFlightsForFlightPaths(List<FlightPath> flightPaths)
        {
            var flightPathIds = flightPaths.Select(f => f.FlightPathId).ToArray();
            var flights = flightRepository.GetFlightsByFlightPath(flightPathIds);
            return flights;
        }
        private List<Ticket> GetTicketsForFlights(List<Flight> flights)
        {
            var flightsIds = flights.Select(f => f.FlightId).ToArray();
            var flightTickets = ticketRepository.GetAllTicketsOfMultipleFlights(flightsIds);
            return flightTickets;
        }
        private void ClearOrders(IEnumerable<Ticket> tickets)
        {
            var ids = tickets.Select(t => t.TicketId);
            var orders = ticketRepository.GetTicketsOrder(ids.ToArray());
            orderRepository.RemoveMultipleOrders(orders);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var planets = planetRepository.GetAllFullDataPlanets();

            var planetsDto = planets
                .Select(Mapper.Map<Planet, PlanetDto>).ToList();

            return Ok(planetsDto);
        }

    }
}
