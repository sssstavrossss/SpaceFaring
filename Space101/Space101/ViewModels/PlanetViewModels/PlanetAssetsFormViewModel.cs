using Space101.Enums;
using Space101.Models;
using Space101.Services;
using Space101.ViewModels.PlanetFileViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.PlanetViewModels
{
    public class PlanetAssetsFormViewModel
    {
        public List<HttpPostedFileBase> Images { get; set; }
        public List<PlanetFileFormViewModel> planetFileFormViewModels { get; set; }

        public string History { get; set; }
        public string Culture { get; set; }
        public string ToDo { get; set; }

        public PlanetAssetsFormViewModel()
        {
            Images = new List<HttpPostedFileBase>();
            planetFileFormViewModels = new List<PlanetFileFormViewModel>();
        }
        public PlanetAssetsFormViewModel(Planet planet, FileManager fileManager)
        {
            planetFileFormViewModels = InitializeFileViewModels(planet.Assets ?? new List<PlanetFile>());
            AssignTexts(planet.Assets, fileManager);
        }

        private List<PlanetFileFormViewModel> InitializeFileViewModels(ICollection<PlanetFile> assets)
        {
            var result = new List<PlanetFileFormViewModel>();
            foreach (var item in assets)
            {
                if (item.GetFileType() == FileType.Image.ToString())
                    result.Add(new PlanetFileFormViewModel (item.FilePathId, item.GetFullPath(), item.GetFileType(), false ));
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