﻿using eCommerce.src.DataAccessLayer.DataTransferObjects.User;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.Offer;
using eCommerce.src.ServiceLayer.Objects;
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
            Id = Service.GenerateId();
            this.UserId = userId;
            this.Store = store;
            Products = new ConcurrentDictionary<Product, int>();
            TotalBagPrice = 0;
        }

        public ShoppingBag(String id , String userId , Store.Store store , ConcurrentDictionary<Product, int> products , Double tp)
        {
            this.Id = id; 
            this.Products = products;
            this.Store = store;
            this.UserId=userId;
            this.TotalBagPrice = tp;
        }

        public Boolean AddProtuctToShoppingBag(Product product, int quantity)
        {
            if (product.Quantity >= quantity && quantity > 0)
            {
                bool res = Products.TryAdd(product, quantity);
                if (res)
                    TotalBagPrice = product.Price * quantity;
                return res;
            }
            throw new Exception($"Asked quantity ({quantity}) of product {product.Name} is higher than quantity in store ({product.Quantity}).");
        }

        // This quantity will be the updated quantity of the product in the bag .
        // If negative or zero then the product will be removed
        public Boolean UpdateShoppingBag(Product product, int quantity)
        {
            if (Products.ContainsKey(product))
            {
                if (quantity <= 0)
                {
                    return Products.TryRemove(product, out int q);
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
                else
                    throw new Exception($"Asked quantity ({quantity}) of product {product.Name} is higher than quantity in store ({product.Quantity}).");
            }
            else
                throw new Exception($"You did not add the product {product.Name} to this shopping bag. Therefore attempt to update shopping bag faild!");
        }

        public double GetTotalPrice(List<Offer> offers, String DiscountCode = "")
        {
            Double amount = Store.PolicyManager.GetTotalBagPrice(this.Products, DiscountCode, offers);
            this.TotalBagPrice = amount;
            return amount;
        }
        public ShoppingBagSO getSO()
        {
            return new ShoppingBagSO(this);
        }

        public DTO_ShoppingBag getDTO()
        {
            ConcurrentDictionary<string, int> products_dto = new ConcurrentDictionary<string, int>();
            foreach (var p in Products)
            {
                products_dto.TryAdd(p.Key.Id, p.Value);
            }
            return new DTO_ShoppingBag(Id, UserId, Store.Id, products_dto, TotalBagPrice);
        }

        public bool AdheresToPolicy(User user)
        {
            return Store.PolicyManager.AdheresToPolicy(this.Products, user);
        }
    }
}
