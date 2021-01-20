using System;
using System.ComponentModel.DataAnnotations;

namespace Space101.Models
{
    public class Starship
    {
        public int StarshipId { get; private set; }

        [Required(ErrorMessage = "The Model is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Model must have 3 to 50 letters")]
        public string Model { get; private set; }

        [Required(ErrorMessage = "The Manufacturer is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Manufacturer must have 3 to 50 letters")]
        public string Manufacturer { get; private set; }

        [Required(ErrorMessage = "The Passenger Capacity is required")]
        [Display(Name = "Passenger Capacity")]
        [Range(0, int.MaxValue, ErrorMessage = "The Passenger Capacity must be a number greater than 0")]
        public int PassengerCapacity { get; private set; }

        [Required(ErrorMessage = "The Cargo Capacity is required")]
        [Display(Name = "Cargo Capacity")]
        [Range(0, int.MaxValue, ErrorMessage = "The Cargo Capacity must be a number greater than 0")]
        public int CargoCapacity { get; private set; }

        [Required(ErrorMessage = "The Length Capacity is required")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Length { get; private set; }

        protected Starship()
        { }

        public Starship(string model, string manufacturer, int passengerCapacity, int cargoCapacity, double length)
        {
            Model = model;
            Manufacturer = manufacturer;
            PassengerCapacity = passengerCapacity;
            CargoCapacity = cargoCapacity;
            Length = length;
        }

        public void Update(string model = null, string manufacturer = null, int? passengerCapacity = null, int? cargoCapacity = null, double? length = null)
        {
            Model = string.IsNullOrWhiteSpace(model) ? Model : model;
            Manufacturer = string.IsNullOrWhiteSpace(manufacturer) ? Manufacturer : manufacturer;
            PassengerCapacity = passengerCapacity ?? PassengerCapacity;
            CargoCapacity = cargoCapacity ?? CargoCapacity;
            Length = length ?? Length;
        }

    }
}