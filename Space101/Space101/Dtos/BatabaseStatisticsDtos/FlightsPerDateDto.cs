using System;

namespace Space101.Dtos.BatabaseStatisticsDtos
{
    public class FlightsPerDateDto
    {
        public string Date { get; set; }
        public int FlightsNumber { get; set; }

        public FlightsPerDateDto()
        { }

        public FlightsPerDateDto(DateTime date, int flightsNumber)
        {
            Date = date.Date.ToString("d");
            FlightsNumber = flightsNumber;
        }
    }
}