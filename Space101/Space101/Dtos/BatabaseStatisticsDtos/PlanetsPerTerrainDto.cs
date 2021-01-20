namespace Space101.Dtos.BatabaseStatisticsDtos
{
    public class PlanetsPerTerrainDto
    {
        public string TerrainName { get; set; }
        public int NumberOfPlanets { get; set; }
        public string DisplayColor { get; set; }

        public PlanetsPerTerrainDto()
        { }
        public PlanetsPerTerrainDto(string terrainName, int numberOfPlanets, string displayColor)
        {
            TerrainName = terrainName;
            NumberOfPlanets = numberOfPlanets;
            DisplayColor = displayColor;
        }
    }
}