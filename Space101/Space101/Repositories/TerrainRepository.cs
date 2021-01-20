using System.Collections.Generic;
using System.Linq;
using Space101.DAL;
using Space101.Models;
using System.Data.Entity;

namespace Space101.Repositories
{
    public class TerrainRepository
    {
        private readonly ApplicationDbContext context;

        public TerrainRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Terrain> GetTerrains()
        {
            return context.Terrains
                .ToList();
        }

        public Terrain GetSingleTerrain(int id)
        {
            return context.Terrains
                .SingleOrDefault(t => t.TerrainId == id);
        }

        public List<Terrain> GetTerrainsWithPlanets()
        {
            return context.Terrains
                .Include(t => t.SurfaceMorphologies.Select(cz => cz.Planet))
                .ToList();
        }

        public int GetTerrainsCount()
        {
            return context.Terrains.Count();
        }

        public void Add(Terrain terrain)
        {
            context.Terrains.Add(terrain);
        }

        public void Remove(Terrain terrain)
        {
            context.Terrains.Remove(terrain);
        }
    }
}