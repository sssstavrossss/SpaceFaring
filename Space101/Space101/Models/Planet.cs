using Space101.Enums;
using Space101.Services;
using Space101.ViewModels.ClimateZoneViewModels;
using Space101.ViewModels.PlanetViewModels;
using Space101.ViewModels.SurfaceMorphologyViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public partial class Planet
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

        [DisplayFormat(NullDisplayText = "The planet has no title")]
        public string Title { get; private set; }

        [Display(Name = "Coordinate X")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal CoordX { get; set; }

        [Display(Name = "Coordinate Y")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal CoordY { get; private set; }

        public byte[] Avatar { get; private set; }

        public bool IsActive { get; private set; }

        public PlanetDetail Details { get; private set; }

        //To become private set
        public ICollection<PlanetFile> Assets { get; private set; }
        public ICollection<RaceHabitat> RaceHabitats { get; private set; }
        public ICollection<FlightPath> Destinations { get; private set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<ClimateZone> ClimateZones { get; private set; }
        public ICollection<SurfaceMorphology> SurfaceMorphologies { get; private set; }

        private List<FileUploadBinder> UploadImagesBinds { get; set; }

        [NotMapped]
        public List<FilePath> FilePathsToDelete { get; private set; }

        protected Planet()
        {
            ClimateZones = new List<ClimateZone>();
            SurfaceMorphologies = new List<SurfaceMorphology>();
            Assets = new List<PlanetFile>();
            UploadImagesBinds = new List<FileUploadBinder>();
            FilePathsToDelete = new List<FilePath>();
            Avatar = new byte[0];
            IsActive = true;
        }
        private Planet(string name, double gravity, int surfaceWater, string title, byte[] avatar, decimal coordX, decimal coordY)
        {
            Name = name;
            Gravity = gravity;
            SurfaceWater = surfaceWater;
            Title = title;
            ClimateZones = new List<ClimateZone>();
            SurfaceMorphologies = new List<SurfaceMorphology>();
            Avatar = avatar;
            Assets = new List<PlanetFile>();
            UploadImagesBinds = new List<FileUploadBinder>();
            FilePathsToDelete = new List<FilePath>();
            CoordX = coordX;
            CoordY = coordY;
            IsActive = true;
        }
    }
    public partial class Planet
    {
        // * * * * * * * * * * * * Planet Basic Model Methods * * * * * * * * * * * * * * *

        public static Planet Create(string name, double gravity, int surfaceWater, string title, byte[] avatar, decimal coordX, decimal coordY)
        {
            return new Planet(name, gravity, surfaceWater, title, avatar, coordX, coordY);
        }
        public static Planet CreateFromFormViewModel(PlanetFormViewModel planetFormViewModel, FileManager fileManager)
        {
            byte[] avatar = planetFormViewModel.GetUploadedAvatar(fileManager);

            return new Planet(planetFormViewModel.Name, planetFormViewModel.Gravity, planetFormViewModel.SurfaceWater, planetFormViewModel.Title, avatar, planetFormViewModel.CoordX, planetFormViewModel.CoordY);
        }

        public void ChangeActiveStatus(bool isActive)
        {
            IsActive = IsActive;
        }
        public void Update(string name, double? gravity, int? surfaceWater, string title, byte[] avatar, decimal? coordX, decimal? coordY)
        {
            Name = string.IsNullOrWhiteSpace(name) ? Name : name;
            Gravity = gravity ?? Gravity;
            SurfaceWater = surfaceWater ?? SurfaceWater;
            Title = string.IsNullOrWhiteSpace(title) ? Title : title;
            Avatar = avatar != null && avatar.Length > 0 ? avatar : Avatar;
            CoordX = coordX ?? CoordX;
            CoordY = coordY ?? CoordY;
        }
        public void UpdateFromFormViewModel(PlanetFormViewModel planetFormViewModel, FileManager fileManager)
        {
            byte[] avatar = planetFormViewModel.GetUploadedAvatar(fileManager);

            Name = string.IsNullOrWhiteSpace(planetFormViewModel.Name) ? Name : planetFormViewModel.Name;
            Gravity = planetFormViewModel.Gravity;
            SurfaceWater = planetFormViewModel.SurfaceWater;
            Title = string.IsNullOrWhiteSpace(planetFormViewModel.Title) ? Title : planetFormViewModel.Title;
            Avatar = avatar;
            CoordX = planetFormViewModel.CoordX;
            CoordY = planetFormViewModel.CoordY;
        }
    }
    public partial class Planet
    {
        // * * * * * * * * * * * * Planet Secondary Data Methods * * * * * * * * * * * * * * *

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

            if (climateZoneViewModels == null)
                return;

            foreach (var viewModel in climateZoneViewModels)
            {
                if (viewModel.PlanetHasClimate)
                {
                    var zone = new ClimateZone(PlanetID, viewModel.ClimateId);
                    ClimateZones.Add(zone);
                }
            }
        }
        private void AssignTerrains(ICollection<SurfaceMorphologyViewModel> surfaceMorphologyViewModels)
        {
            SurfaceMorphologies.Clear();

            if (surfaceMorphologyViewModels == null)
                return;

            foreach (var viewModel in surfaceMorphologyViewModels)
            {
                if (viewModel.PlanetHasTerrain)
                {
                    var morphology = new SurfaceMorphology(PlanetID, viewModel.TerrainId);
                    SurfaceMorphologies.Add(morphology);
                }
            }
        }
    }
    public partial class Planet
    {
        // * * * * * * * * * * * * Planet Assets Methods * * * * * * * * * * * * * * *

        public void InitializeAssets(FileManager fileManager, PlanetAssetsFormViewModel viewModel)
        {
            CreateAssets(fileManager, viewModel.Images).ForEach(a => Assets.Add(a));
            CreateTextFiles().ForEach(a => Assets.Add(a));
        }
        private List<PlanetFile> CreateAssets(FileManager filemanager, IEnumerable<HttpPostedFileBase> uploadedImages)
        {
            var assets = new List<PlanetFile>();
            var imagesFiles = filemanager.FilterValidFiles(uploadedImages);
            foreach (var file in imagesFiles)
            {
                string name = Path.GetFileNameWithoutExtension(file.FileName);
                string nameExtension = Path.GetExtension(file.FileName);
                var image = new FilePath(Guid.NewGuid().ToString() + "." + name + nameExtension, FileType.Image, nameExtension);
                var imageFile = new PlanetFile(image);
                assets.Add(imageFile);
                UploadImagesBinds.Add(new FileUploadBinder(imageFile, file));
            }

            return assets.ToList();
        }
        private List<PlanetFile> CreateTextFiles()
        {
            var assets = new List<PlanetFile>();
            var historyFile = new FilePath(Guid.NewGuid().ToString() + "-History" + ".txt", FileType.Text, ".txt");
            var cultureFile = new FilePath(Guid.NewGuid().ToString() + "-Culture" + ".txt", FileType.Text, ".txt");
            var toDoFile = new FilePath(Guid.NewGuid().ToString() + "-ToDo" + ".txt", FileType.Text, ".txt");

            var texts = new List<FilePath> { historyFile, cultureFile, toDoFile };
            texts.ForEach(txt => assets.Add(new PlanetFile(txt)));

            return assets.ToList();
        }
        public void UpdateAssets(FileManager fileManager, PlanetAssetsFormViewModel viewModel)
        {
            RemoveAssets(fileManager, viewModel);

            CreateAssets(fileManager, viewModel.Images).ForEach(a => Assets.Add(a));
            EditTextFiles(fileManager, viewModel);
        }
        private void RemoveAssets(FileManager fileManager, PlanetAssetsFormViewModel viewModel)
        {
            foreach (var asset in viewModel.planetFileFormViewModels)
            {
                if (asset.DeleteIt)
                {
                    var assetToRemove = Assets.Single(a => a.FilePathId == asset.PlanetFileId);
                    FilePathsToDelete.Add(assetToRemove.FilePath);
                    Assets.Remove(assetToRemove);
                    fileManager.DeleteFile(assetToRemove);
                }
            }
        }
        private void EditTextFiles(FileManager fileManager, PlanetAssetsFormViewModel viewModel)
        {
            if (Assets.Where(a => a.FilePath.FileType == FileType.Text).ToList().Count == 0)
                CreateTextFiles().ForEach(a => Assets.Add(a));
        }
        public void SaveAssets(FileManager filemanager, PlanetAssetsFormViewModel viewModel)
        {
            for (var i = 0; i < UploadImagesBinds.Count; i++)
            {
                filemanager.SaveUploadedFile(UploadImagesBinds[i].PostedFile, UploadImagesBinds[i].FilePath);
            }

            var texts = Assets.Where(a => a.FilePath.FileType == FileType.Text).ToList();

            filemanager.SaveTextFile(texts[0], viewModel.History);
            filemanager.SaveTextFile(texts[1], viewModel.Culture);
            filemanager.SaveTextFile(texts[2], viewModel.ToDo);
        }
    }
}