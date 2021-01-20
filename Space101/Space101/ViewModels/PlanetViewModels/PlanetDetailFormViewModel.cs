using Space101.Enums;
using Space101.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.PlanetViewModels
{
    public class PlanetDetailFormViewModel
    {
        private const int unknownRotationPeriod = (int)InvalidPropertyValues.undefinedValue;
        private const int unknownOrbitalPeriod = (int)InvalidPropertyValues.undefinedValue;
        private const int unknownDiameter = (int)InvalidPropertyValues.undefinedValue;
        private const long unknownPopulation = (long)InvalidPropertyValues.undefinedValue;

        public int PlanetDetailFormViewModelId { get; set; }

        [Display(Name = "Rotation Period")]
        [DefaultValue(unknownRotationPeriod)]
        [Range(-1, int.MaxValue, ErrorMessage = "The Rotation Period must be an integer greater than 0, -1 for no info")]
        public int RotationPeriod { get; set; } = unknownRotationPeriod;

        [Display(Name = "Orbital Period")]
        [DefaultValue(unknownOrbitalPeriod)]
        [Range(-1, int.MaxValue, ErrorMessage = "The Orbital Period must be an integer greater than 0, -1 for no info")]
        public int OrbitalPeriod { get; set; } = unknownOrbitalPeriod;

        [DefaultValue(unknownDiameter)]
        [Range(-1, int.MaxValue, ErrorMessage = "The Diameter must be an integer greater than 0, -1 for no info")]
        public int Diameter { get; set; } = unknownDiameter;

        [DefaultValue(unknownPopulation)]
        [Range(-1L, long.MaxValue, ErrorMessage = "The Population must be an integer greater than 0, -1 for no info")]
        public long Population { get; set; } = unknownPopulation;

        public PlanetDetailFormViewModel()
        { }

        public PlanetDetailFormViewModel(PlanetDetail details)
        {
            if (details == null)
                return;
            PlanetDetailFormViewModelId = details.PlanetID;
            RotationPeriod = details.RotationPeriod;
            OrbitalPeriod = details.OrbitalPeriod;
            Diameter = details.Diameter;
            Population = details.Population;
        }

    }
}