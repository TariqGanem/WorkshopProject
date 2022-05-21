using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Policies
{
    internal abstract class PrimitivePolicy : IPurchasePolicy
    {
        public string Id { get; }

        protected PrimitivePolicy(string id = "")
        {
            this.Id = id;
            if (id.Equals(""))
                this.Id = Service.GenerateId();
        }

        public abstract bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user);

        public abstract PrimitivePolicy Create(Dictionary<string, object> info);
    }
}
