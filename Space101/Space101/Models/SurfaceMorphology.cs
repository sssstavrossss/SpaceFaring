using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class SurfaceMorphology
    {
        [Key]
        [Column(Order = 0)]
        public int PlanetId { get; private set; }

        [Key]
        [Column(Order = 1)]
        public int TerrainId { get; private set; }

        public Planet Planet { get; private set; }
        public Terrain Terrain { get; private set; }

        protected SurfaceMorphology()
        { }

        public SurfaceMorphology(int planetId, int terrainId)
        {
            PlanetId = planetId;
            TerrainId = terrainId;
        }
    }
}