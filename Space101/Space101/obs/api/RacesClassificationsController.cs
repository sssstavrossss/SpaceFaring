using AutoMapper;
using Space101.Dtos;
using Space101.Models;
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
    public class RacesClassificationsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly RaceClassificationRepository raceClassificationRepository;
        private readonly UnitOfWork unitOfWork;

        public RacesClassificationsController()
        {
            context = new ApplicationDbContext();
            raceClassificationRepository = new RaceClassificationRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // Get Races Classifications api/racesclassifications
        [HttpGet]
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
        public IHttpActionResult DeleteRaceClassification(int id)
        {
            var raceClassificationDB = raceClassificationRepository.GetRaceClassification(id);

            if (raceClassificationDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            raceClassificationRepository.Remove(raceClassificationDB);
            unitOfWork.Complete();

            return Ok();
        }

    }
}
