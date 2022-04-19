using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using System;
using System.Collections.Generic;

namespace eCommerce.src.ServiceLayer.Objects
{
    internal class shoppingBasket
    {
        #region parameters
        public string id { get; }
        public string userId { get; }
        public string storeId { get; }
        public Dictionary<product, int> products { get; }
        public double totalPrice { get; }
        #endregion

        #region constructors
        public shoppingBasket(string id, string userId, string storeId, Dictionary<product, int> products, double totalPrice)
        {
            this.id = id;
            this.userId = userId;
            this.storeId = storeId;
            this.products = products;
            this.totalPrice = totalPrice;
        }

        public shoppingBasket(ShoppingBag shoppingBag)
        {
            this.id = shoppingBag.Id;
            this.userId = shoppingBag.UserId;
            this.storeId = shoppingBag.Store.Id;
            this.totalPrice = shoppingBag.TotalBagPrice;
            this.products = new Dictionary<product, int>();
            foreach (Product p in shoppingBag.Products.Keys)
            {
                product new_p = new product(p);
                this.products[new_p] = shoppingBag.Products[p];
            }
        }
        #endregion
    }
}