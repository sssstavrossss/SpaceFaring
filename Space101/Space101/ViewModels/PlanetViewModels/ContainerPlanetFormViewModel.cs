using Space101.Controllers;
using Space101.Models;
using Space101.Services;
using Space101.ViewModels.ClimateZoneViewModels;
using Space101.ViewModels.SurfaceMorphologyViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Space101.ViewModels.PlanetViewModels
{
    public class ContainerPlanetFormViewModel
    {
        public PlanetFormViewModel PlanetFormViewModel { get; set; }
        public PlanetDetailFormViewModel PlanetDetailFormViewModel { get; set; }
        public PlanetAssetsFormViewModel PlanetAssets { get; set; }
        public IEnumerable<ClimateZoneViewModel> Climates { get; set; }
        public IEnumerable<SurfaceMorphologyViewModel> Terrains { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<PlanetController, ActionResult>> newAction = (c => c.New());
                Expression<Func<PlanetController, ActionResult>> editAction = (c => c.Edit(this.PlanetFormViewModel.PlanetFormViewModelId));

                var action = (this.PlanetFormViewModel.PlanetFormViewModelId != 0) ? editAction : newAction;
                var actionName = (action.Body as MethodCallExpression).Method.Name;

                return actionName;
            }
        }

        private ContainerPlanetFormViewModel(IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            PlanetFormViewModel = new PlanetFormViewModel();
            PlanetDetailFormViewModel = new PlanetDetailFormViewModel();
            PlanetAssets = new PlanetAssetsFormViewModel();
            Climates = climates;
            Terrains = terrains;
        }

        private ContainerPlanetFormViewModel(Planet planet, PlanetDetail planetDetail, FileManager fileManager, IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            PlanetFormViewModel = new PlanetFormViewModel(planet);
            PlanetDetailFormViewModel = new PlanetDetailFormViewModel(planetDetail);
            PlanetAssets = new PlanetAssetsFormViewModel(planet, fileManager);
            Climates = climates;
            Terrains = terrains;
        }

        private ContainerPlanetFormViewModel(PlanetFormViewModel planetFormViewModel, PlanetDetailFormViewModel planetDetailFormViewModel, PlanetAssetsFormViewModel planetAssetsFormViewModel, IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            PlanetFormViewModel = planetFormViewModel;
            PlanetDetailFormViewModel = planetDetailFormViewModel;
            PlanetAssets = planetAssetsFormViewModel;
            Climates = climates;
            Terrains = terrains;
        }

        public static ContainerPlanetFormViewModel CreateEmpty(IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            return new ContainerPlanetFormViewModel(climates, terrains);
        }

        public static ContainerPlanetFormViewModel CreateFromModels(Planet planet, PlanetDetail details, FileManager fileManager, IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            return new ContainerPlanetFormViewModel(planet, details, fileManager, climates, terrains);
        }

        public static ContainerPlanetFormViewModel CreateFromViewModels(PlanetFormViewModel planetFormViewModel, PlanetDetailFormViewModel detailFormViewModel, PlanetAssetsFormViewModel planetAssetsFormViewModel, IEnumerable<ClimateZoneViewModel> climates, IEnumerable<SurfaceMorphologyViewModel> terrains)
        {
            return new ContainerPlanetFormViewModel(planetFormViewModel, detailFormViewModel, planetAssetsFormViewModel, climates, terrains);
        }

    }
}