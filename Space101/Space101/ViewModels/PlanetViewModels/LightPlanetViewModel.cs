using Space101.Models;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.PlanetViewModels
{
    public class LightPlanetViewModel
    {
        public int PlanetID { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must have 3 to 50 letters")]
        public string Name { get; set; }

        protected LightPlanetViewModel()
        { }

        private LightPlanetViewModel(int planetId, string name)
        {
            PlanetID = planetId;
            Name = name;
        }

        private LightPlanetViewModel(Planet planet)
        {
            PlanetID = planet.PlanetID;
            Name = planet.Name;
        }

        public static LightPlanetViewModel Create(int planetId, string name)
        {
            return new LightPlanetViewModel(planetId, name);
        }

        public static LightPlanetViewModel CreateFromModel(Planet planet)
        {
            return new LightPlanetViewModel(planet);
        } 
    }
}