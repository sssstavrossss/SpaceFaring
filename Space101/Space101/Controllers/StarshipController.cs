using Space101.Persistence;
using Space101.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Space101.DAL;
using Space101.ViewModels.StarShipViewModels;
using Space101.Enums;
using Space101.Models;
using Space101.ViewModels.StarshipViewModels;
using Space101.Helper_Models;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class StarshipController : Controller
    {
        private ApplicationDbContext context;
        private StarshipRepository starshipRepository;
        private UnitOfWork unitOfWork;

        public StarshipController()
        {
            context = new ApplicationDbContext();
            starshipRepository = new StarshipRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var starships = starshipRepository.GetStarships();

            var viewModels = new List<StarshipViewModel>();
            starships.ForEach(s => viewModels.Add(new StarshipViewModel(s)));

            return View(viewModels);
        }

        public ActionResult New()
        {
            var viewModel = new StarshipFormViewModel();

            return View("StarshipForm", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var starship = starshipRepository.GetSingleStarship(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (starship == null)
                return HttpNotFound();

            var viewModel = new StarshipFormViewModel(starship);

            return View("StarshipForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(StarshipFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("StarshipForm", viewModel);
            }

            if(viewModel.Id == 0)
            {
                Starship starship = new Starship(viewModel.Model, viewModel.Manufacturer, viewModel.PassengerCapacity, viewModel.CargoCapacity, viewModel.Length);
                starshipRepository.Add(starship);
            }
            else
            {
                var starshipDb = starshipRepository.GetSingleStarship(viewModel.Id);

                if (starshipDb == null)
                    return HttpNotFound();

                starshipDb.Update(viewModel.Model, viewModel.Manufacturer, viewModel.PassengerCapacity, viewModel.CargoCapacity, viewModel.Length);
            }

            unitOfWork.Complete();

            return RedirectToAction("Index", "Starship");
        }

    }
}