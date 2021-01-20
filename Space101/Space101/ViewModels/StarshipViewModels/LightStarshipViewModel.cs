using Space101.Models;

namespace Space101.ViewModels.StarshipViewModels
{
    public class LightStarshipViewModel
    {
        public int StarshipId { get; set; }
        public string Name { get; set; }

        public LightStarshipViewModel(Starship starship)
        {
            StarshipId = starship.StarshipId;
            Name = starship.Model;
        }
    }
}