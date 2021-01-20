using Space101.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ModelLibrary.Space101;

namespace Space101.Models
{
    public class Race
    {
        public int RaceID { get; private set; }

        [DisplayName("Race Name")]
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Race Name must be between 2 and 50 characters")]
        public string Name { get; private set; }

        [DisplayName("Race Classification")]
        [Required(ErrorMessage = "Race Classification is Required")]
        public int RaceClassificationID { get; private set; }

        [DisplayName("Average Height (cm)")]
        [Required(ErrorMessage = "Average Height is Required!")]
        [Range(40, 600, ErrorMessage = "Average Height must be between 40cm and 600cm")]
        public int AverageHeight { get; private set; }

        //public ICollection<RaceUser> RaceUsers { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; private set; }

        public ICollection<RaceHabitat> RaceHabitats { get; private set; }

        public RaceClassification RaceClassification { get; private set; }

        protected Race()
        {
            ApplicationUsers = new List<ApplicationUser>();
            RaceHabitats = new List<RaceHabitat>();
        }

        private Race(RaceFormViewModel race, List<RaceHabitat> raceHabitats)
        {
            RaceID = race.ID;
            Name = race.Name;
            AverageHeight = race.AverageHeight;
            RaceClassificationID = race.RaceClassificationID;
            RaceHabitats = raceHabitats;
        }

        private void RaceUpdate(RaceFormViewModel race, List<RaceHabitat> raceHabitats)
        {
            RaceID = race.ID;
            Name = race.Name;
            AverageHeight = race.AverageHeight;
            RaceClassificationID = race.RaceClassificationID;
            RaceHabitats = raceHabitats;
        }

        public static Race RaceCreation(RaceFormViewModel race)
        {
            List<RaceHabitat> raceHabitats = new List<RaceHabitat>();

            race.Planets
                .Where(p => p.IsAssigned == true).ToList()
                .ForEach(p => raceHabitats.Add(RaceHabitat.RaceHabitatCreate(p.ID, race.ID)));

            return new Race(race, raceHabitats);
        }

        public void RaceUpdate(RaceFormViewModel race)
        {
            List<RaceHabitat> raceHabitats = new List<RaceHabitat>();

            race.Planets
                .Where(p => p.IsAssigned == true).ToList()
                .ForEach(p => raceHabitats.Add(RaceHabitat.RaceHabitatCreate(p.ID, race.ID)));

            RaceUpdate(race, raceHabitats);
        }

    }
}