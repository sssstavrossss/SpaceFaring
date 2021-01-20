using Space101.Enums;
using Space101.Models;
using Space101.Services;
using Space101.ViewModels.PlanetFileViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Space101.ViewModels.PlanetUserViewModels
{
    public class PlanetUserViewModel
    {

        public Planet Planet { get; set; }
        public string History { get; set; }
        public string Culture { get; set; }
        public List<PlanetFileFormViewModel> Images { get; set; }
        public List<FlightPath> Departures { get; set; }
        public List<FlightPath> Destinations { get; set; }

        public PlanetUserViewModel() 
        {
            Images = new List<PlanetFileFormViewModel>();
            Departures = new List<FlightPath>();
            Destinations = new List<FlightPath>();
        }

        public PlanetUserViewModel
            (Planet planet, FileManager fileManager, List<FlightPath> flightPathsDestination, List<FlightPath> flightPathsDeparture)
        {
            Planet = planet;
            AssignTexts(planet.Assets, fileManager);
            Images = InitializeFileViewModels(planet.Assets ?? new List<PlanetFile>());
            Departures = flightPathsDeparture ?? new List<FlightPath>();
            Destinations = flightPathsDestination ?? new List<FlightPath>();
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
                    }
                }
            }
        }

        private List<PlanetFileFormViewModel> InitializeFileViewModels(ICollection<PlanetFile> assets)
        {
            var result = new List<PlanetFileFormViewModel>();
            foreach (var item in assets)
            {
                if (item.GetFileType() == FileType.Image.ToString())
                    result.Add(new PlanetFileFormViewModel(item.FilePathId, item.GetFullPath(), item.GetFileType(), false));
            }
            return result;
        }
    }
}