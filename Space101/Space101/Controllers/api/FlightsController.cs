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
    public class FlightsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly FlightRepository flightRepository;
        private readonly FlightSeatRepository flightSeatRepository;
        private readonly TicketRepository ticketRepository;
        private readonly OrderRepository orderRepository;
        private readonly UnitOfWork unitOfWork;

        public FlightsController()
        {
            context = new ApplicationDbContext();
            flightRepository = new FlightRepository(context);
            flightSeatRepository = new FlightSeatRepository(context);
            ticketRepository = new TicketRepository(context);
            orderRepository = new OrderRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //Delete api/flights/id")
        [HttpDelete]
        [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
        public IHttpActionResult DeleteFlight(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flightDb = flightRepository.GetSingleFlight(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flightDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var flightTickets = ticketRepository.GetTicketsOfFlight(flightDb.FlightId);

            if (flightTickets.Any())
                return BadRequest("The Flight has purchased Tickets!\nTo delete it contact your Database Manager.");

            flightRepository.Remove(flightDb);

            unitOfWork.Complete();

            var flightService = new FlightHubService();
            flightService.FlightDeleted(id ?? (int)InvalidPropertyValues.undefinedValue);

            return Ok();
        }

        [Route("api/flights/hardDelete/{id}")]
        [HttpDelete]
        public IHttpActionResult HardDeleteFlight(int? id)
        {
            if (!User.IsInRole(AvailableRoles.DatabaseManager))
                return BadRequest("You have not access to delete the flight.\nPlease contact your Database Manager.");

            //The HardDeleteFlight Action will remove every record in database that relates with the flight
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flightDb = flightRepository.GetSingleFlight(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flightDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var flightTickets = ticketRepository.GetTicketsOfFlight(flightDb.FlightId);
            ClearOrders(flightTickets);
            ticketRepository.RemoveMultipleTicket(flightTickets);

            flightRepository.Remove(flightDb);

            unitOfWork.Complete();

            var flightService = new FlightHubService();
            flightService.FlightDeleted(id ?? (int)InvalidPropertyValues.undefinedValue);

            return Ok();
        }
        private void ClearOrders(IEnumerable<Ticket> tickets)
        {
            var ids = tickets.Select(t => t.TicketId);
            var orders = ticketRepository.GetTicketsOrder(ids.ToArray());
            orderRepository.RemoveMultipleOrders(orders);
        }

        [Route("api/flights/flightStatus/{id}/{statusId}")]
        [HttpPost]
        [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
        public IHttpActionResult ChangeStatus(int? id, int? statusId)
        {
            if (!id.HasValue || !statusId.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flightDb = flightRepository.GetSingleFlight(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flightDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            flightDb.ChangeFlightStatus(statusId ?? (int)InvalidPropertyValues.undefinedValue);

            unitOfWork.Complete();

            var flightService = new FlightHubService();
            flightService.FligthStatusChanged(id ?? (int)InvalidPropertyValues.undefinedValue, statusId ?? (int)InvalidPropertyValues.undefinedValue);

            return Ok();
        }

        [Route("api/flights/seats/releaseSeats")]
        [HttpPost]
        public IHttpActionResult ReleaseSeat([FromBody] long[] seatIds)
        {
            if (seatIds == null || seatIds.Length < 1)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var seatsToRelease = flightSeatRepository.GetFlightSeatsById(seatIds);

            if (seatsToRelease == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var flightId = seatsToRelease[0].FlightId;
            var flightTickets = ticketRepository.GetTicketsOfFlight(flightId);

            var closedSeats = flightTickets.Where(t => seatIds.Contains(t.SeatId));

            if (closedSeats.Count() > 0)
                return BadRequest("The Seat cannot be released because a ticket is already purchased!");

            seatsToRelease.ForEach(fs => fs.ReleaseSeat());

            unitOfWork.Complete();

            var flightService = new FlightHubService();
            flightService.FlightSeatsOpened(seatIds);

            return Ok(seatIds);
        }

        [Route("api/flights/seats/holdSeats")]
        [HttpPost]
        public IHttpActionResult HoldSeats([FromBody] long[] seatIds)
        {
            if (seatIds == null || seatIds.Length < 1)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var seatsToRelease = flightSeatRepository.GetFlightSeatsById(seatIds);

            if (seatsToRelease == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            seatsToRelease.ForEach(fs => fs.HoldSeat());

            unitOfWork.Complete();

            var flightService = new FlightHubService();
            flightService.FlightSeatsClosed(seatIds);

            return Ok(seatIds);
        }
    }
}
