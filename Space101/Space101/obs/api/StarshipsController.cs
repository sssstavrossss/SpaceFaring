
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
    public class StarshipsController : ApiController
    {
        private ApplicationDbContext context;
        private StarshipRepository starshipRepository;
        private UnitOfWork unitOfWork;

        public StarshipsController()
        {
            context = new ApplicationDbContext();
            starshipRepository = new StarshipRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //Delete /api/starships/id
        [HttpDelete]
        public IHttpActionResult DeleteStarship(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var starshipDb = starshipRepository.GetSingleStarship(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (starshipDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            starshipRepository.Remove(starshipDb);

            unitOfWork.Complete();

            return Ok();
        }

    }
}
