﻿using eCommerce.src.DomainLayer.Store;
using eCommerce.src.ExternalSystems;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.User
{
    public class RegisteredUser : User
    {
        public string UserName { get; }
        private string _password;
        public History History { get; set; }

        public RegisteredUser(String userName, String password) : base()
        {
            UserName = userName;
            _password = password;
            this.History = new History();
        }

        public void Login(String password)
        {
            if (!_password.Equals(password))
            {
                throw new Exception("Wrong password!");
            }
            Active = true;
        }

        public void Logout()
        {
            if (Active)
                Active = false;
            else throw new Exception("User already loged out!");
        }

        //TODO 
        public Product AddProductReview(Store.Store store, Product product, String review)
        {
            /*if (checkIfProductPurchasedByUser(store, product))
            {
                product.AddProductReview(Id, review);
            }
            throw new Exception("The User did not purchase the product before, therefore can not write it a review!");*/
            throw new NotImplementedException();
        }

        //TODO
        private Boolean checkIfProductPurchasedByUser(Store.Store store, Product product)
        {
            /*LinkedList<ShoppingBag> shoppingBags = History.ShoppingBags;
            foreach (ShoppingBag bag in shoppingBags)
            {
                foreach (ConcurrentDictionary<Product, int> productQuantity in bag.Products)
                {
                    Product productInHistory = productQuantity.TryGetValue(pr);
                    if (productInHistory.Id.Equals(product.Id)) { return true; }
                }

            }
            return false;*/
            throw new NotImplementedException();
        }
    }
}
