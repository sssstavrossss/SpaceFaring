
using Space101.Persistence;
using Space101.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Space101.DAL;
using ModelLibrary.SpaceFaring.Enums;

namespace Space101.Controllers.api
{
    [Authorize]
    public class ClimatesController : ApiController
    {
        private ApplicationDbContext context;
        private ClimateRepository climateRepository;
        private UnitOfWork unitOfWork;

        public ClimatesController()
        {
            context = new ApplicationDbContext();
            climateRepository = new ClimateRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //Delete /api/climates/id
        [HttpDelete]
        public IHttpActionResult DeleteClimate(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var climateDb = climateRepository.GetSingleClimate(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (climateDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            climateRepository.Remove(climateDb);

            unitOfWork.Complete();

            return Ok();
        }
    }
}
