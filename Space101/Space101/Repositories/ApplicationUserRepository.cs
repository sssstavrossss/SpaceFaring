using System.Collections.Generic;
using System.Linq;
using Space101.DAL;
using System.Data.Entity;
using Space101.Models;

namespace Space101.Repositories
{
    public class ApplicationUserRepository
    {
        private readonly ApplicationDbContext context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<ApplicationUser> GetApplicationUsers()
        {
            var applicationUsers = context.Users
                .Include(u => u.Race)
                .Include(u => u.Roles)
                .Include(u => u.Planet)
                .ToList();

            return applicationUsers;
        }
        public ApplicationUser GetApplicationUserById(string id)
        {
            var applicationUser = context.Users.Find(id);

            return applicationUser;
        }
        public long GetApplicationUsersCount()
        {
            return context.Users.Count();
        }

        public void Add(ApplicationUser applicationUser)
        {
            context.Users.Add(applicationUser);
        }
    }
}