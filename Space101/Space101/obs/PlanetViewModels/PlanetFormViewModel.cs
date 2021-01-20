using Space101.Enums;
using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.PlanetViewModels
{
    public class PlanetFormViewModel
    {
        private const double unknownGravity = (double)InvalidPropertyValues.undefinedValue;
        private const int unknownSurfaceWater = (int)InvalidPropertyValues.undefinedValue;

        public int PlanetFormViewModelId { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must have 3 to 50 letters")]
        public string Name { get; set; }

        [DefaultValue(unknownGravity)]
        [Range(unknownGravity, 10.0, ErrorMessage = "The Gravity must be a number between 0 and 10, -1 for no info")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Gravity { get; set; } = unknownGravity;

        [DefaultValue(unknownSurfaceWater)]
        [Range(unknownSurfaceWater, 100, ErrorMessage = "The Surface Water must be an integer between 0 and 100, -1 for no info")]
        public int SurfaceWater { get; set; } = unknownSurfaceWater;

        [DisplayFormat(NullDisplayText = "No URL")]
        public string Url { get; set; }

        public PlanetFormViewModel()
        { }

        public PlanetFormViewModel(Planet planet)
        {
            if (planet == null)
                return;
            PlanetFormViewModelId = planet.PlanetID;
            Name = planet.Name;
            Gravity = planet.Gravity;
            SurfaceWater = planet.SurfaceWater;
            Url = planet.Url;
        }
    }
}