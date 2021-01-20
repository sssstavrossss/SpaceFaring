using Space101.Enums;
using Space101.Models;
using Space101.Services;
using Space101.ViewModels.ClimateViewModels;
using Space101.ViewModels.PlanetFileViewModels;
using Space101.ViewModels.RaceViewModels;
using Space101.ViewModels.TerrainViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Space101.ViewModels.PlanetViewModels
{
    public class DisplayFullPlanetViewModel
    {
        public int PlanetID { get; private set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Gravity { get; set; }

        public int SurfaceWater { get; set; }
        public string Title { get; set; }

        [Display(Name = "Coordinate X")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal CoordX { get; set; }

        [Display(Name = "Coordinate Y")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal CoordY { get; set; }

        public byte[] Avatar { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Rotation Period")]
        public int RotationPeriod { get; set; }

        [Display(Name = "Orbital Period")]
        public int OrbitalPeriod { get; set; }

        public int Diameter { get; set; }
        public long Population { get; set; }

        public List<DisplayPlanetFileViewModel> PlanetFileViewModels { get; set; }

        public string History { get; set; }
        public string Culture { get; set; }

        [Display(Name = "Sights")]
        public string ToDo { get; set; }

        public List<ClimateViewModel> Climates { get; set; }
        public List<TerrainViewModel> Terrains { get; set; }
        public List<LightRaceViewModel> Habitants { get; set; }

        public DisplayFullPlanetViewModel()
        {
            PlanetFileViewModels = new List<DisplayPlanetFileViewModel>();
            Climates = new List<ClimateViewModel>();
            Terrains = new List<TerrainViewModel>();
            Habitants = new List<LightRaceViewModel>();
        }
        public DisplayFullPlanetViewModel(Planet planet, FileManager fileManager)
        {
            PlanetID = planet.PlanetID;
            Name = planet.Name;
            Gravity = planet.Gravity;
            SurfaceWater = planet.SurfaceWater;
            Title = planet.Title;
            CoordX = planet.CoordX;
            CoordY = planet.CoordY;
            Avatar = planet.Avatar;
            IsActive = planet.IsActive;

            if(planet.Details != null)
            {
                RotationPeriod = planet.Details.RotationPeriod;
                OrbitalPeriod = planet.Details.OrbitalPeriod;
                Diameter = planet.Details.Diameter;
                Population = planet.Details.Population;
            }

            Climates = new List<ClimateViewModel>();
            if(planet.ClimateZones != null)
            {
                foreach (var climateZone in planet.ClimateZones)
                {
                    Climates.Add(new ClimateViewModel(climateZone.Climate));
                }
            }

            Terrains = new List<TerrainViewModel>();
            if (planet.SurfaceMorphologies != null)
            {
                foreach (var surfaceMorfology in planet.SurfaceMorphologies)
                {
                    Terrains.Add(new TerrainViewModel(surfaceMorfology.Terrain));
                }
            }

            Habitants = new List<LightRaceViewModel>();
            if (planet.RaceHabitats != null)
            {
                foreach (var raceHabitat in planet.RaceHabitats)
                {
                    Habitants.Add(LightRaceViewModel.CreateFromModel(raceHabitat.Race));
                }
            }

            PlanetFileViewModels = InitializeFileViewModels(planet.Assets ?? new List<PlanetFile>());
            AssignTexts(planet.Assets, fileManager);
        }

        private List<DisplayPlanetFileViewModel> InitializeFileViewModels(ICollection<PlanetFile> assets)
        {
            var result = new List<DisplayPlanetFileViewModel>();
            foreach (var item in assets)
            {
                if (item.GetFileType() == FileType.Image.ToString())
                    result.Add(new DisplayPlanetFileViewModel(item.GetFullPath(), item.GetFileType()));
            }
            return result;
        }
        private void AssignTexts(ICollection<PlanetFile> Assets, FileManager fileManager)
        {
            foreach (var asset in Assets)
            {
                if (asset.FilePath.FileType == FileType.Text)
                {
                    string name = asset.FilePath.FileName.Split('-').Last();
                    switch (name)
                    {
                        case "History.txt":
                            History = fileManager.ReadTextFile(asset);
                            break;
                        case "Culture.txt":
                            Culture = fileManager.ReadTextFile(asset);
                            break;
                        case "ToDo.txt":
                            ToDo = fileManager.ReadTextFile(asset);
                            break;
                    }
                }
            }
        }

    }
}