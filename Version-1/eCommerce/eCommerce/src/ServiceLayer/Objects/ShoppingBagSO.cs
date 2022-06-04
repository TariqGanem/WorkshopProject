using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class ShoppingBagSO
    {
        #region parameters
        public string Id { get; }
        public string UserId { get; }
        public string StoreId { get; }
        public Dictionary<ProductService, int> Products { get; }
        public double TotalPrice { get; }
        #endregion

        #region constructors

        public ShoppingBagSO(ShoppingBag shoppingBag)
        {
            this.Id = shoppingBag.Id;
            this.UserId = shoppingBag.UserId;
            this.StoreId = shoppingBag.Store.Id;
            this.TotalPrice = shoppingBag.TotalBagPrice;
            this.Products = new Dictionary<ProductService, int>();
            foreach (Product p in shoppingBag.Products.Keys)
            {
                ProductService new_p = new ProductService(p.Id, p.Name, p.Price, p.Quantity, p.Category);
                this.Products[new_p] = shoppingBag.Products[p];
            }
        }

        public ShoppingBagSO(string id, string userId, string storeId, Dictionary<ProductService, int> products, double totalBagPrice)
        {
            Id = id;
            UserId = userId;
            StoreId = storeId;
            this.Products = products;
            this.TotalPrice = totalBagPrice;
        }
        #endregion
    }
}