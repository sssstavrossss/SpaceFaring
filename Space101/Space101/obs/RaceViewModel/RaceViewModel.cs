using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Space101.ViewModels
{
    public class RaceViewModel
    {
        public int ID { get; set; }


        [DisplayName("Race Name")]
        public string Name { get; set; }


        [DisplayName("Average Height (cm)")]
        public int AverageHeight { get; set; }


        [DisplayName("Check Planet Habitats")]
        public List<RaceHabitatViewModel> Planets { get; set; }


        [DisplayName("Race Classification")]
        public RaceClassificationViewModel RaceClassification { get; set; }

        public RaceViewModel() { }

        private RaceViewModel(RaceClassificationViewModel raceClassification, string name, 
            int averageHeight, List<RaceHabitatViewModel> planets, int id)
        {
            ID = id;
            RaceClassification = raceClassification;
            Name = name;
            AverageHeight = averageHeight;
            Planets = planets;
        }

        public static RaceViewModel RaceViewModelCreation(Race race)
        {
            List<RaceHabitatViewModel> racePlanets = new List<RaceHabitatViewModel>();

            var planets = race.RaceHabitats.Select(rh => rh.Planet).ToList();

            planets.ForEach(p => racePlanets
                .Add(RaceHabitatViewModel.RaceHabitatViewModelCreation(p, true)));

            var raceClassification = RaceClassificationViewModel.RaceClassificationViewModelCreation(race.RaceClassification);

            return new RaceViewModel(raceClassification, race.Name, race.AverageHeight, racePlanets, race.RaceID);
        }

    }
}