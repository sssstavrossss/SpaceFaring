using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class PlanetDetailDto
    {
        public int RotationPeriod { get;  set; }
        public int OrbitalPeriod { get; set; }
        public int Diameter { get; set; }
        public long Population { get; set; }
    }
}