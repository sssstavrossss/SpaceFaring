using Space101.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Space101.Models;

namespace Space101.Repositories
{
    public class ApplicationRoleRepository
    {
        private readonly ApplicationDbContext context;

        public ApplicationRoleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<ApplicationRole> GetApplicationRoles()
        {
            var list = context.ApplicationRoles
                .Include(ar => ar.Users)
                .ToList();

            return list;
        }

        public List<ApplicationRole> GetApplicationRolesActive()
        {
            var list = context.ApplicationRoles
                .Where(a => a.Active == true)
                .Include(ar => ar.Users)
                .ToList();

            return list;
        }

        public ApplicationRole GetApplicationRoleById(string id)
        {
            var applicationRole = context.ApplicationRoles
                .Include(ar => ar.Users)
                .SingleOrDefault(ar => ar.Id == id);

            return applicationRole;
        }

        public ApplicationRole GetApplicationRoleByUserId(string id)
        {
            var applicationRole = context.ApplicationRoles
                .Include(ar => ar.Users.SingleOrDefault(u => u.UserId == id))
                .Single();

            return applicationRole;
        }

        public String[] GetRoleNames()
        {
            var roles = context.ApplicationRoles.ToList().Select(ar => ar.Name).ToArray();

            return roles;
        }

        public void Add(ApplicationRole applicationRole)
        {
            context.ApplicationRoles.Add(applicationRole);
        }
    }
}