using Space101.Enums;
using Space101.ViewModels.PlanetViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class PlanetDetail
    {
        private const int unknownRotationPeriod = (int)InvalidPropertyValues.undefinedValue;
        private const int unknownOrbitalPeriod = (int)InvalidPropertyValues.undefinedValue;
        private const int unknownDiameter = (int)InvalidPropertyValues.undefinedValue;
        private const long unknownPopulation = (long)InvalidPropertyValues.undefinedValue;

        [Key]
        [ForeignKey("Planet")]
        public int PlanetID { get; private set; }
        public Planet Planet { get; private set; }

        [Display(Name = "Rotation Period")]
        [DefaultValue(unknownRotationPeriod)]
        [Range(unknownRotationPeriod, int.MaxValue, ErrorMessage = "The Rotation Period must be an integer greater than 0, -1 for no info")]
        public int RotationPeriod { get; private set; } = unknownRotationPeriod;

        [Display(Name = "Orbital Period")]
        [DefaultValue(unknownOrbitalPeriod)]
        [Range(unknownOrbitalPeriod, int.MaxValue, ErrorMessage = "The Orbital Period must be an integer greater than 0, -1 for no info")]
        public int OrbitalPeriod { get; private set; }= unknownOrbitalPeriod;

        [DefaultValue(unknownDiameter)]
        [Range(unknownDiameter, int.MaxValue, ErrorMessage = "The Diameter must be an integer greater than 0, -1 for no info")]
        public int Diameter { get; private set; } = unknownDiameter;

        [DefaultValue(unknownPopulation)]
        [Range(unknownPopulation, long.MaxValue, ErrorMessage = "The Population must be an integer greater than 0, -1 for no info")]
        public long Population { get; private set; } = unknownPopulation;

        protected PlanetDetail()
        { }

        private PlanetDetail(int rotationPeriod, int orbitalPeriod, int diameter, long population, Planet planet)
        {
            if (planet == null)
                throw new ArgumentNullException("Planet");
            RotationPeriod = rotationPeriod;
            OrbitalPeriod = orbitalPeriod;
            Diameter = diameter;
            Population = population;
            PlanetID = IsEmpty() ? (int)InvalidPropertyValues.undefinedValue : planet.PlanetID;
        }

        private PlanetDetail(PlanetDetailFormViewModel detailFormViewModel, PlanetFormViewModel planetFormViewModel)
        {
            if (planetFormViewModel == null)
                throw new ArgumentNullException("Planet View Model");
            RotationPeriod = detailFormViewModel.RotationPeriod;
            OrbitalPeriod = detailFormViewModel.OrbitalPeriod;
            Diameter = detailFormViewModel.Diameter;
            Population = detailFormViewModel.Population;
            PlanetID = IsEmpty() ? (int)InvalidPropertyValues.undefinedValue : planetFormViewModel.PlanetFormViewModelId;
        }

        public static PlanetDetail Create(int rotationPeriod, int orbitalPeriod, int diameter, long population, Planet planet)
        {
            return new PlanetDetail(rotationPeriod, orbitalPeriod, diameter, population, planet);
        }

        public static  PlanetDetail CreateFromFormViewModel(PlanetDetailFormViewModel detailFormViewModel, PlanetFormViewModel planetFormViewModel)
        {
            return new PlanetDetail(detailFormViewModel, planetFormViewModel);
        }

        private bool IsEmpty()
        {
            bool result = false;
            if (RotationPeriod == unknownRotationPeriod && OrbitalPeriod == unknownOrbitalPeriod && Diameter == unknownDiameter && Population == unknownPopulation)
                result = true;
            return result;
        }

    }
}