using Space101.DAL;
using Space101.Persistence;
using Space101.Repositories;
using System.Web.Mvc;
using Space101.ViewModels.ApplicationRoleViewModels;
using Space101.Models;
using Space101.Helper_Models;

namespace Space101.Controllers
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class ApplicationRoleController : Controller
    {
        private ApplicationDbContext context;
        private readonly ApplicationRoleRepository applicationRoleRepository;
        private readonly UnitOfWork unitOfWork;

        public ApplicationRoleController()
        {
            context = new ApplicationDbContext();
            applicationRoleRepository = new ApplicationRoleRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: ApplicationRole
        public ActionResult Index()
        {
            var applicationRoles = applicationRoleRepository.GetApplicationRoles();

            var viewModel = new ApplicationRoleContainerViewModel(applicationRoles);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ApplicationRole applicationRole)
        {
            if (string.IsNullOrEmpty(applicationRole.Name))
            {
                ModelState.AddModelError("Errors", "Name is required, up to 128 characters!"); 
                
                var applicationRoles = applicationRoleRepository.GetApplicationRoles();

                var viewModel = new ApplicationRoleContainerViewModel(applicationRoles);

                return View("Index", viewModel);
            }

            applicationRole.MakeActive();
            applicationRoleRepository.Add(applicationRole);
            unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}