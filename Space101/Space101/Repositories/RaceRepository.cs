using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Space101.DAL;
using Space101.Models;

namespace Space101.Repositories
{
    public class RaceRepository
    {
        private readonly ApplicationDbContext context;

        public RaceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Race> GetRaces()
        {
            var races = context.Races
                .Include(r => r.RaceHabitats.Select(rh => rh.Planet))
                .Include(r => r.RaceClassification)
                .Include(r => r.Assets.Select(a => a.FilePath))
                .ToList();

            return races;
        }

        public Race GetRace(int id)
        {
            var race = context.Races
                .Include(r => r.RaceHabitats.Select(rh => rh.Planet))
                .Include(r => r.RaceClassification)
                .Include(r => r.Assets.Select(a => a.FilePath))
                .SingleOrDefault(r => r.RaceID == id);

            return race;
        }

        public List<Race> GetRacesForClassifications(params int[] raceClassificationIds)
        {
            return context.Races
                .Where(r => raceClassificationIds.Contains(r.RaceClassificationID))
                .ToList();
        }

        public int GetRacesCount()
        {
            return context.Races.Count();
        }

        public void Add(Race race)
        {
            context.Races.Add(race);
        }

        public void Remove(Race race)
        {
            context.Races.Remove(race);
        }
    }
}