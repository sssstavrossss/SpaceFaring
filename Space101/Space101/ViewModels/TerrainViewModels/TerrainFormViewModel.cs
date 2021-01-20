using Space101.Controllers;
using Space101.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Space101.ViewModels.TerrainViewModels
{
    public class TerrainFormViewModel
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
                Expression<Func<ClimateController, ActionResult>> newAction = (t => t.New());
                Expression<Func<ClimateController, ActionResult>> editAction = (t => t.Edit(this.Id));

                var action = (this.Id != 0) ? editAction : newAction;
                var actionName = (action.Body as MethodCallExpression).Method.Name;

                return actionName;
            }
        }

        public TerrainFormViewModel()
        { }

        public TerrainFormViewModel(Terrain terrain)
        {
            if (terrain == null)
                return;
            Id = terrain.TerrainId;
            Name = terrain.Name;
            Color = terrain.DisplayColor;
        }
    }
}