namespace Space101.Dtos.BatabaseStatisticsDtos
{
    public class FlightsPerStatusDto
    {
        public string StatusName { get; set; }
        public int FlightsNumber { get; set; }

        public FlightsPerStatusDto()
        { }

        public FlightsPerStatusDto(string statusName, int flightsNumber)
        {
            StatusName = statusName;
            FlightsNumber = flightsNumber;
        }
    }
}