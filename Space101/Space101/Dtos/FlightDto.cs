using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class FlightDto
    {
        public int FlightId { get; set; }
        public DateTime Date { get; set; }
        public string FlightDate
        {
            get 
            {
                return Date.Date.ToString("d");
            }
        }
        public string FlightTime
        {
            get
            {
                return Date.TimeOfDay.ToString("T");
            }
        }
        public string BasePriceFormatted
        {
            get
            {
                return BasePrice.ToString("N2");
            }
        }
        public string DistanceFormatted
        {
            get
            {
                return FlightPath.Distance.ToString("N2");
            }
        }
        public string DateString { get; set; }
        public int DateDiff { get; set; }
        public decimal BasePrice { get; set; }
        public FlightPathDto FlightPath { get; set; }
        public FlightStatusDto FlightStatus { get; set; }
        public StarshipDto Starship { get; set; }
        public bool IsVIP { get; set; }
        public ICollection<FlightStatusDto> FlightStatuses { get; set; }

        public FlightDto() 
        {  }

        public void ConvertDate()
        {
            DateString = Convert.ToDateTime(Date).ToString("MM/dd/yyyy HH:mm:ss");
            DateDiff = Date.DayOfYear - DateTime.Now.DayOfYear;
            //DisplayDateOnly = Convert.ToDateTime(Date).ToString("dd/MM/yyyy");
        }

    }
}