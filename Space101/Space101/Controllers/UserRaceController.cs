using Space101.DAL;
using Space101.Repositories;
using Space101.Services;
using Space101.ViewModels.RaceViewModels;
using System.Net;
using System.Web.Mvc;

namespace Space101.Controllers
{
    public class UserRaceController : Controller
    {
        private ApplicationDbContext context;
        private readonly RaceRepository raceRepository;

        public UserRaceController()
        {
            context = new ApplicationDbContext();
            raceRepository = new RaceRepository(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        public ActionResult Races()
        {
            return View("Races");
        }

        public ActionResult Race(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fileManager = new FileManager(Server);

            var race = raceRepository.GetRace(id.Value);

            var viewModel = new UserRaceViewModel(race, fileManager);

            return View("Race", viewModel);
        }
    }
}