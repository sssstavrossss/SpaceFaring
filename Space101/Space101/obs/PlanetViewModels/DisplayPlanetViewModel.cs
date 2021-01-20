using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Space101.Models;

namespace Space101.ViewModels.PlanetViewModels
{
    public class DisplayPlanetViewModel
    {
        public int PlanetID { get; private set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Gravity { get; set; }

        public int SurfaceWater { get; set; }

        [DisplayFormat(NullDisplayText = "No URL")]
        public string Url { get; private set; }

        public DisplayPlanetViewModel()
        { }

        public DisplayPlanetViewModel(Planet planet)
        {
            PlanetID = planet.PlanetID;
            Name = planet.Name;
            Gravity = planet.Gravity;
            SurfaceWater = planet.SurfaceWater;
            Url = planet.Url;
        }
    }
}