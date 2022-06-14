using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class ProductService
    {
        public string Id { get; }
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; }
        public string Category { get; }
    
        public ProductService(string id, string name, double price, int quantity, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        // del me l8r
        public string[] ToStringArray()
        {
            string[] output = new string[5];
            output[0] = Id;
            output[1] = Name;
            output[2] = Price.ToString();
            output[3] = Quantity.ToString();
            output[4] = Category;
            return output;
        }
    }
}
