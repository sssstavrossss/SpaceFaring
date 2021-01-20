using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Space101.Models
{
    public class Terrain
    {
        public int TerrainId { get; private set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must have 3 to 50 letters")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Color is required")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Color must have 4 to 50 letters")]
        public string DisplayColor { get; private set; }

        public ICollection<SurfaceMorphology> SurfaceMorphologies { get; private set; }

        protected Terrain()
        {
            SurfaceMorphologies = new List<SurfaceMorphology>();
        }

        public Terrain(string name, string colorCode)
        {
            Name = name;
            DisplayColor = colorCode;
            SurfaceMorphologies = new List<SurfaceMorphology>();
        }

        public void Update(string name, string colorCode)
        {
            Name = string.IsNullOrWhiteSpace(name) ? Name : name;
            DisplayColor = string.IsNullOrWhiteSpace(colorCode) ? DisplayColor : colorCode;
        }

    }
}