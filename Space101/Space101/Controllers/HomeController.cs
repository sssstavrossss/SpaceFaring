using Space101.Helper_Models;
using System.Web.Mvc;


namespace Space101.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        { }

        public ActionResult Index()
        {
            if (User.IsInRole(AvailableRoles.Admin) || User.IsInRole(AvailableRoles.DatabaseManager))
            {
                return RedirectToAction("Index", "DatabaseStatistic");
            }

            return View();
        }

        public ActionResult Intro()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}