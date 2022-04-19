using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;

namespace eCommerce.src.ServiceLayer.Objects
{
    public class shoppingBasket
    {
        #region parameters
        public string Id { get; }
        public string UserId { get; }
        public string StoreId { get; }
        public Dictionary<product, int> Products { get; }
        public double TotalPrice { get; }
        #endregion

        #region constructors
        public shoppingBasket(string id, string userId, string storeId, Dictionary<product, int> products, double totalPrice)
        {
            this.Id = id;
            this.UserId = userId;
            this.StoreId = storeId;
            this.Products = products;
            this.TotalPrice = totalPrice;
        }

        public shoppingBasket(ShoppingBag shoppingBag)
        {
            this.Id = shoppingBag.Id;
            this.UserId = shoppingBag.UserId;
            this.StoreId = shoppingBag.Store.Id;
            this.TotalPrice = shoppingBag.TotalBagPrice;
            this.Products = new Dictionary<product, int>();
            foreach (Product p in shoppingBag.Products.Keys)
            {
                product new_p = new product(p);
                this.Products[new_p] = shoppingBag.Products[p];
            }
        }
        #endregion
    }
}