using AutoMapper;
using Space101.Dtos;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using System.Linq;
using System.Net;
using System.Web.Http;
using Space101.DAL;
using Space101.Enums;
using Space101.Services;
using System.Web.Hosting;
using System.Collections.Generic;
using Space101.Helper_Models;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class RacesClassificationsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly RaceClassificationRepository raceClassificationRepository;
        private readonly RaceRepository raceRepository;
        private readonly TicketRepository ticketRepository;
        private readonly OrderRepository orderRepository;
        private readonly FilePathRepository filePathRepository;
        private readonly FileManager fileManager;
        private readonly UnitOfWork unitOfWork;

        public RacesClassificationsController()
        {
            context = new ApplicationDbContext();
            raceClassificationRepository = new RaceClassificationRepository(context);
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

        // Get Races Classifications api/racesclassifications
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetRacesClassifications() {

            var racesClassificationsDto = raceClassificationRepository
                .GetRaceClassifications()
                .Select(Mapper.Map<RaceClassification, RaceClassificationsDto>);

            return Ok(racesClassificationsDto);
        }

        [HttpPost]
        public IHttpActionResult AddRaceClassification(RaceClassificationsDto raceClassification)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var raceClassificationDB = Mapper.Map<RaceClassificationsDto, RaceClassification>(raceClassification);

            raceClassificationRepository.Add(raceClassificationDB);
            unitOfWork.Complete();

            return Ok(raceClassificationDB.RaceClassificationID);
        }

        [HttpDelete]
        public IHttpActionResult DeleteRaceClassification(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var raceClassificationDB = raceClassificationRepository.GetRaceClassification(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (raceClassificationDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var races = raceRepository.GetRacesForClassifications(raceClassificationDB.RaceClassificationID);

            if (races.Any())
                return BadRequest("The Race Classification is used by a Race!\nTo delete it contact your Database Manager.");

            raceClassificationRepository.Remove(raceClassificationDB);
            unitOfWork.Complete();

            return Ok();
        }

        [Route("api/racesclassifications/hardDelete/{id}")]
        [HttpDelete]
        public IHttpActionResult HardDeleteRaceClassification(int? id)
        {
            if (!User.IsInRole(AvailableRoles.DatabaseManager))
                return BadRequest("You have not access to delete the race classification.\nPlease contact your Database Manager.");

            //The HardDeleteRaceClassification Action will remove every record in database that relates with the race classification
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var raceClassificationDB = raceClassificationRepository.GetRaceClassification(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (raceClassificationDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var races = raceRepository.GetRacesForClassifications(raceClassificationDB.RaceClassificationID);
            var racesIds = races.Select(r => r.RaceID);

            var tickets = ticketRepository.GetTicketsByRace(racesIds.ToArray());
            ClearOrders(tickets);
            ticketRepository.RemoveMultipleTicket(tickets);

            foreach (var race in races)
            {
                ClearAssets(race);
                raceRepository.Remove(race);
            }

            raceClassificationRepository.Remove(raceClassificationDB);
            unitOfWork.Complete();

            return Ok();
        }
        private void ClearAssets(Race race)
        {
            foreach (var asset in race.Assets)
            {
                fileManager.DeleteFile(asset);
                filePathRepository.RemoveFilepath(asset.FilePath);
            }

            fileManager.DeleteFolders(ModelType.Race, race.RaceID.ToString());
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

    }
}
