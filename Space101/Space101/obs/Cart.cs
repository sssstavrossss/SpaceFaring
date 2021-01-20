using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public class Cart
    {

        [ForeignKey("Departure")]
        [Index("UniqueRoutes", IsUnique = true, Order = 1)]
        [Display(Name = "Departure Planet")]
        public int DepartureId { get; private set; }

        public List<CartItem> Items = new List<CartItem>();
    }
}