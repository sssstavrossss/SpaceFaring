using Space101.Controllers;
using Space101.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Space101.ViewModels.StarShipViewModels
{
    public class StarshipFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Model is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Model must have 3 to 50 letters")]
        public string Model { get; set; }

        [Required(ErrorMessage = "The Manufacturer is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The Manufacturer must have 3 to 50 letters")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "The Passenger Capacity is required")]
        [Display(Name = "Passenger Capacity")]
        [Range(0, int.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
        public int PassengerCapacity { get; set; }

        [Required(ErrorMessage = "The Cargo Capacity is required")]
        [Display(Name = "Cargo Capacity")]
        [Range(0, int.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
        public int CargoCapacity { get; set; }

        [Required(ErrorMessage = "The Length is required")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Length { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ClimateController, ActionResult>> newAction = (s => s.New());
                Expression<Func<ClimateController, ActionResult>> editAction = (s => s.Edit(this.Id));

                var action = (this.Id != 0) ? editAction : newAction;
                var actionName = (action.Body as MethodCallExpression).Method.Name;

                return actionName;
            }
        }

        public StarshipFormViewModel()
        { }

        public StarshipFormViewModel(Starship starship)
        {
            if (starship == null)
                return;
            Id = starship.StarshipId;
            Model = starship.Model;
            Manufacturer = starship.Manufacturer;
            PassengerCapacity = starship.PassengerCapacity;
            CargoCapacity = starship.CargoCapacity;
            Length = starship.Length;
        }

    }
}