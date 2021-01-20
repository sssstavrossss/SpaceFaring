using Space101.Enums;
using Space101.ViewModels.ClimateZoneViewModels;
using Space101.ViewModels.PlanetViewModels;
using Space101.ViewModels.SurfaceMorphologyViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public class Planet
    {
        private const double unknownGravity = (double)InvalidPropertyValues.undefinedValue;
        private const int unknownSurfaceWater = (int)InvalidPropertyValues.undefinedValue;

        public int PlanetID { get; private set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must have 3 to 50 letters")]
        public string Name { get; private set; }

        [DefaultValue(unknownGravity)]
        [Range(unknownGravity, 10.0, ErrorMessage = "The Gravity must be a number between 0 and 10, -1 for no info")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Gravity { get; private set; } = unknownGravity;

        [DefaultValue(unknownSurfaceWater)]
        [Range(unknownSurfaceWater, 100, ErrorMessage = "The Surface Water must be an integer between 0 and 100, -1 for no info")]
        public int SurfaceWater { get; private set; } = unknownSurfaceWater;

        [DisplayFormat(NullDisplayText = "No URL")]
        public string Url { get; private set; }

        public PlanetDetail Details { get; private set; }

        //To become private set
        public ICollection<RaceHabitat> RaceHabitats { get; set; }
        public ICollection<FlightPath> Destinations { get; private set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<ClimateZone> ClimateZones { get; private set; }
        public ICollection<SurfaceMorphology> SurfaceMorphologies { get; private set; }

        protected Planet()
        {
            ClimateZones = new List<ClimateZone>();
            SurfaceMorphologies = new List<SurfaceMorphology>();
        }

        private Planet(string name, double gravity, int surfaceWater, string url)
        {
            Name = name;
            Gravity = gravity;
            SurfaceWater = surfaceWater;
            Url = url;
            ClimateZones = new List<ClimateZone>();
            SurfaceMorphologies = new List<SurfaceMorphology>();
        }

        private Planet(PlanetFormViewModel formViewModel)
        {
            Name = formViewModel.Name;
            Gravity = formViewModel.Gravity;
            SurfaceWater = formViewModel.SurfaceWater;
            Url = formViewModel.Url;
            ClimateZones = new List<ClimateZone>();
            SurfaceMorphologies = new List<SurfaceMorphology>();
        }

        public static Planet Create(string name, double gravity, int surfaceWater, string url)
        {
            return new Planet(name, gravity, surfaceWater, url);
        }

        public static Planet CreateFromFormViewModel(PlanetFormViewModel planetFormViewModel)
        {
            return new Planet(planetFormViewModel);
        }

        public void UpdateSecondaryData(PlanetDetail details, ICollection<ClimateZoneViewModel> climateZoneViewModels, ICollection<SurfaceMorphologyViewModel> surfaceMorphologyViewModels)
        {
            AssignDetails(details);
            AssignClimates(climateZoneViewModels);
            AssignTerrains(surfaceMorphologyViewModels);
        }

        private void AssignDetails(PlanetDetail details)
        {
            if (details == null)
                return;
            Details = details.PlanetID == PlanetID ? details : null;
        }

        private void AssignClimates(ICollection<ClimateZoneViewModel> climateZoneViewModels)
        {
            ClimateZones.Clear();

            foreach (var viewModel in climateZoneViewModels)
            {
                if (viewModel.PlanetHasClimate)
                {
                    var zone = new ClimateZone(PlanetID,viewModel.ClimateId);
                    ClimateZones.Add(zone);
                }
            }
        }

        private void AssignTerrains(ICollection<SurfaceMorphologyViewModel> surfaceMorphologyViewModels)
        {
            SurfaceMorphologies.Clear();

            foreach (var viewModel in surfaceMorphologyViewModels)
            {
                if (viewModel.PlanetHasTerrain)
                {
                    var morphology = new SurfaceMorphology(PlanetID, viewModel.TerrainId);
                    SurfaceMorphologies.Add(morphology);
                }
            }
        }

        public void Update(string name, double? gravity, int? surfaceWater, string url)
        {
            Name = string.IsNullOrWhiteSpace(name) ? Name : name;
            Gravity = gravity ?? Gravity;
            SurfaceWater = surfaceWater ?? SurfaceWater;
            Url = string.IsNullOrWhiteSpace(url) ? Url : url;
        }

        public void UpdateFromFormViewModel(PlanetFormViewModel planetFormViewModel)
        {
            Name = string.IsNullOrWhiteSpace(planetFormViewModel.Name) ? Name : planetFormViewModel.Name;
            Gravity = planetFormViewModel.Gravity;
            SurfaceWater = planetFormViewModel.SurfaceWater;
            Url = string.IsNullOrWhiteSpace(planetFormViewModel.Url) ? Url : planetFormViewModel.Url;
        }

    }
}