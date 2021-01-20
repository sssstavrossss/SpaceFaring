using System.Collections.Generic;

namespace Space101.Dtos
{
    public class RaceDto
    {
        public int RaceID { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public int AverageHeight { get; set; }

        public ICollection<RaceHabitatPLANETDto> RaceHabitats { get; set; }
        public RaceClassificationsDto RaceClassification { get; set; }
        public ICollection<RaceFileDto> Assets { get; set; }
    }
}