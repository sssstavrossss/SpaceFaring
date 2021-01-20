using Space101.DAL;
using Space101.Persistence;
using Space101.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Space101.Controllers.api
{
    public class ApplicationRolesController : ApiController
    {
        private ApplicationDbContext context;
        private readonly ApplicationRoleRepository applicationRoleRepository;
        private readonly UnitOfWork unitOfWork;

        public ApplicationRolesController()
        {
            context = new ApplicationDbContext();
            applicationRoleRepository = new ApplicationRoleRepository(context);
            unitOfWork = new UnitOfWork(context);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        [HttpPost]
        public IHttpActionResult ToggleActivity(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var applicationRole = applicationRoleRepository.GetApplicationRoleById(id);
            applicationRole.ToggleActivity();
            unitOfWork.Complete();

            return Ok(applicationRole.Active);
        }
    }
}
