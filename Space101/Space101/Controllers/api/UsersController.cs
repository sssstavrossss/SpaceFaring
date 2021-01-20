using Newtonsoft.Json.Linq;
using Space101.DAL;
using Space101.Persistence;
using Space101.Repositories;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Space101.Models;
using Space101.Helper_Models;

namespace Space101.Controllers.api
{
    [Authorize(Roles = AvailableRoles.Admin + "," + AvailableRoles.DatabaseManager)]
    public class UsersController : ApiController
    {

        private ApplicationDbContext context;
        private readonly ApplicationUserRepository applicationUserRepository;
        private readonly ApplicationRoleRepository applicationRoleRepository;
        private readonly RaceRepository raceRepository;
        private readonly UnitOfWork unitOfWork;
        private const string NoRole = "Select Role";
        private UserStore<ApplicationUser> userStore;
        private UserManager<ApplicationUser> userManager;
        //private System.Web.Security.SqlRoleProvider sqlManager;

        public UsersController()
        {
            //sqlManager = new System.Web.Security.SqlRoleProvider();
            context = new ApplicationDbContext();
            applicationUserRepository = new ApplicationUserRepository(context);
            applicationRoleRepository = new ApplicationRoleRepository(context);
            raceRepository = new RaceRepository(context);
            unitOfWork = new UnitOfWork(context);
            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult ChangeRole([FromBody] JObject data)
        {
            var userId = data["userId"].ToString();
            var roleId = data["roleId"].ToString();

            var user = applicationUserRepository.GetApplicationUserById(userId);
            var role = applicationRoleRepository.GetApplicationRoleById(roleId);
            var roleNames = userManager.GetRoles(user.Id).ToArray();

            if (roleNames.Count() > 0)
            {
                userManager.RemoveFromRoles(user.Id, roleNames);
            }
            
            userManager.AddToRole(user.Id, role.Name);
            unitOfWork.Complete();

            return Ok(role.Name);
        }

    }

}
