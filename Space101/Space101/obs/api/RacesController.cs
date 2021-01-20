
using Space101.Persistence;
using Space101.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Space101.DAL;

namespace Space101.Controllers.api
{
    public class RacesController : ApiController
    {
        private ApplicationDbContext context;
        private readonly RaceRepository raceRepository;
        private readonly UnitOfWork unitOfWork;

        public RacesController()
        {
            context = new ApplicationDbContext();
            raceRepository = new RaceRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpDelete]
        public IHttpActionResult DeleteRaceClassification(int id)
        {
            var raceDB = raceRepository.GetRace(id);

            if (raceDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            raceRepository.Remove(raceDB);
            unitOfWork.Complete();

            return Ok();
        }

    }
}
