using Space101.Enums;
using Space101.Models;
using Space101.Services;
using System.Collections.Generic;
using System.Linq;

namespace Space101.ViewModels.RaceViewModels
{
    public class UserRaceViewModel
    {
        public Race Race { get; set; }
        public string Details { get; set; }

        public UserRaceViewModel() { }

        public UserRaceViewModel(Race race, FileManager fileManager)
        {
            Race = race;
            AssignTexts(race.Assets, fileManager);
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