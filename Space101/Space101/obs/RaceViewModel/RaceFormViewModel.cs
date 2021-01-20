using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ModelLibrary.Space101;

namespace Space101.ViewModels
{
    public class RaceFormViewModel
    {
        public int ID { get; set; }

        [DisplayName("Race Name")]
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Race Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        [DisplayName("Race Classification")]
        [Required(ErrorMessage = "Race Classification is Required")]
        public int RaceClassificationID { get; set; }

        [DisplayName("Average Height (cm)")]
        [Required(ErrorMessage = "Average Height is Required!")]
        [Range(40, 600, ErrorMessage = "Average Height must be between 40cm and 600cm")]
        public int AverageHeight { get; set; }

        [DisplayName("Check Planet Habitats")]
        public List<RaceHabitatViewModel> Planets { get; set; }

        public List<RaceClassificationViewModel> RaceClassificationList { get; set; }

        public RaceClassificationViewModel RaceClassification { get; set; }

        public RaceFormViewModel() { }

        private RaceFormViewModel(List<RaceHabitatViewModel> planets, List<RaceClassificationViewModel> raceClassifications)
        {
            Planets = planets;
            RaceClassificationList = raceClassifications;
        }

        private RaceFormViewModel
            (Race race, List<RaceHabitatViewModel> planets, List<RaceClassificationViewModel> raceClassifications)
        {
            ID = race.RaceID;
            Planets = planets;
            RaceClassificationList = raceClassifications;
            Name = race.Name;
            AverageHeight = race.AverageHeight;
            RaceClassificationID = race.RaceClassificationID;
        }

        private RaceFormViewModel(RaceFormViewModel viewmodel, List<RaceClassificationViewModel> raceClassifications)
        {
            ID = viewmodel.ID;
            Planets = viewmodel.Planets;
            RaceClassificationList = raceClassifications;
            RaceClassificationID = viewmodel.RaceClassificationID;
            Name = viewmodel.Name;
            AverageHeight = viewmodel.AverageHeight;
        }

        public static RaceFormViewModel RaceFormViewModelCreationNew(List<Planet> planetsAll, List<RaceClassification> raceClassificationList)
        {
            List<RaceHabitatViewModel> racePlanets = new List<RaceHabitatViewModel>();
            List<RaceClassificationViewModel> raceClassifications = new List<RaceClassificationViewModel>();

            planetsAll.ForEach(p => racePlanets
                .Add(RaceHabitatViewModel.RaceHabitatViewModelCreation(p, false)));

            raceClassificationList
                .ForEach(rc => raceClassifications.Add(RaceClassificationViewModel.RaceClassificationViewModelCreation(rc)));

            return new RaceFormViewModel(racePlanets, raceClassifications);
        }

        public static RaceFormViewModel RaceFormViewModelEdit
            (Race race, List<Planet> planetsAll, List<RaceClassification> raceClassificationList)
        {
            List<RaceHabitatViewModel> racePlanets = new List<RaceHabitatViewModel>();
            List<RaceClassificationViewModel> raceClassifications = new List<RaceClassificationViewModel>();

            planetsAll.ForEach(p => racePlanets
                .Add(RaceHabitatViewModel.RaceHabitatViewModelCreation(p, race.RaceHabitats.Select(rh => rh.Planet).Contains(p))));

            raceClassificationList
                .ForEach(rc => raceClassifications.Add(RaceClassificationViewModel.RaceClassificationViewModelCreation(rc)));

            return new RaceFormViewModel(race, racePlanets, raceClassifications);
        }

        public static RaceFormViewModel RaceFormViewModelValidate(RaceFormViewModel viewmodel, List<RaceClassification> raceClassification)
        {
            List<RaceClassificationViewModel> raceClassifications = new List<RaceClassificationViewModel>();

            raceClassification
                .ForEach(rc => raceClassifications.Add(RaceClassificationViewModel.RaceClassificationViewModelCreation(rc)));

            return new RaceFormViewModel(viewmodel, raceClassifications);
        }

    }
}