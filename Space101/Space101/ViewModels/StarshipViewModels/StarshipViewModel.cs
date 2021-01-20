using Space101.Models;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.StarshipViewModels
{
    public class StarshipViewModel
    {
        [Display(Name = "Starship Id")]
        public int StarshipId { get; private set; }
        public string Model { get; private set; }
        public string Manufacturer { get; private set; }

        [Display(Name = "Passenger Capacity")]
        public int PassengerCapacity { get; private set; }

        [Display(Name = "Cargo Capacity")]
        public int CargoCapacity { get; private set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Length { get; private set; }

        public StarshipViewModel(Starship starship)
        {
            StarshipId = starship.StarshipId;
            Model = starship.Model;
            Manufacturer = starship.Manufacturer;
            PassengerCapacity = starship.PassengerCapacity;
            CargoCapacity = starship.CargoCapacity;
            Length = starship.Length;
        }
    }
}