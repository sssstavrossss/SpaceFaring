using System.Collections.Generic;

namespace Space101.Dtos
{
    public class RacePLANETDto
    {
        public int RaceID { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }

        public ICollection<RaceFileDto> Assets { get; set; }
    }
}