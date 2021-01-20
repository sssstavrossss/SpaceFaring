using Space101.Models;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.TerrainViewModels
{
    public class TerrainViewModel
    {
        [Display(Name = "Terrain Id")]
        public int TerrainId { get; private set; }
        public string Name { get; private set; }

        [Display(Name = "Display Color")]
        public string DisplayColor { get; set; }

        public TerrainViewModel(Terrain terrain)
        {
            TerrainId = terrain.TerrainId;
            Name = terrain.Name;
            DisplayColor = terrain.DisplayColor;
        }

    }
}