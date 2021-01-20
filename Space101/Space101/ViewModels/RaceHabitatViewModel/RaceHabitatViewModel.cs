using Space101.Models;
using System.ComponentModel;

namespace Space101.ViewModels
{
    public class RaceHabitatViewModel
    {
        public int ID { get; set; }

        [DisplayName("Planet Habitats")]
        public string Name { get; set; }

        public bool IsAssigned { get; set; }

        public RaceHabitatViewModel() { }

        private RaceHabitatViewModel(int id, string name, bool isAssigned)
        {
            ID = id;
            Name = name;
            IsAssigned = isAssigned;
        }

        public static RaceHabitatViewModel RaceHabitatViewModelCreation(Planet planet, bool assigned)
        {
            return new RaceHabitatViewModel(planet.PlanetID, planet.Name, assigned);
        }
    }
}