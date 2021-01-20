
namespace Space101.ViewModels.ClimateZoneViewModels
{
    public class ClimateZoneViewModel
    {
        public int ClimateId { get; set; }
        public string ClimateName { get; set; }
        public bool PlanetHasClimate { get; set; }

        public ClimateZoneViewModel()
        { }

        public ClimateZoneViewModel(int climateId, string climateName, bool planetHasIt)
        {
            ClimateId = climateId;
            ClimateName = climateName;
            PlanetHasClimate = planetHasIt;
        }



    }
}