using System.Collections.Generic;
using System.Linq;
using Space101.DAL;
using Space101.Models;

namespace Space101.Repositories
{
    public class StarshipRepository
    {
        private readonly ApplicationDbContext context;

        public StarshipRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Starship> GetStarships()
        {
            return context.Starships.ToList();
        }

        public Starship GetSingleStarship(int id)
        {
            return context.Starships
                .SingleOrDefault(s => s.StarshipId == id);
        }

        public int GetStarshipsCount()
        {
            return context.Starships.Count();
        }

        public void Add(Starship starship)
        {
            context.Starships.Add(starship);
        }

        public void Remove(Starship starship)
        {
            context.Starships.Remove(starship);
        }

    }
}