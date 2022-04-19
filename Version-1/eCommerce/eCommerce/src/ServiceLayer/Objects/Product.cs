using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    internal class product
    {
        public string id { get; }
        public string name { get; }
        public double price { get; }
        public int quantity { get; }
        public string category { get; }

        public product(string id, string name, double price, int quantity, string category)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.quantity = quantity;
            this.category = category;
        }
    }
}
