using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public class Item
    {
        public int ItemID { get; private set; }

        [DisplayName("Item Name")]
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Item Name must be between 2 and 50 characters")]
        public string Name { get; private set; }

        [DisplayName("Item Price")]
        [Required(ErrorMessage = "Price is Required!")]
        public int Price { get; private set; }

        public int? Discount { get; private set; }

        protected Item() { }

        private Item(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public static Item NewItem(string name, int price)
        {
            return new Item(name, price);
        }
    }
}