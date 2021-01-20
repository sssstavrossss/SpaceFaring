using Space101.Models;

namespace Space101.ViewModels.RaceViewModels
{
    public class LightRaceViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }

        private LightRaceViewModel()
        { }

        private LightRaceViewModel(int id, string name, byte[] avatar)
        {
            ID = id;
            Name = name;
            Avatar = avatar ?? new byte[0];
        }

        private LightRaceViewModel(Race race)
        {
            ID = race.RaceID;
            Name = race.Name;
            Avatar = race.Avatar ?? new byte[0];
        }

        public static LightRaceViewModel Create(int id, string name, byte[] avatar)
        {
            return new LightRaceViewModel(id, name, avatar);
        }

        public static LightRaceViewModel CreateFromModel(Race race)
        {
            return new LightRaceViewModel(race);
        }
    }
}