using Space101.Models;
using Space101.ViewModels.PlanetViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using Space101.Repositories;
using Space101.Persistence;
using Space101.Helper_Models;
using Space101.DAL;
using Space101.ViewModels.ClimateZoneViewModels;
using Space101.ViewModels.SurfaceMorphologyViewModels;
using Space101.Services;
using Space101.Enums;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class PlanetController : Controller
    {
        private ApplicationDbContext context;
        private readonly PlanetRepository planetRepository;
        private readonly ClimateRepository climateRepository;
        private readonly TerrainRepository terrainRepository;
        private readonly FilePathRepository filePathRepository;
        private readonly UnitOfWork unitOfWork;

        public PlanetController()
        {
            context = new ApplicationDbContext();
            planetRepository = new PlanetRepository(context);
            climateRepository = new ClimateRepository(context);
            terrainRepository = new TerrainRepository(context);
            filePathRepository = new FilePathRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        public ActionResult Index()
        {
            var planets = planetRepository.GetPlanetsRaw();
            var fullplanetsViewModels = new List<DisplayPlanetViewModel>();

            planets.ForEach(p => fullplanetsViewModels.Add(new DisplayPlanetViewModel(p)));

            return View(fullplanetsViewModels);
        }

        public ActionResult Details(int? id)
        {
            var fileManager = new FileManager(Server);
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fullPlanet = planetRepository.GetFullDataPlanet(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (fullPlanet == null)
                return HttpNotFound();

            var viewModel = new DisplayFullPlanetViewModel(fullPlanet, fileManager);

            return View(viewModel);
        }

        private IEnumerable<ClimateZoneViewModel> PopulateClimates(IEnumerable<Climate> allClimates, IEnumerable<ClimateZone> planetClimates)
        {
            var viewModels = new List<ClimateZoneViewModel>();

            if(planetClimates != null)
            {
                var planetClimatesHS = new HashSet<int>(planetClimates.Select(cz => cz.ClimateId));
                allClimates.ToList().ForEach(c => viewModels.Add(new ClimateZoneViewModel(c.ClimateId, c.Name, planetClimatesHS.Contains(c.ClimateId))));
            }
            else
            {
                allClimates.ToList().ForEach(c => viewModels.Add(new ClimateZoneViewModel(c.ClimateId, c.Name, false)));
            }

            return viewModels;
        }

        private IEnumerable<SurfaceMorphologyViewModel> PopulateTerrains(IEnumerable<Terrain> allTerrains, IEnumerable<SurfaceMorphology> planetTerrains)
        {
            var viewModels = new List<SurfaceMorphologyViewModel>();

            if(planetTerrains != null)
            {
                var planetTerrainsHS = new HashSet<int>(planetTerrains.Select(cz => cz.TerrainId));
                allTerrains.ToList().ForEach(t => viewModels.Add(new SurfaceMorphologyViewModel(t.TerrainId, t.Name, planetTerrainsHS.Contains(t.TerrainId))));
            }
            else
            {
                allTerrains.ToList().ForEach(t => viewModels.Add(new SurfaceMorphologyViewModel(t.TerrainId, t.Name, false)));
            }
            
            return viewModels;
        }

        public ActionResult New()
        {
            var climates = climateRepository.GetClimates();
            var terrains = terrainRepository.GetTerrains();

            var climateViewModels = PopulateClimates(climates, null);
            var terrainViewModels = PopulateTerrains(terrains, null);

            var viewModel = ContainerPlanetFormViewModel.CreateEmpty(climateViewModels, terrainViewModels);

            return View("PlanetForm", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            var fileManager = new FileManager(Server);
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fullPlanet = planetRepository.GetFullDataPlanet(id ?? (int)InvalidPropertyValues.undefinedValue);

            if (fullPlanet == null)
                return HttpNotFound();

            var climates = climateRepository.GetClimates();
            var terrains = terrainRepository.GetTerrains();

            var planetClimates = fullPlanet.ClimateZones.ToList();
            var planetTerrains = fullPlanet.SurfaceMorphologies.ToList();

            var climateViewModels = PopulateClimates(climates, planetClimates);
            var terrainViewModels = PopulateTerrains(terrains, planetTerrains);

            var viewModel = ContainerPlanetFormViewModel.CreateFromModels(fullPlanet, fullPlanet.Details, fileManager, climateViewModels, terrainViewModels);

            return View("PlanetForm", viewModel);
        }


        //POST: Planet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PlanetFormViewModel planetViewModel, PlanetDetailFormViewModel detailsViewModel, PlanetAssetsFormViewModel planetAssetsFormViewModel, List<ClimateZoneViewModel> climateZonesViewModel, List<SurfaceMorphologyViewModel> surfaceMorphologiesViewModel)
        {
            var fileManager = new FileManager(Server);
            if (!ModelState.IsValid)
            {
                Planet planetWithAssets = planetRepository.GetPlanetRawWithAssets(planetViewModel.PlanetFormViewModelId);
                planetViewModel.AssignUploadedAvatar(fileManager);
                if(planetWithAssets != null)
                    planetAssetsFormViewModel = new PlanetAssetsFormViewModel(planetWithAssets, fileManager);
                var viewModel = ContainerPlanetFormViewModel.CreateFromViewModels(planetViewModel,detailsViewModel, planetAssetsFormViewModel, climateZonesViewModel, surfaceMorphologiesViewModel);
                return View("PlanetForm", viewModel);
            }

            Planet planetToDB;

            if (planetViewModel.PlanetFormViewModelId == 0)
            {
                planetToDB = Planet.CreateFromFormViewModel(planetViewModel,fileManager);
                var planetDetails = PlanetDetail.CreateFromFormViewModel(detailsViewModel,planetViewModel);
                planetToDB.UpdateSecondaryData(planetDetails, climateZonesViewModel, surfaceMorphologiesViewModel);
                planetToDB.InitializeAssets(fileManager, planetAssetsFormViewModel);
                planetRepository.Add(planetToDB);
            }
            else
            {
                planetToDB = planetRepository.GetFullDataPlanet(planetViewModel.PlanetFormViewModelId);

                if (planetToDB == null)
                    return HttpNotFound();

                planetToDB.UpdateFromFormViewModel(planetViewModel,fileManager);
                var planetDetails = PlanetDetail.Create(detailsViewModel.RotationPeriod, detailsViewModel.OrbitalPeriod, detailsViewModel.Diameter, detailsViewModel.Population, planetToDB);
                planetToDB.UpdateSecondaryData(planetDetails, climateZonesViewModel, surfaceMorphologiesViewModel);
                planetToDB.UpdateAssets(fileManager, planetAssetsFormViewModel);
                filePathRepository.RemoveFilepaths(planetToDB.FilePathsToDelete);
            }

            unitOfWork.Complete();

            planetToDB.SaveAssets(fileManager, planetAssetsFormViewModel);

            return RedirectToAction("Index","Planet");
        }
        
    }
}