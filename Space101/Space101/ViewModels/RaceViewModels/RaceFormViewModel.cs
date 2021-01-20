using Space101.Enums;
using Space101.Models;
using Space101.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.RaceViewModels
{
    public class RaceFormViewModel
    {
        public int ID { get; set; }

        [DisplayName("Race Name")]
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Race Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        [DisplayName("Race Classification")]
        [Required(ErrorMessage = "Race Classification is Required")]
        public int RaceClassificationID { get; set; }

        [DisplayName("Average Height (cm)")]
        [Required(ErrorMessage = "Average Height is Required!")]
        [Range(40, 600, ErrorMessage = "Average Height must be between 40cm and 600cm")]
        public int AverageHeight { get; set; }

        public byte[] Avatar { get; set; }

        [StringLength(1024)]
        public string Details { get; set; }

        public HttpPostedFileBase UploadedAvatar { get; set; }


        [DisplayName("Planet Habitats")]
        public List<RaceHabitatViewModel> Planets { get; set; }

        public List<RaceClassificationViewModel> RaceClassificationList { get; set; }

        public RaceClassificationViewModel RaceClassification { get; set; }

        public ICollection<RaceFile> Assets { get; set; }

        public RaceFormViewModel() 
        {
            Avatar = new byte[0];
            Planets = new List<RaceHabitatViewModel>();
            RaceClassificationList = new List<RaceClassificationViewModel>();
            Assets = new List<RaceFile>();
        }

        private RaceFormViewModel
            (List<RaceHabitatViewModel> planets, List<RaceClassificationViewModel> raceClassifications, FileManager fileManager)
        {
            Planets = planets;
            RaceClassificationList = raceClassifications;
            Assets = new List<RaceFile>();
            Avatar = new byte[0];
        }

        private RaceFormViewModel
            (Race race, List<RaceHabitatViewModel> planets, List<RaceClassificationViewModel> raceClassifications, FileManager fileManager)
        {
            ID = race.RaceID;
            Planets = planets;
            RaceClassificationList = raceClassifications;
            Name = race.Name;
            AverageHeight = race.AverageHeight;
            RaceClassificationID = race.RaceClassificationID;
            Avatar = race.Avatar ?? new byte[0];
            AssignTexts(race.Assets, fileManager);
        }

        private RaceFormViewModel
            (RaceFormViewModel viewmodel, List<RaceClassificationViewModel> raceClassifications, FileManager fileManager)
        {
            ID = viewmodel.ID;
            Planets = viewmodel.Planets;
            RaceClassificationList = raceClassifications;
            RaceClassificationID = viewmodel.RaceClassificationID;
            Name = viewmodel.Name;
            AverageHeight = viewmodel.AverageHeight;
            Avatar = viewmodel.Avatar ?? new byte[0];
            AssignTexts(viewmodel.Assets, fileManager);
        }

        public static RaceFormViewModel RaceFormViewModelCreationNew
            (List<Planet> planetsAll, List<RaceClassification> raceClassificationList, FileManager fileManager)
        {
            List<RaceHabitatViewModel> racePlanets = new List<RaceHabitatViewModel>();
            List<RaceClassificationViewModel> raceClassifications = new List<RaceClassificationViewModel>();

            planetsAll.ForEach(p => racePlanets
                .Add(RaceHabitatViewModel.RaceHabitatViewModelCreation(p, false)));

            raceClassificationList
                .ForEach(rc => raceClassifications.Add(RaceClassificationViewModel.RaceClassificationViewModelCreation(rc)));

            return new RaceFormViewModel(racePlanets, raceClassifications, fileManager);
        }

        public static RaceFormViewModel RaceFormViewModelEdit
            (Race race, List<Planet> planetsAll, List<RaceClassification> raceClassificationList, FileManager fileManager)
        {
            List<RaceHabitatViewModel> racePlanets = new List<RaceHabitatViewModel>();
            List<RaceClassificationViewModel> raceClassifications = new List<RaceClassificationViewModel>();

            planetsAll.ForEach(p => racePlanets
                .Add(RaceHabitatViewModel.RaceHabitatViewModelCreation(p, race.RaceHabitats.Select(rh => rh.Planet).Contains(p))));

            raceClassificationList
                .ForEach(rc => raceClassifications.Add(RaceClassificationViewModel.RaceClassificationViewModelCreation(rc)));

            return new RaceFormViewModel(race, racePlanets, raceClassifications, fileManager);
        }

        public static RaceFormViewModel 
            RaceFormViewModelValidate(RaceFormViewModel viewmodel, List<RaceClassification> raceClassification, FileManager fileManager)
        {
            List<RaceClassificationViewModel> raceClassifications = new List<RaceClassificationViewModel>();

            raceClassification
                .ForEach(rc => raceClassifications.Add(RaceClassificationViewModel.RaceClassificationViewModelCreation(rc)));

            return new RaceFormViewModel(viewmodel, raceClassifications, fileManager);
        }

        public byte[] GetUploadedAvatar(FileManager fileManager)
        {
            byte[] avatar;
            if (fileManager.FileIsValid(UploadedAvatar))
            {
                using (var reader = new BinaryReader(UploadedAvatar.InputStream))
                {
                    avatar = reader.ReadBytes(UploadedAvatar.ContentLength);
                }
            }
            else
                avatar = Avatar ?? new byte[0];

            return avatar;
        }

        private void AssignTexts(ICollection<RaceFile> Assets, FileManager fileManager)
        {
            foreach (var asset in Assets)
            {
                if (asset.FilePath.FileType == FileType.Text)
                {
                    string name = asset.FilePath.FileName.Split('-').Last();
                    switch (name)
                    {
                        case "Details.txt":
                            Details = fileManager.ReadTextFile(asset);
                            break;
                    }
                }
            }
        }

    }
}