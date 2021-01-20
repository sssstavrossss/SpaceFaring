namespace Space101.Dtos.BatabaseStatisticsDtos
{
    public class PlanetsPerClimateDto
    {
            public string ClimateName { get; set; }
            public int NumberOfPlanets { get; set; }
            public string DisplayColor { get; set; }

            public PlanetsPerClimateDto()
            { }
            public PlanetsPerClimateDto(string climateName, int numberOfPlanets, string displayColor)
            {
                ClimateName = climateName;
                NumberOfPlanets = numberOfPlanets;
                DisplayColor = displayColor;
            }
    }
}