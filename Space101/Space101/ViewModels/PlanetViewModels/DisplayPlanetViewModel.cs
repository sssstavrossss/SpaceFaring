using System.ComponentModel.DataAnnotations;
using Space101.Models;

namespace Space101.ViewModels.PlanetViewModels
{
    public class DisplayPlanetViewModel
    {
        [Display(Name = "Planet Id")]
        public int PlanetID { get; private set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Gravity { get; set; }

        public int SurfaceWater { get; set; }

        [DisplayFormat(NullDisplayText = "The planet has no title")]
        public string Title { get; set; }

        public byte[] Avatar { get; set; }

        public DisplayPlanetViewModel()
        { }

        public DisplayPlanetViewModel(Planet planet)
        {
            PlanetID = planet.PlanetID;
            Name = planet.Name;
            Gravity = planet.Gravity;
            SurfaceWater = planet.SurfaceWater;
            Title = planet.Title;
            Avatar = planet.Avatar;
        }
    }
}