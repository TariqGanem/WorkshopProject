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
        public Dictionary<ProductSO, int> Products { get; }
        public double TotalPrice { get; }
        #endregion

        #region constructors
        public ShoppingBagSO(string id, string userId, string storeId, Dictionary<ProductSO, int> products, double totalPrice)
        {
            this.Id = id;
            this.UserId = userId;
            this.StoreId = storeId;
            this.Products = products;
            this.TotalPrice = totalPrice;
        }

        public ShoppingBagSO(ShoppingBag shoppingBag)
        {
            this.Id = shoppingBag.Id;
            this.UserId = shoppingBag.UserId;
            this.StoreId = shoppingBag.Store.Id;
            this.TotalPrice = shoppingBag.TotalBagPrice;
            this.Products = new Dictionary<ProductSO, int>();
            foreach (Product p in shoppingBag.Products.Keys)
            {
                ProductSO new_p = new ProductSO(p);
                this.Products[new_p] = shoppingBag.Products[p];
            }
        }
        #endregion
    }
}