using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Space101.Models
{
    public class TravelClass
    {
        [Key]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "The ID must have 1 to 10 letters")]
        public string ClassId { get; private set; }
        public float BasePriceRate { get; private set; }

        public ICollection<FlightSeat> FlightSeats;

        protected TravelClass()
        { }

        public TravelClass(string classId)
        {
            switch (classId)
            {
                case "A":
                    BasePriceRate = 400 / 100;
                    ClassId = "A";
                    break;
                case "B":
                    BasePriceRate = 300 / 100;
                    ClassId = "B";
                    break;
                case "C":
                    BasePriceRate = 200 / 100;
                    ClassId = "C";
                    break;
                case "D":
                    BasePriceRate = 100 / 100;
                    ClassId = "D";
                    break;
                case "E":
                    BasePriceRate = 80 / 100;
                    ClassId = "E";
                    break;
                default:
                    BasePriceRate = 100 / 100;
                    ClassId = "D";
                    break;
            }
        }
    }
}