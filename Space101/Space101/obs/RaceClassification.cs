using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public class RaceClassification
    {
        public int RaceClassificationID { get; private set; }

        [DisplayName("Classification name")]
        [Required(ErrorMessage = "Classification name is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Classification name must be between 2 and 50 characters")]
        public string Name { get; private set; }

        protected RaceClassification() { }

        private RaceClassification(string name)
        {
            Name = name;
        }

        public RaceClassification RaceClassificationCreate(string name)
        {
            return new RaceClassification(name);
        }

    }
}