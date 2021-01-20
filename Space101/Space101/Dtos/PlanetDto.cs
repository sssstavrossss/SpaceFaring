using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class PlanetDto
    {
        public int PlanetID { get;  set; }
        public string Name { get;  set; }
        public double Gravity { get;  set; }
        public int SurfaceWater { get;  set; }
        public string Title { get;  set; }
        public decimal CoordX { get; set; }
        public decimal CoordY { get;  set; }
        public byte[] Avatar { get;  set; }
        public bool IsActive { get;  set; }

        public PlanetDetailDto Details { get;  set; }

        public ICollection<RaceHabitatPLANETDto> RaceHabitats { get; set; }
        public ICollection<ClimateZoneDto> ClimateZones { get;  set; }
        public ICollection<SurfaceMorphologyDto> SurfaceMorphologies { get;  set; }

    }
}