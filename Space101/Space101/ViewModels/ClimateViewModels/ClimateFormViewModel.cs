using Space101.Controllers;
using Space101.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Space101.ViewModels.ClimateViewModels
{
    public class ClimateFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name must have 3 to 50 letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Color is required")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Color must have 4 to 50 letters")]
        public string Color { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ClimateController,ActionResult>> newAction = (c => c.New());
                Expression<Func<ClimateController,ActionResult>> editAction = (c => c.Edit(this.Id));

                var action = (this.Id != 0) ? editAction : newAction;
                var actionName = (action.Body as MethodCallExpression).Method.Name;

                return actionName;
            }
        }

        public ClimateFormViewModel()
        { }

        public ClimateFormViewModel(Climate climate)
        {
            if (climate == null)
                return;
            Id = climate.ClimateId;
            Name = climate.Name;
            Color = climate.DisplayColor;
        }
    }
}