using Space101.Enums;
using Space101.Persistence;
using Space101.Repositories;
using System.Net;
using System.Web.Http;
using Space101.DAL;
using Space101.Helper_Models;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class TerrainsController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly TerrainRepository terrainRepository;
        private readonly UnitOfWork unitOfWork;

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
        public IHttpActionResult DeleteTerrrain(int? id)
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
