namespace Space101.ViewModels.PlanetFileViewModels
{
    public class PlanetFileFormViewModel
    {
        public int PlanetFileId { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public bool DeleteIt { get; set; }

        public PlanetFileFormViewModel()
        { }

        public PlanetFileFormViewModel(int planetFileId, string path, string type,bool deleteIt)
        {
            PlanetFileId = planetFileId;
            Path = path;
            Type = type;
            DeleteIt = deleteIt;
        }
    }
}