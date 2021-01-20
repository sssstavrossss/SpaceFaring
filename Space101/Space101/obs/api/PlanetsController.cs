
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Space101.Repositories;
using Space101.Persistence;
using Space101.DAL;
using ModelLibrary.SpaceFaring.Enums;

namespace Space101.Controllers.api
{
    [Authorize]
    public class PlanetsController : ApiController
    {
        private ApplicationDbContext context;
        private PlanetRepository planetRepository;
        private FlightPathRepository flightPathRepository;
        private UnitOfWork unitOfWork;

        public PlanetsController()
        {
            context = new ApplicationDbContext();
            planetRepository = new PlanetRepository(context);
            flightPathRepository = new FlightPathRepository(context);
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

            //No cascade delete in flightpaths
            var flightPathsToDelete = flightPathRepository.GetPlanetFlightPaths(id ?? (int)InvalidPropertyValues.undefinedValue);
            var planetDB = planetRepository.GetFullDataPlanet(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (planetDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            flightPathRepository.RemoveRange(flightPathsToDelete);
            planetRepository.Remove(planetDB);

            unitOfWork.Complete();

            return Ok();
        }
    }
}
