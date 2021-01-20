using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class ClimateZone
    {
        [Key]
        [Column(Order = 0)]
        public int PlanetId { get; private set; }

        [Key]
        [Column(Order = 1)]
        public int ClimateId { get; private set; }

        public Planet Planet { get; private set; }
        public Climate Climate { get; private set; }

        protected ClimateZone()
        { }

        public ClimateZone(int planetId, int climateId)
        {
            PlanetId = planetId;
            ClimateId = climateId;
        }
    }
}