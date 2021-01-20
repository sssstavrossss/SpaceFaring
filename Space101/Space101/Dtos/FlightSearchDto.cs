using System;

namespace Space101.Dtos
{
    public class FlightSearchDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string DeparturePlanet { get; set; }
        public decimal MaxPrice { get; set; }
        public string DestinationPlanet { get; set; }
        public decimal MinPrice { get; set; }
        public bool VipBool { get; set; }
        public bool NoVipBool { get; set; }

        public void FixDates()
        {
            if (DateFrom < DateTime.Now)
                DateFrom = DateTime.Now;
            if (DateTo < DateFrom)
                DateTo = DateFrom.AddDays(180);
        }

    }
}