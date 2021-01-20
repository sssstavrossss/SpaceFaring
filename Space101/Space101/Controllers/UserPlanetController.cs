using Space101.DAL;
using Space101.Persistence;
using Space101.Repositories;
using Space101.Services;
using Space101.ViewModels.PlanetUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Space101.Controllers
{
    public class UserPlanetController : Controller
    {

        private ApplicationDbContext context;
        private readonly PlanetRepository planetRepository;
        private readonly FlightPathRepository flightPathRepository;

        public UserPlanetController()
        {
            context = new ApplicationDbContext();
            planetRepository = new PlanetRepository(context);
            flightPathRepository = new FlightPathRepository(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        public ActionResult Planets()
        {
            //var planets = planetRepository.GetAllFullDataPlanets();

            return View("Planets");
        }

        public ActionResult Planet(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fileManager = new FileManager(Server);

            var planet = planetRepository.GetFullDataPlanet(id.Value);
            var flightPathsDestination = flightPathRepository.GetFlightPathsByIdforDestination(id.Value);
            var flightPathsDeparture = flightPathRepository.GetFlightPathsByIdforDeparture(id.Value);

            var viewModel = new PlanetUserViewModel(planet, fileManager, flightPathsDestination, flightPathsDeparture);

            return View("Planet", viewModel);
        }
    }
}