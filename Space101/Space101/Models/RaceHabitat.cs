using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class RaceHabitat
    {

        [Key, ForeignKey("Planet"), Column(Order = 1)]
        public int PlanetID { get; private set; }

        [Key, ForeignKey("Race"), Column(Order = 2)]
        public int RaceID { get; private set; }

        public Planet Planet { get; private set; }

        public Race Race { get; private set; }

        protected RaceHabitat() { }

        private RaceHabitat(int planetid, int raceid)
        {
            PlanetID = planetid;
            RaceID = raceid;
        }

        public static RaceHabitat RaceHabitatCreate(int planetID, int raceID)
        {
            return new RaceHabitat(planetID, raceID);
        }
    }
}