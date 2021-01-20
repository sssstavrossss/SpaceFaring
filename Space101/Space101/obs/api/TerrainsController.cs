
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
    public class TerrainsController : ApiController
    {
        private ApplicationDbContext context;
        private TerrainRepository terrainRepository;
        private UnitOfWork unitOfWork;

        public TerrainsController()
        {
            context = new ApplicationDbContext();
            terrainRepository = new TerrainRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        //Delete /api/terrains/id
        [HttpDelete]
        public IHttpActionResult DeleteClimate(int? id)
        {
            if (!id.HasValue)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var terrainDb = terrainRepository.GetSingleTerrain(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (terrainDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            terrainRepository.Remove(terrainDb);

            unitOfWork.Complete();

            return Ok();
        }


    }
}
