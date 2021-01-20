using System.Collections.Generic;
using System.Web.Mvc;
using Space101.Repositories;
using Space101.Persistence;
using Space101.DAL;
using Space101.Models;
using Space101.Services;
using System.Net;
using Space101.ViewModels.RaceViewModels;
using Space101.Helper_Models;

namespace Space101.Controllers
{
    [RoutePrefix("Race")]
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class RaceController : Controller
    {
        private ApplicationDbContext context;
        private readonly PlanetRepository planetRepository;
        private readonly RaceClassificationRepository raceClassificationRepository;
        private readonly RaceRepository raceRepository;
        private readonly UnitOfWork unitOfWork;
        private readonly FilePathRepository filePathRepository;

        public RaceController()
        {
            context = new ApplicationDbContext();
            planetRepository = new PlanetRepository(context);
            raceClassificationRepository = new RaceClassificationRepository(context);
            raceRepository = new RaceRepository(context);
            unitOfWork = new UnitOfWork(context);
            filePathRepository = new FilePathRepository(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: Race
        [AllowAnonymous]
        public ActionResult Index()
        {

            var races = raceRepository.GetRaces();

            var viewModel = new List<RaceViewModel>();

            races.ForEach(r => viewModel.Add(RaceViewModel.RaceViewModelCreation(r)));

            return View(viewModel);
        }

        //Get
        public ActionResult Manipulate(int? id)
        {
            var fileManager = new FileManager(Server);

            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var planets = planetRepository.GetPlanetsRaw();
            var raceClassificationList = raceClassificationRepository.GetRaceClassifications();

            if (id.Value == 0)
            {
                var viewModel = RaceFormViewModel.RaceFormViewModelCreationNew(planets, raceClassificationList, fileManager);

                return View(viewModel);
            }
            else
            {
                var race = raceRepository.GetRace(id.Value);

                var viewModel = RaceFormViewModel.RaceFormViewModelEdit(race, planets, raceClassificationList, fileManager);

                return View(viewModel);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manipulate(RaceFormViewModel viewmodel)
        {

            var fileManager = new FileManager(Server);
            Race race;

            if (!ModelState.IsValid)
            {
                var raceClassificationList = raceClassificationRepository.GetRaceClassifications();
                viewmodel = RaceFormViewModel.RaceFormViewModelValidate(viewmodel, raceClassificationList, fileManager);
                ModelState.AddModelError("NotValidModel", "Failed!");
                return View("Manipulate", viewmodel);
            }

            if (viewmodel.ID == 0)
            {
                race = Race.RaceCreation(viewmodel, fileManager);
                race.InitializeAssets();
                raceRepository.Add(race);
            }
            else
            {
                race = raceRepository.GetRace(viewmodel.ID);
                race.RaceUpdate(viewmodel, fileManager);
                race.UpdateAssets(fileManager, viewmodel);
                unitOfWork.Modify(race);
            }

            unitOfWork.Complete();
            race.SaveAssets(fileManager, viewmodel);

            return RedirectToAction("Index");

        }

    }
}