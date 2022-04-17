using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using eCommerce.src.ServiceLayer.Response;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.DomainLayer.User;

namespace eCommerce.src.DomainLayer.Store
{
    public class Store
    {
        public String Id { get; }
        public String Name { get; }
        public Boolean IsOpen { get; }
        public StoreOwner Founder { get; }
        public double Rating { get; }
        public int AmountOfRates { get; }
        public StoreHistory History { get; set; }
        public ConcurrentDictionary<String,StoreOwner> Owners { get; }
        public ConcurrentDictionary<String,StoreManager> Managers { get; }

        public Store(RegisteredUser founder, String name)
        {
            Owners = new ConcurrentDictionary<string, StoreOwner>();
            Managers = new ConcurrentDictionary<string, StoreManager>();
            IsOpen = true;
            Founder = new StoreOwner();
            Name = name;
            Owners.TryAdd(founder.UserName, Founder);
            AmountOfRates = 0;
            Rating = 0;
            History = new StoreHistory();
        }
    }
}
