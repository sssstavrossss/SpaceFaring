using Space101.Persistence;
using Space101.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Space101.DAL;
using Space101.ViewModels.TerrainViewModels;
using Space101.Enums;
using Space101.Models;
using Space101.Helper_Models;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class TerrainController : Controller
    {
        private ApplicationDbContext context;
        private TerrainRepository terrainRepository;
        private UnitOfWork unitOfWork;

        public TerrainController()
        {
            context = new ApplicationDbContext();
            terrainRepository = new TerrainRepository(context);
            unitOfWork = new UnitOfWork(context);
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var terrains = terrainRepository.GetTerrains();

            var viewModels = new List<TerrainViewModel>();
            terrains.ForEach(t => viewModels.Add(new TerrainViewModel(t)));

            return View(viewModels);
        }

        public ActionResult New()
        {
            var viewModel = new TerrainFormViewModel();

            return View("TerrainForm", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var terrain = terrainRepository.GetSingleTerrain(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (terrain == null)
                return HttpNotFound();

            var viewModel = new TerrainFormViewModel(terrain);

            return View("TerrainForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TerrainFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("TerrainForm", viewModel);
            }

            if(viewModel.Id == 0)
            {
                Terrain terrain = new Terrain(viewModel.Name, viewModel.Color);
                terrainRepository.Add(terrain);
            }
            else
            {
                var terrainDb = terrainRepository.GetSingleTerrain(viewModel.Id);

                if (terrainDb == null)
                    return HttpNotFound();

                terrainDb.Update(viewModel.Name, viewModel.Color);
            }

            unitOfWork.Complete();

            return RedirectToAction("Index","Terrain");
        }
    }
}