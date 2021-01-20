using System.Collections.Generic;
using System.Linq;
using Space101.DAL;
using Space101.Models;
using System.Data.Entity;

namespace Space101.Repositories
{
    public class ClimateRepository
    {
        private readonly ApplicationDbContext context;

        public ClimateRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Climate> GetClimates()
        {
            return context.Climates
                .ToList();
        }

        public Climate GetSingleClimate(int id)
        {
            return context.Climates
                .SingleOrDefault(c => c.ClimateId == id);
        }

        public List<Climate> GetClimatesWithPlanets()
        {
            return context.Climates
                .Include(c => c.ClimateZones.Select(cz => cz.Planet))
                .ToList();
        }

        public int GetClimatesCount()
        {
            return context.Climates.Count();
        }

        public void Add(Climate climate)
        {
            context.Climates.Add(climate);
        }

        public void Remove(Climate climate)
        {
            context.Climates.Remove(climate);
        }

    }
}