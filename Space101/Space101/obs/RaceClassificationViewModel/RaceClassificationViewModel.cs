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
    public class RaceClassificationViewModel
    {
        public int RaceClassificationID { get; set; }

        [DisplayName("Classification name")]
        [Required(ErrorMessage = "Classification name is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Classification name must be between 2 and 50 characters")]
        public string Name { get; set; }

        protected RaceClassificationViewModel() { }

        private RaceClassificationViewModel(int id, string name)
        {
            RaceClassificationID = id;
            Name = name;
        }

        public static RaceClassificationViewModel RaceClassificationViewModelCreation(RaceClassification raceClassification)
        {
            return new RaceClassificationViewModel(raceClassification.RaceClassificationID, raceClassification.Name);
        }

        public static RaceClassificationViewModel RaceClassificationViewModelCreation(int id, string name)
        {
            return new RaceClassificationViewModel(id, name);
        }

    }
}