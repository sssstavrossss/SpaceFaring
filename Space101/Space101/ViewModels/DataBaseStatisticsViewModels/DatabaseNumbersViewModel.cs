using System;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.DataBaseStatisticsViewModels
{
    public class DatabaseNumbersViewModel
    {
        public DateTime DatabaseTimeStamp { get; set; }
        public int Planets { get; set; }
        public int Flightpaths { get; set; }
        public long Flights { get; set; }
        public int Starships { get; set; }
        public int Climates { get; set; }
        public int Terrains { get; set; }
        public int Races { get; set; }

        [Display(Name = "Race Classifications")]
        public int RaceClassification { get; set; }
        public long Users { get; set; }
        public long Orders { get; set; }
        public long Tickets { get; set; }
        public decimal Revenue { get; set; }

        public DatabaseNumbersViewModel()
        { }
    }
}