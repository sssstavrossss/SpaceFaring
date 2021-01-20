using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Space101.Models
{
    public class Climate
    {
        public int ClimateId { get; private set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must have 3 to 50 letters")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Color is required")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Color must have 4 to 50 letters")]
        public string DisplayColor { get; private set; }

        public ICollection<ClimateZone> ClimateZones { get; private set; }

        protected Climate()
        {
            ClimateZones = new List<ClimateZone>();
        }

        public Climate(string name, string colorCode)
        {
            Name = name;
            DisplayColor = colorCode;
            ClimateZones = new List<ClimateZone>();
        }

        public void Update(string name, string colorCode)
        {
            Name = string.IsNullOrWhiteSpace(name) ? Name : name;
            DisplayColor = string.IsNullOrWhiteSpace(colorCode) ? DisplayColor : colorCode;
        }
    }
}