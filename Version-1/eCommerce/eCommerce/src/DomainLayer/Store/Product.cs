using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using eCommerce.src.ServiceLayer.Response;

namespace eCommerce.src.DomainLayer.Store
{
    public class Product
    {
        public String Id { get; }
        public String Name { get; set; }
        public double Price { get; set; } 
        public int Quantity { get; set; }
        public String Category { get; set; }
        public Double Rating { get; set; }
        public int AmountOfRate { get; set; }

        public Product(string id, string name, double price, int quantity, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity < 0 ? 0 : quantity;
            Category = category;
            Rating = 0;
            AmountOfRate = 0;
        }

        public void PickProduct(int quantity)
        {
            if (quantity > this.Quantity)
                throw new Exception($"There is no enough quantity of {this.Name}");
            this.Quantity -= quantity;
        }




    }
}
