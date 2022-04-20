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
    }
}
