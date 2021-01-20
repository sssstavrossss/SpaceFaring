using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class StarshipDto
    {
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int PassengerCapacity { get; set; }
        public int CargoCapacity { get; set; }
        public double Length { get; set; }
        public StarshipDto()  { }
    }
}