using Space101.Models;
using Space101.ViewModels.ClimateZoneViewModels;
using Space101.ViewModels.SurfaceMorphologyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.PlanetViewModels
{
    public class ContainerPlanetFormViewModel
    {
        public PlanetFormViewModel PlanetFormViewModel { get; set; }
        public PlanetDetailFormViewModel PlanetDetailFormViewModel { get; set; }
        public IEnumerable<ClimateZoneViewModel> Climates { get; set; }
        public IEnumerable<SurfaceMorphologyViewModel> Terrains { get; set; }

        private ContainerPlanetFormViewModel(IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            PlanetFormViewModel = new PlanetFormViewModel();
            PlanetDetailFormViewModel = new PlanetDetailFormViewModel();
            Climates = climates;
            Terrains = terrains;
        }

        private ContainerPlanetFormViewModel(Planet planet, PlanetDetail planetDetail, IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            PlanetFormViewModel = new PlanetFormViewModel(planet);
            PlanetDetailFormViewModel = new PlanetDetailFormViewModel(planetDetail);
            Climates = climates;
            Terrains = terrains;
        }

        private ContainerPlanetFormViewModel(PlanetFormViewModel planetFormViewModel, PlanetDetailFormViewModel planetDetailFormViewModel, IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            PlanetFormViewModel = planetFormViewModel;
            PlanetDetailFormViewModel = planetDetailFormViewModel;
            Climates = climates;
            Terrains = terrains;
        }

        public static ContainerPlanetFormViewModel CreateEmpty(IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            return new ContainerPlanetFormViewModel(climates, terrains);
        }

        public static ContainerPlanetFormViewModel CreateFromModels(Planet planet, PlanetDetail details, IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            return new ContainerPlanetFormViewModel(planet, details, climates, terrains);
        }

        public static ContainerPlanetFormViewModel CreateFromViewModels(PlanetFormViewModel planetFormViewModel, PlanetDetailFormViewModel detailFormViewModel, IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            return new ContainerPlanetFormViewModel(planetFormViewModel, detailFormViewModel, climates, terrains);
        }

    }
}