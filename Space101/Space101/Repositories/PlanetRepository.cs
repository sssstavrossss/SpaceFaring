using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Space101.DAL;
using Space101.Models;

namespace Space101.Repositories
{
    public class PlanetRepository
    {
        private readonly ApplicationDbContext context;

        public PlanetRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Planet> GetPlanetsRaw()
        {
            var planets = context.Planets
                .ToList();

            return planets;
        }

        public List<Planet> GetActivePlanetsRaw()
        {
            var planets = context.Planets
                .Where(p => p.IsActive)
                .ToList();

            return planets;
        }

        public List<Planet> GetAllFullDataPlanets()
        {
            return context.Planets
                .Include(p => p.RaceHabitats.Select(rh => rh.Race))
                .Include(p => p.Details)
                .Include(p => p.SurfaceMorphologies.Select(sm => sm.Terrain))
                .Include(p => p.ClimateZones.Select(cz => cz.Climate))
                .Include(p => p.Assets.Select(a => a.FilePath))
                .ToList();
        }

        public List<Planet> GetPlanetsByIdRaw(params int[] ids)
        {
            var planets = context.Planets
                .Where(p => ids.Contains(p.PlanetID))
                .ToList();

            return planets;
        }

        public List<Planet> GetActivePlanetsByIdRaw(params int[] ids)
        {
            var planets = context.Planets
                .Where(p => ids.Contains(p.PlanetID) && p.IsActive)
                .ToList();

            return planets;
        }

        public List<Planet> GetPlanetsWithDestinations()
        {
            return context.Planets
                .Include(p => p.Destinations)
                .ToList();
        }

        public List<Planet> GetActivePlanetsWithDestinations()
        {
            return context.Planets
                .Include(p => p.Destinations)
                .Where(p => p.IsActive)
                .ToList();
        }

        public Planet GetPlanetWithDetails(int id)
        {
            return context.Planets
                .Include(p => p.Details)
                .SingleOrDefault(p => p.PlanetID == id);
        }

        public Planet GetActivePlanetWithDetails(int id)
        {
            return context.Planets
                .Include(p => p.Details)
                .SingleOrDefault(p => p.PlanetID == id && p.IsActive);
        }

        public Planet GetFullDataPlanet(int id)
        {
            return context.Planets
                .Include(p => p.Details)
                .Include(p => p.SurfaceMorphologies.Select(sm => sm.Terrain))
                .Include(p => p.ClimateZones.Select(cz => cz.Climate))
                .Include(p => p.Assets.Select(a => a.FilePath))
                .Include(p => p.RaceHabitats.Select(rh => rh.Race))
                .SingleOrDefault(p => p.PlanetID == id);
        }

        public Planet GetFullDataActivePlanet(int id)
        {
            return context.Planets
                .Include(p => p.Details)
                .Include(p => p.SurfaceMorphologies)
                .Include(p => p.ClimateZones)
                .Include(p => p.Assets.Select(a => a.FilePath))
                .SingleOrDefault(p => p.PlanetID == id && p.IsActive);
        }

        public Planet GetPlanetRawWithAssets(int id)
        {
            return context.Planets
                .Include(p => p.Assets.Select(a => a.FilePath))
                .SingleOrDefault(p => p.PlanetID == id);
        }

        public Planet GetActivePlanetRawWithAssets(int id)
        {
            return context.Planets
                .Include(p => p.Assets.Select(a => a.FilePath))
                .SingleOrDefault(p => p.PlanetID == id && p.IsActive);
        }

        public int GetPlanetsCount()
        {
            return context.Planets.Count();
        }

        public void Add(Planet planet)
        {
            context.Planets.Add(planet);
        }

        public void Remove(Planet planet)
        {
            context.Planets.Remove(planet);
        }

    }
}