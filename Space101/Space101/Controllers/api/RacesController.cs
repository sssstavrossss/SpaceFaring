using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using System.Linq;
using System.Net;
using System.Web.Http;
using Space101.DAL;
using AutoMapper;
using Space101.Dtos;
using Space101.Helper_Models;
using Space101.Enums;
using System.Collections.Generic;
using Space101.Services;
using System.Web.Hosting;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin+","+AvailableRoles.DatabaseManager)]
    public class RacesController : ApiController
    {
        private ApplicationDbContext context;
        private readonly RaceRepository raceRepository;
        private readonly TicketRepository ticketRepository;
        private readonly OrderRepository orderRepository;
        private readonly FilePathRepository filePathRepository;
        private readonly FileManager fileManager;
        private readonly UnitOfWork unitOfWork;

        public RacesController()
        {
            context = new ApplicationDbContext();
            raceRepository = new RaceRepository(context);
            ticketRepository = new TicketRepository(context);
            orderRepository = new OrderRepository(context);
            filePathRepository = new FilePathRepository(context);
            fileManager = new FileManager(HostingEnvironment.MapPath("~/App_Assets/"));
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpDelete]
        public IHttpActionResult DeleteRace(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var raceDB = raceRepository.GetRace(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (raceDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var tickets = ticketRepository.GetTicketsByRace(raceDB.RaceID);

            if (tickets.Any())
                return BadRequest("The Race is used in a Ticket!\nTo delete it contact your Database Manager.");

            foreach (var item in raceDB.Assets)
            {
                fileManager.DeleteFile(item);
                filePathRepository.RemoveFilepath(item.FilePath);
            }

            fileManager.DeleteFolders(ModelType.Race, raceDB.RaceID.ToString());

            raceRepository.Remove(raceDB);
            unitOfWork.Complete();

            return Ok();
        }

        [Route("api/races/hardDelete/{id}")]
        [HttpDelete]
        public IHttpActionResult HardDeleteRace(int? id)
        {
            if (!User.IsInRole(AvailableRoles.DatabaseManager))
                return BadRequest("You have not access to delete the race.\nPlease contact your Database Manager.");

            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var raceDB = raceRepository.GetRace(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (raceDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var tickets = ticketRepository.GetTicketsByRace(raceDB.RaceID);
            ClearOrders(tickets);
            ticketRepository.RemoveMultipleTicket(tickets);

            foreach (var item in raceDB.Assets)
            {
                fileManager.DeleteFile(item);
                filePathRepository.RemoveFilepath(item.FilePath);
            }

            fileManager.DeleteFolders(ModelType.Race, raceDB.RaceID.ToString());

            raceRepository.Remove(raceDB);
            unitOfWork.Complete();

            return Ok();
        }
        private void ClearOrders(IEnumerable<Ticket> tickets)
        {
            var ids = tickets.Select(t => t.TicketId);
            var orders = ticketRepository.GetTicketsOrder(ids.ToArray());
            foreach (var order in orders)
            {
                for (var i = 0; i < order.Tickets.Count; i++)
                {
                    if (ids.Contains(order.Tickets.ToList()[i].TicketId))
                        order.Tickets.Remove(order.Tickets.ToList()[i]);
                }
                if (order.Tickets.Count == 0)
                    orderRepository.Remove(order);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetRaces()
        {
            var races = raceRepository.GetRaces();

            var racesDto = races
                .Select(Mapper.Map<Race, RaceDto>).ToList();

            return Ok(racesDto);
        }

    }
}
