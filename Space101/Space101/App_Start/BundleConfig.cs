using System.Web;
using System.Web.Optimization;

namespace Space101
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/databaseStatistics").Include(
                       "~/Scripts/app/services/databaseStatisticsService.js",
                       "~/Scripts/app/controllers/databaseStatisticsController.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                      "~/Scripts/jquery.signalR-2.2.2.min.js",
                      "~/signalr/hubs"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/pagination").Include(
                        "~/Scripts/pagination/pagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/userFrontPage").Include(
                        "~/Scripts/app/services/userFrontPageService.js",
                        "~/Scripts/app/controllers/userFrontPageController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/usersFlights").Include(
                        "~/Scripts/app/services/usersFlightsService.js",
                        "~/Scripts/app/singalR/userFlightsSignalR.js",
                        "~/Scripts/app/controllers/usersFlightsController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/userFavorites").Include(
                        "~/Scripts/app/services/userFavoriteService.js",
                        "~/Scripts/app/controllers/userFavoriteController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/planetsUsers").Include(
                        "~/Scripts/app/services/planetsUsersService.js",
                        "~/Scripts/app/controllers/planetsUsersController.js",
                        "~/Scripts/app/app.js")); 

                bundles.Add(new ScriptBundle("~/bundles/racesUsers").Include(
                        "~/Scripts/app/services/racesUsersService.js",
                        "~/Scripts/app/controllers/racesUsersController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                        "~/Scripts/app/navbar.js"));

            bundles.Add(new ScriptBundle("~/bundles/printOrderData").Include(
                        "~/Scripts/app/printOrderData.js"));

            bundles.Add(new ScriptBundle("~/bundles/userPlanet").Include(
                       "~/Scripts/app/UserPlanet.js"));

            bundles.Add(new ScriptBundle("~/bundles/userRace").Include(
                       "~/Scripts/app/UserRace.js"));

            bundles.Add(new ScriptBundle("~/bundles/ticket").Include(
                       "~/Scripts/app/services/ticketService.js",
                       "~/Scripts/app/controllers/ticketController.js",
                       "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/order").Include(
                        "~/Scripts/app/services/orderService.js",
                        "~/Scripts/app/controllers/orderController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/user").Include(
                        "~/Scripts/app/services/applicationUserService.js",
                        "~/Scripts/app/controllers/applicationUserController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/flight").Include(
                        "~/Scripts/app/services/flightService.js",
                        "~/Scripts/app/controllers/flightController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/flightPath").Include(
                        "~/Scripts/app/services/flightPathService.js",
                        "~/Scripts/app/controllers/flightPathController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/planet").Include(
                        "~/Scripts/app/services/planetService.js",
                        "~/Scripts/app/controllers/planetController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/applicationrole").Include(
                        "~/Scripts/app/services/applicationRoleService.js",
                        "~/Scripts/app/controllers/applicationRoleController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/starship").Include(
                        "~/Scripts/app/services/starshipService.js",
                        "~/Scripts/app/controllers/starshipController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/terrain").Include(
                        "~/Scripts/app/services/terrainService.js",
                        "~/Scripts/app/controllers/terrainController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/climate").Include(
                        "~/Scripts/app/services/climateService.js",
                        "~/Scripts/app/controllers/climateController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/race").Include(
                        "~/Scripts/app/services/raceService.js",
                        "~/Scripts/app/controllers/raceController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/raceclassification").Include(
                        "~/Scripts/app/services/raceClassificationsService.js",
                        "~/Scripts/app/controllers/raceClassificationsController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/bootbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.globalize/globalize.js",
                        $"~/Scripts/jquery.globalize/cultures/globalize.culture.{System.Threading.Thread.CurrentThread.CurrentCulture.Name}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalCustomLocals").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.globalize/globalize.js",
                        $"~/Scripts/jquery.globalize/cultures/globalize.culture.{System.Threading.Thread.CurrentThread.CurrentCulture.Name}.js",
                        "~/Scripts/app/services/localCultureService.js",
                        "~/Scripts/app/controllers/localCultureController.js",
                        "~/Scripts/app/customValidations/globalizeDateTime.js",
                        "~/Scripts/app/customValidations/globalizeNumber.js",
                        "~/Scripts/app/customValidations/customDateTimePick.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/customVal").Include(
                        "~/Scripts/app/customValidations/globalizeDateTime.js",
                        "~/Scripts/app/customValidations/globalizeNumber.js",
                        "~/Scripts/app/customValidations/customDateTimePick.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js",
                      "~/Scripts/bootbox.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/particle").Include(
                      "~/Scripts/particlejs/jsdelivr.js",
                      "~/Scripts/particlejs/threejs.js",
                      "~/Scripts/particlejs/particlejs.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/AdminContent/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/DataTables/css/dataTables.bootstrap.css",
                     "~/Content/bootstrap-datetimepicker.css",
                     "~/Content/toastr.css",
                      "~/Content/AdminSite.css"));

            //Try Datetime Picker

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                       "~/Scripts/moment-with-locales.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimePicker").Include(
                      "~/Scripts/bootstrap-datetimepicker.js"));

        }
    }
}