using AutoMapper;
using Space101.Dtos;
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
using ModelLibrary.SpaceFaring.Models;

namespace Space101.Controllers.api
{
    [Authorize]
    public class FlightPathsController : ApiController
    {
        private ApplicationDbContext context;
        private PlanetRepository planetRepository;
        private FlightPathRepository flightPathRepository;
        private UnitOfWork unitOfWork;

        public FlightPathsController()
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

        //Get /api/flightpaths/destinations/departureId
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

        //Delete /api/flightpaths/id
        [HttpDelete]
        public IHttpActionResult DeleteFlightPath(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flightPathDb = flightPathRepository.GetSingleFlightPath(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flightPathDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            flightPathRepository.Remove(flightPathDb);

            unitOfWork.Complete();

            return Ok();
        }

    }
}
