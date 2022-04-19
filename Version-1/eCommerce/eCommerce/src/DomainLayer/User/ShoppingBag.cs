﻿using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class ShoppingBag
    {
        public String Id { get; }
        public String UserId { get; }
        public Store.Store Store { get; }
        public ConcurrentDictionary<Product, int> Products { get; }     // <Product, Quantity>
        public Double TotalBagPrice { get; set; }

        public ShoppingBag(String userId, Store.Store store)
        {
            //Id = Service.GetId();
            this.UserId = userId;
            this.Store = store;
            Products = new ConcurrentDictionary<Product, int>();
            TotalBagPrice = 0;
        }

        public Boolean AddProtuctToShoppingBag(Product product, int quantity)
        {
            /* if (product.Quantity >= quantity && quantity > 0)
             {
                 return Products.TryAdd(product, quantity);
             }
             throw new Exception($"Asked quantity ({quantity}) of product {product.Name} is higher than quantity in store ({product.Quantity}).");*/
            throw new NotImplementedException();
        }

        // This quantity will be the updated quantity of the product in the bag .
        // If negative or zero then the product will be removed
        public Boolean UpdateShoppingBag(Product product, int quantity)
        {
            /* if (Products.ContainsKey(product))
             {
                 if (quantity <= 0)
                 {
                     return Products.Remove(product, out int q);
                 }

                 if (product.Quantity >= quantity)
                 {
                     bool getCurrquantity = Products.TryGetValue(product, out int currQuantity);
                     bool update = Products.TryUpdate(product, quantity, currQuantity);
                     if (getCurrquantity && update)
                     {
                         return true;
                     }
                     throw new Exception("Update shopping cart faild!");
                 }
                 throw new Exception($"Asked quantity ({quantity}) of product {product.Name} is higher than quantity in store ({product.Quantity}).");
             }
             throw new Exception($"You did not add the product {product.Name} to this shopping bag. Therefore attempt to update shopping bag faild!");*/
            throw new NotImplementedException();
        }

        internal double GetTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
