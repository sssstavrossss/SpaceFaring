using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Space101.Models
{
    public class FlightStatus
    {
        public byte FlightStatusId { get; private set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; private set; }

        public ICollection<Flight> Flights { get; private set; }

        protected FlightStatus()
        { }

        public FlightStatus(string statusName)
        {
            StatusName = statusName;
        }
    }
}