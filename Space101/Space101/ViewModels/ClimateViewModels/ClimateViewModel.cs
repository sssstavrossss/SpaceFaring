using Space101.Models;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.ClimateViewModels
{
    public class ClimateViewModel
    {
        [Display(Name = "Climate Id")]
        public int ClimateId { get; private set; }
        public string Name { get; private set; }

        [Display(Name = "Display Color")]
        public string DisplayColor { get; set; }

        public ClimateViewModel(Climate climate)
        {
            ClimateId = climate.ClimateId;
            Name = climate.Name;
            DisplayColor = climate.DisplayColor;
        }
    }
}