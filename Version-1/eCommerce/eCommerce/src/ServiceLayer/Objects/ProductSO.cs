using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class ProductSO
    {
        #region parameters
        public string Id { get; }
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; }
        public string Category { get; }
        #endregion

        #region constructors
        public ProductSO(string id, string name, double price, int quantity, string category)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
            this.Category = category;
        }
      
        public ProductSO(Product p)
        {
            this.Id = p.Id;
            this.Name = p.Name;
            this.Price = p.Price;
            this.Quantity = p.Quantity;
            this.Category = p.Category;
        }
        #endregion
    }
}
