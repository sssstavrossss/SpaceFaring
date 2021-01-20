namespace Space101.ViewModels.PlanetFileViewModels
{
    public class DisplayPlanetFileViewModel
    {
        public string Path { get; set; }
        public string Type { get; set; }

        public DisplayPlanetFileViewModel()
        { }

        public DisplayPlanetFileViewModel(string path, string type)
        {
            Path = path;
            Type = type;
        }
    }
}