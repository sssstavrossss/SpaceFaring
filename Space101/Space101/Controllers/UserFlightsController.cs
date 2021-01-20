using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Space101.Controllers
{
    public class UserFlightsController : Controller
    {
        // GET: UserFlights
        public ActionResult Flights()
        {
            return View("Flights");
        }
    }
}