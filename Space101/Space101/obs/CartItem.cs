using ModelLibrary.Space101.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Space101.Models
{
    public class CartItem
    {

        public Item Item { get; private set; }

        public int Quantity { get; private set; }

    }
}