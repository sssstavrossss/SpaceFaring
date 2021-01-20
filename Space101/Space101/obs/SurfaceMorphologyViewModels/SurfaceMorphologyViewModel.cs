using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.SurfaceMorphologyViewModels
{
    public class SurfaceMorphologyViewModel
    {
        public int TerrainId { get; set; }
        public string TerrainName { get; set; }
        public bool PlanetHasTerrain { get; set; }

        public SurfaceMorphologyViewModel()
        { }

        public SurfaceMorphologyViewModel(int terrainId, string terrainName, bool planetHasIt)
        {
            TerrainId = terrainId;
            TerrainName = terrainName;
            PlanetHasTerrain = planetHasIt;
        }
    }
}