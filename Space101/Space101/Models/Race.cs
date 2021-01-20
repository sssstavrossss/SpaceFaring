using Space101.Enums;
using Space101.Services;
using Space101.ViewModels.RaceViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Space101.Models
{
    public class Race
    {
        public int RaceID { get; private set; }

        [DisplayName("Race Name")]
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Race Name must be between 2 and 50 characters")]
        public string Name { get; private set; }

        [DisplayName("Race Classification")]
        [Required(ErrorMessage = "Race Classification is Required")]
        public int RaceClassificationID { get; private set; }

        [DisplayName("Average Height (cm)")]
        [Required(ErrorMessage = "Average Height is Required!")]
        [Range(40, 600, ErrorMessage = "Average Height must be between 40cm and 600cm")]
        public int AverageHeight { get; private set; }

        public byte[] Avatar { get; private set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; private set; }
        public ICollection<RaceHabitat> RaceHabitats { get; private set; }
        public RaceClassification RaceClassification { get; private set; }
        public ICollection<RaceFile> Assets { get; private set; }

        protected Race()
        {
            ApplicationUsers = new List<ApplicationUser>();
            RaceHabitats = new List<RaceHabitat>();
            Assets = new List<RaceFile>();
            Avatar = new byte[0];
        }

        private Race(RaceFormViewModel race, List<RaceHabitat> raceHabitats, byte[] avatar)
        {
            RaceID = race.ID;
            Name = race.Name;
            AverageHeight = race.AverageHeight;
            RaceClassificationID = race.RaceClassificationID;
            RaceHabitats = raceHabitats;
            Avatar = avatar;
            Assets = new List<RaceFile>();
        }

        private void RaceUpdate(RaceFormViewModel race, List<RaceHabitat> raceHabitats, byte[] avatar)
        {
            RaceID = race.ID;
            Name = race.Name;
            AverageHeight = race.AverageHeight;
            RaceClassificationID = race.RaceClassificationID;
            RaceHabitats = raceHabitats;
            Avatar = avatar;
            if (race.Assets != null && race.Assets.Count > 0)
                Assets = race.Assets;
        }

        public static Race RaceCreation(RaceFormViewModel race, FileManager fileManager)
        {
            List<RaceHabitat> raceHabitats = new List<RaceHabitat>();

            byte[] avatar = race.GetUploadedAvatar(fileManager);

            race.Planets
                .Where(p => p.IsAssigned == true).ToList()
                .ForEach(p => raceHabitats.Add(RaceHabitat.RaceHabitatCreate(p.ID, race.ID)));

            return new Race(race, raceHabitats, avatar);
        }
        public void RaceUpdate(RaceFormViewModel race, FileManager fileManager)
        {
            List<RaceHabitat> raceHabitats = new List<RaceHabitat>();

            byte[] avatar = race.GetUploadedAvatar(fileManager);

            race.Planets
                .Where(p => p.IsAssigned == true).ToList()
                .ForEach(p => raceHabitats.Add(RaceHabitat.RaceHabitatCreate(p.ID, race.ID)));

            RaceUpdate(race, raceHabitats, avatar);
        }

        public void InitializeAssets()
        {
            CreateTextFiles().ForEach(a => Assets.Add(a));
        }

        public void UpdateAssets(FileManager fileManager, RaceFormViewModel race)
        {
            EditTextFiles(fileManager, race);
        }

        private List<RaceFile> CreateTextFiles()
        {
            var assets = new List<RaceFile>();
            var detailsFile = new FilePath(Guid.NewGuid().ToString() + "-Details" + ".txt", FileType.Text, ".txt");

            var texts = new List<FilePath> { detailsFile };
            texts.ForEach(txt => assets.Add(new RaceFile(txt)));

            return assets.ToList();
        }

        private void EditTextFiles(FileManager fileManager, RaceFormViewModel race)
        {
            if (Assets == null || Assets.Count == 0)
                InitializeAssets();
        }

        public void SaveAssets(FileManager filemanager, RaceFormViewModel viewModel)
        {
            var texts = Assets.Where(a => a.FilePath.FileType == FileType.Text).ToList();
            filemanager.SaveTextFile(texts[0], viewModel.Details);
        }
    }
}