using Space101.DAL;
using Space101.Dtos;
using Space101.Enums;
using Space101.Helper_Models;
using Space101.Hub_Services;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class OrdersController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly OrderRepository orderRepository;
        private readonly UnitOfWork unitOfWork;

        public OrdersController()
        {
            context = new ApplicationDbContext();
            orderRepository = new OrderRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [Route("api/orders/getTickets/{id}")]
        [HttpGet]
        public IHttpActionResult GetOrderTickets(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var ticketsDb = orderRepository.GetTicketsOfOrder(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (!ticketsDb.Any())
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var ticketDtos = new List<TicketDto>();
            ticketsDb.ForEach(ot => ticketDtos.Add(new TicketDto(ot.Ticket)));

            return Ok(ticketDtos);
        }

        [HttpDelete]
        public IHttpActionResult DeleteOrder(int? id)
        {
            if (!User.IsInRole(AvailableRoles.DatabaseManager))
                return BadRequest("You have not access to delete the order.\nPlease contact your Database Manager.");

            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var orderDb = orderRepository.GetFullOrderById(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (orderDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (orderDb.Tickets != null && orderDb.Tickets.Count() > 0)
            {
                var releasedSeats = new List<long>();
                var flight = orderDb.Tickets.FirstOrDefault().Ticket.Flight;

                bool shoulReleaseSeats = flight.FlightStatusId == (int)FlightStatusEnum.Closed || flight.FlightStatusId == (int)FlightStatusEnum.OnScedule;

                foreach (var orderTicket in orderDb.Tickets)
                {
                    if (shoulReleaseSeats)
                        releasedSeats.Add(ReleaseTicketSeat(orderTicket));

                    context.Entry(orderTicket.Ticket).State = EntityState.Deleted;
                }

                if (shoulReleaseSeats)
                    flight.ChangeFlightStatus((int)FlightStatusEnum.OnScedule);

                orderRepository.Remove(orderDb);

                unitOfWork.Complete();

                if (shoulReleaseSeats)
                    InformViewsForReleaseSeats(flight, releasedSeats);
            }
            else
            {
                orderRepository.Remove(orderDb);

                unitOfWork.Complete();
            }

            return Ok();
        }
        private long ReleaseTicketSeat(TicketOrder orderTicket)
        {
            orderTicket.Ticket.Seat.ReleaseSeat();
            context.Entry(orderTicket.Ticket.Seat).State = EntityState.Modified;
            return orderTicket.Ticket.SeatId;
        }
        private void InformViewsForReleaseSeats(Flight flight, IEnumerable<long> releasedSeat)
        {
            var flightService = new FlightHubService();
            flightService.FlightSeatsOpened(releasedSeat.ToArray());
            flightService.FligthStatusChanged(flight.FlightId, (int)FlightStatusEnum.OnScedule);
        }
    }
}
