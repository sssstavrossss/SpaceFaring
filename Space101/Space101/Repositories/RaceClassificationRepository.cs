using System.Collections.Generic;
using System.Linq;
using Space101.DAL;
using Space101.Models;

namespace Space101.Repositories
{
    public class RaceClassificationRepository
    {
        private readonly ApplicationDbContext context;

        public RaceClassificationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<RaceClassification> GetRaceClassifications()
        {
            var raceClassificationList = context.RaceClassifications.ToList();

            return raceClassificationList;
        }

        public RaceClassification GetRaceClassification(int id)
        {
            var raceClassification = context.RaceClassifications.SingleOrDefault(rc => rc.RaceClassificationID == id);

            return raceClassification;
        }

        public int GetRaceClassificationCount()
        {
            return context.RaceClassifications.Count();
        }

        public void Add(RaceClassification raceClassification)
        {
            context.RaceClassifications.Add(raceClassification);
        }

        public void Remove(RaceClassification raceClassification)
        {
            context.RaceClassifications.Remove(raceClassification);
        }
    }
}