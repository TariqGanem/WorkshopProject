using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Purchase.Policies;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Types
{
    public class BuyNow : IPurchaseType
    {
        public LogicPolicy Policy { get; set; }
        public string Id { get; }

        public BuyNow(string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
            this.Policy = new AndPolicy();
        }

        public BuyNow(AndPolicy policy, string id)
        {
            Policy = policy;
            Id = id;
        }

        public bool AddPolicy(IPurchasePolicy policy)
        {
            return Policy.AddPolicy(policy);
        }

        public bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            return Policy.IsSatisfiedCond(bag, user);
        }

        public IPurchasePolicy RemovePolicy(string id)
        {
            return Policy.RemovePolicy(id);
        }
    }
}
