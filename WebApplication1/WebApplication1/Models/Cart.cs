using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Cart
    {
        public phone phone { get; set; }
        public int Quantity { get; set; }

        public Cart(phone phone1, int quantity)
        {
            this.phone = phone1;
            this.Quantity = quantity;
        }
    }
}