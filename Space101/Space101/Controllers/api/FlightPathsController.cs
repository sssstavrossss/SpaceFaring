using AutoMapper;
using Space101.Dtos;
using Space101.Models;
using System.Linq;
using System.Net;
using System.Web.Http;
using Space101.Repositories;
using Space101.Persistence;
using Space101.Enums;
using Space101.DAL;
using Space101.Helper_Models;
using System.Collections.Generic;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class FlightPathsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly PlanetRepository planetRepository;
        private readonly FlightPathRepository flightPathRepository;
        private readonly FlightRepository flightRepository;
        private readonly TicketRepository ticketRepository;
        private readonly OrderRepository orderRepository;
        private readonly UnitOfWork unitOfWork;

        public FlightPathsController()
        {
            context = new ApplicationDbContext();
            planetRepository = new PlanetRepository(context);
            flightPathRepository = new FlightPathRepository(context);
            flightRepository = new FlightRepository(context);
            ticketRepository = new TicketRepository(context);
            orderRepository = new OrderRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [Route("api/flightpaths/destinations/{departureId}")]
        [HttpGet]
        public IHttpActionResult AvailableDestinations(int? departureId)
        {
            if (!departureId.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var allPlanets = planetRepository.GetPlanetsWithDestinations();

            var departureFlightPaths = allPlanets
                .Single(p => p.PlanetID == departureId)
                .Destinations
                .Select(fp => fp.DestinationId)
                .ToList();

            var availableDestinations = allPlanets
                .Where(p => !departureFlightPaths.Contains(p.PlanetID) && p.PlanetID != departureId)
                .Select(Mapper.Map<Planet, AvailableDestinationDto>)
                .ToList();

            return Ok(availableDestinations);
        }

        [Route("api/flightpaths/findDistance/{destinationId}/{departureId}")]
        [HttpGet]
        public IHttpActionResult GetDistance(int? destinationId, int? departureId)
        {
            if (!departureId.HasValue || !destinationId.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var planets = planetRepository.GetPlanetsByIdRaw(departureId ?? (int)InvalidPropertyValues.undefinedValue, destinationId ?? (int)InvalidPropertyValues.undefinedValue);

            if(planets.Count != 2)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var distance = FlightPath.FindDistance(planets[0], planets[1]);

            return Ok(distance);
        }

        //Delete /api/flightpaths/id
        [HttpDelete]
        public IHttpActionResult DeleteFlightPath(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flightPathDb = flightPathRepository.GetSingleFlightPath(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flightPathDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var flights = flightRepository.GetFlightsByFlightPath(flightPathDb.FlightPathId);

            if (flights.Any())
                return BadRequest("The Flightpath is used in a Flight!\nTo delete it contact your Database Manager.");

            flightPathRepository.Remove(flightPathDb);

            unitOfWork.Complete();

            return Ok();
        }

        [Route("api/flightpaths/hardDelete/{id}")]
        [HttpDelete]
        public IHttpActionResult HardDeleteFlightPath(int? id)
        {
            if (!User.IsInRole(AvailableRoles.DatabaseManager))
                return BadRequest("You have not access to delete the flightpath.\nPlease contact your Database Manager.");

            //The HardDeleteFlightPath Action will remove every record in database that relates with the flightpath
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flightPathDb = flightPathRepository.GetSingleFlightPath(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flightPathDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Flights of the flightPath must be removed
            var flights = flightRepository.GetFlightsByFlightPath(flightPathDb.FlightPathId);

            //Tickets of the flights must be removed
            List<Ticket> flightTickets = GetTicketsForFlights(flights);

            //Orders of the flights must be removed
            ClearOrders(flightTickets);
            ticketRepository.RemoveMultipleTicket(flightTickets);
            flightRepository.RemoveMultipleFlights(flights);

            flightPathRepository.Remove(flightPathDb);

            unitOfWork.Complete();

            return Ok();
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

    }
}
