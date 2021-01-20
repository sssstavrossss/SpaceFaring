using System.Web.Mvc;
using Space101.DAL;
using Space101.Helper_Models;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class RaceClassificationController : Controller
    {
        private ApplicationDbContext context;

        public RaceClassificationController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: RaceClassification
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

    }
}