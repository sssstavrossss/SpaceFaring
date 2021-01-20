using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.ViewModels.ClimateViewModels
{
    public class ClimateFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must have 3 to 50 letters")]
        public string Name { get; set; }

        public ClimateFormViewModel()
        { }

        public ClimateFormViewModel(Climate climate)
        {
            if (climate == null)
                return;
            Id = climate.ClimateId;
            Name = climate.Name;
        }
    }
}