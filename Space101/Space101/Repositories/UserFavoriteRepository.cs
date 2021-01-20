using Space101.DAL;
using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Space101.Repositories
{
    public class UserFavoriteRepository
    {
        private readonly ApplicationDbContext context;

        public UserFavoriteRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<UserFavorite> GetFullUserFavorites(string id)
        {
            return context.UserFavorites
                .Include(uf => uf.ApplicationUser)
                .Include(uf => uf.Flight.FlightPath.Departure)
                .Include(uf => uf.Flight.FlightPath.Destination)
                .Include(uf => uf.Flight.FlightStatus)
                .Include(uf => uf.Flight.Starship)
                .Where(uf => uf.ApplicationUserID == id)
                .ToList();
        }

        public List<UserFavorite> GetSimpleUserFavorites(string id)
        {
            return context.UserFavorites
                .Where(uf => uf.ApplicationUserID == id)
                .ToList();
        }

        public UserFavorite GetSimpleUserFavorite(int id)
        {
            return context.UserFavorites
                .SingleOrDefault(uf => uf.UserFavoriteID == id);
        }

        public void Add(UserFavorite favorite)
        {
            context.UserFavorites.Add(favorite);
        }

        public void Remove(UserFavorite favorite)
        {
            context.UserFavorites.Remove(favorite);
        }
    }
}