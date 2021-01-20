using ModelLibrary.SpaceFaring.Enums;
using Space101.DAL;
using Space101.Persistence;
using Space101.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Space101.Controllers.api
{
    public class FlightsController : ApiController
    {
        private ApplicationDbContext context;
        private FlightRepository flightRepository;
        private UnitOfWork unitOfWork;

        public FlightsController()
        {
            context = new ApplicationDbContext();
            flightRepository = new FlightRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpDelete]
        public IHttpActionResult DeleteFlight(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var flightDb = flightRepository.GetSingleFlight(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (flightDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            flightRepository.Remove(flightDb);

            unitOfWork.Complete();

            return Ok();
        }
    }
}
