using Space101.Enums;
using Space101.Models;
using Space101.Persistence;
using Space101.Repositories;
using Space101.ViewModels.ClimateViewModels;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Space101.DAL;
using Space101.Helper_Models;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class ClimateController : Controller
    {
        private ApplicationDbContext context;
        private readonly ClimateRepository climateRepository;
        private readonly UnitOfWork unitOfWork;

        public ClimateController()
        {
            context = new ApplicationDbContext();
            climateRepository = new ClimateRepository(context);
            unitOfWork = new UnitOfWork(context);
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var climates = climateRepository.GetClimates();

            var viewModels = new List<ClimateViewModel>();
            climates.ForEach(c => viewModels.Add(new ClimateViewModel(c)));

            return View(viewModels);
        }

        public ActionResult New()
        {
            var viewModel = new ClimateFormViewModel();

            return View("ClimateForm", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var climate = climateRepository.GetSingleClimate(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (climate == null)
                return HttpNotFound();

            var viewModel = new ClimateFormViewModel(climate);

            return View("ClimateForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ClimateFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ClimateForm", viewModel);
            }

            if(viewModel.Id == 0)
            {
                Climate climate = new Climate(viewModel.Name, viewModel.Color);
                climateRepository.Add(climate);
            }
            else
            {
                var climateDb = climateRepository.GetSingleClimate(viewModel.Id);

                if (climateDb == null)
                    return HttpNotFound();

                climateDb.Update(viewModel.Name, viewModel.Color);
            }

            unitOfWork.Complete();

            return RedirectToAction("Index", "Climate");
        }
    }
}