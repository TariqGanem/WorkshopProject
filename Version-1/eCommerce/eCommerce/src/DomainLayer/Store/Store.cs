using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using eCommerce.src.ServiceLayer.Response;

namespace eCommerce.src.DomainLayer.Store
{
    public class Store
    {
        public String name { get; set; }
        public Boolean isOpen { get; set; }
        public StoreOwner Founder { get; }
        public ConcurrentDictionary<String,Product> products { get; }
        public double Rating { get; set; }
        public int AmountOfRating { get; set; }
        public StoreHistory History { get; set; }
        public ConcurrentDictionary<String,StoreOwner> owners { get; }
        public ConcurrentDictionary<String,StoreManager> managers { get; }

        public Store(RegisteredUser owner , String name)
        {
            owners = new ConcurrentDictionary<string, StoreOwner>();
            managers = new ConcurrentDictionary<string, StoreManager>();
            products = new ConcurrentDictionary<String,Product>();
            isOpen = true;
            Founder = owner;
            this.name = name;
            owners.AddOrUpdate(owner.name, owner);
            AmountOfRating = 0;
            Rating = 0;
            History = new StoreHistory();
        }

        public void PurchaseProduct(String productName, int quantity)
        {
            if (!products.ContainsKey(productName))
                throw new Exception($"Store: {this.name} does not have {productName}");
            Product temp;
            products.TryGetValue(productName, out temp);
            temp.PickProduct(quantity);
        }



    }
}
