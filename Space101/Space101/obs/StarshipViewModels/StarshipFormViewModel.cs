using Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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

        //It is used when we want to have access only in runtime -> It throws an error in compile time
        //[Obsolete("For Model binding only",true)]
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

    //Locked StarshipFormViewModel - To work it needs a custom model binder
    //public class StarshipFormViewModel
    //{
    //    public int Id { get; private set; }

    //    [Required(ErrorMessage = "The Model is required")]
    //    [StringLength(100, MinimumLength = 3, ErrorMessage = "The Model must have 3 to 50 letters")]
    //    public string Model { get; private set; }

    //    [Required(ErrorMessage = "The Manufacturer is required")]
    //    [StringLength(100, MinimumLength = 3, ErrorMessage = "The Manufacturer must have 3 to 50 letters")]
    //    public string Manufacturer { get; private set; }

    //    [Required(ErrorMessage = "The Passenger Capacity is required")]
    //    [Display(Name = "Passenger Capacity")]
    //    [Range(0, int.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
    //    public int PassengerCapacity { get; private set; }

    //    [Required(ErrorMessage = "The Cargo Capacity is required")]
    //    [Display(Name = "Cargo Capacity")]
    //    [Range(0, int.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
    //    public int CargoCapacity { get; private set; }

    //    [Required(ErrorMessage = "The Length is required")]
    //    [Range(0.0, Double.MaxValue, ErrorMessage = "The Length must be a number greater than 0")]
    //    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    //    public double Length { get; private set; }

    //    //[Obsolete("For Model binding only",true)]
    //    protected StarshipFormViewModel()
    //    { }

    //    public StarshipFormViewModel(Starship starship, int? id = null)
    //    {
    //        Id = id ?? starship.StarshipId;
    //        Model = starship.Model;
    //        Manufacturer = starship.Manufacturer;
    //        PassengerCapacity = starship.PassengerCapacity;
    //        CargoCapacity = starship.CargoCapacity;
    //        Length = starship.Length;
    //    }

    //}

    //Custom Model Binder for Locked StarshipFormViewModel
    //public class TestBinder : DefaultModelBinder
    //{
    //    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {

    //        if (bindingContext.ModelType == typeof(StarshipFormViewModel))
    //        {
    //            HttpRequestBase request = controllerContext.HttpContext.Request;

    //            int frmStarshipId = request.Form.Get("Id").ToString().Equals(string.Empty) ? 0 : Convert.ToInt32(request.Form.Get("Id"));
    //            string frmModel = request.Form.Get("Model").ToString();
    //            string frmManufacturer = request.Form.Get("Manufacturer").ToString();
    //            int frmPassengerCapacity = Convert.ToInt32(request.Form.Get("PassengerCapacity"));
    //            int frmCargoCapacity = Convert.ToInt32(request.Form.Get("CargoCapacity"));
    //            double frmLength = Convert.ToDouble(request.Form.Get("Length"));

    //            var starship = new Starship(frmModel, frmManufacturer, frmPassengerCapacity, frmCargoCapacity, frmLength);

    //            return new StarshipFormViewModel(starship, frmStarshipId);
    //        }
    //        else
    //        {
    //            return base.BindModel(controllerContext, bindingContext);
    //        }
    //    }
    //}

}