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
    public class ClimatesController : ApiController
    {
        private readonly ApplicationDbContext context;
        private readonly ClimateRepository climateRepository;
        private readonly UnitOfWork unitOfWork;

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
