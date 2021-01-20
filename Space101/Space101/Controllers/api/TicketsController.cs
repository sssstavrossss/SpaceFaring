using Space101.DAL;
using Space101.Enums;
using Space101.Helper_Models;
using Space101.Hub_Services;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class TicketsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly TicketRepository ticketRepository;
        private readonly OrderRepository orderRepository;
        private readonly UnitOfWork unitOfWork;

        public TicketsController()
        {
            context = new ApplicationDbContext();
            ticketRepository = new TicketRepository(context);
            orderRepository = new OrderRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpDelete]
        public IHttpActionResult DeleteTicket(int? id)
        {
            if(!User.IsInRole(AvailableRoles.DatabaseManager))
                return BadRequest("You have not access to delete the ticket.\nPlease contact your Database Manager.");

            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var ticketDb = ticketRepository.GetTicketById(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (ticketDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var releasedSeat = new List<long>();
            var flight = ticketDb.Flight;

            bool shoulReleaseSeat = flight.FlightStatusId == (int)FlightStatusEnum.Closed || flight.FlightStatusId == (int)FlightStatusEnum.OnScedule;

            if (shoulReleaseSeat)
                releasedSeat = ReleaseTicketSeat(ticketDb, flight).ToList();

            ticketRepository.RemoveTicket(ticketDb);
            var ticketsOrder = ticketRepository.GetTicketsOrder(ticketDb.TicketId).FirstOrDefault();

            if (ticketsOrder.Tickets.Count == 1)
                orderRepository.Remove(ticketsOrder);

            unitOfWork.Complete();

            if (shoulReleaseSeat)
                InformViewsForReleaseSeat(flight, releasedSeat);

            return Ok();
        }
        private void InformViewsForReleaseSeat(Flight flight, IEnumerable<long> releasedSeat)
        {
            var flightService = new FlightHubService();
            flightService.FlightSeatsOpened(releasedSeat.ToArray());
            flightService.FligthStatusChanged(flight.FlightId, (int)FlightStatusEnum.OnScedule);
        }
        private IEnumerable<long> ReleaseTicketSeat(Ticket ticket, Flight flight)
        {
            var releasedSeat = new List<long>();
            ticket.Seat.ReleaseSeat();
            releasedSeat.Add(ticket.SeatId);
            flight.ChangeFlightStatus((int)FlightStatusEnum.OnScedule);
            return releasedSeat;
        }
    }
}
