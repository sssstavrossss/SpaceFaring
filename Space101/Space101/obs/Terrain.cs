using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public class Terrain
    {
        public int TerrainId { get; private set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must have 3 to 50 letters")]
        public string Name { get; private set; }

        protected Terrain()
        { }

        public Terrain(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? Name : name;
        }

    }
}