using Space101.Enums;
using Space101.Persistence;
using Space101.Repositories;
using System.Net;
using System.Web.Http;
using Space101.DAL;
using Space101.Helper_Models;
using System.Collections.Generic;
using Space101.Models;
using System.Linq;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class StarshipsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly StarshipRepository starshipRepository;
        private readonly FlightRepository flightRepository;
        private readonly TicketRepository ticketRepository;
        private readonly OrderRepository orderRepository;
        private readonly UnitOfWork unitOfWork;

        public StarshipsController()
        {
            context = new ApplicationDbContext();
            starshipRepository = new StarshipRepository(context);
            flightRepository = new FlightRepository(context);
            ticketRepository = new TicketRepository(context);
            orderRepository = new OrderRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //Delete /api/starships/id
        [HttpDelete]
        public IHttpActionResult DeleteStarship(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var starshipDb = starshipRepository.GetSingleStarship(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (starshipDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var flights = flightRepository.GetFlightsByStarship(starshipDb.StarshipId);

            if (flights.Any())
                return BadRequest("The Starshio is used in a Flight!\nTo delete it contact your Database Manager.");

            starshipRepository.Remove(starshipDb);

            unitOfWork.Complete();

            return Ok();
        }

        [Route("api/starships/hardDelete/{id}")]
        [HttpDelete]
        public IHttpActionResult HardDeleteStarship(int? id)
        {
            if (!User.IsInRole(AvailableRoles.DatabaseManager))
                return BadRequest("You have not access to delete the starship.\nPlease contact your Database Manager.");

            //The HardDeleteStarship Action will remove every record in database that relates with the starship
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var starshipDb = starshipRepository.GetSingleStarship(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (starshipDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Flights of the starship must be removed
            var flights = flightRepository.GetFlightsByStarship(starshipDb.StarshipId);

            //Tickets of the flights must be removed
            List<Ticket> flightTickets = GetTicketsForFlights(flights);

            //Orders of the flights must be removed
            ClearOrders(flightTickets);
            ticketRepository.RemoveMultipleTicket(flightTickets);

            flightRepository.RemoveMultipleFlights(flights);

            starshipRepository.Remove(starshipDb);

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
