using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Purchase.Policies;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Types
{
    internal class BuyNow : IPurchaseType
    {
        public string Id => throw new NotImplementedException();

        public bool AddPolicy(LogicPolicy policy, string id)
        {
            throw new NotImplementedException();
        }

        public bool EditPolicy(Dictionary<string, object> info, string id)
        {
            throw new NotImplementedException();
        }

        public bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            throw new NotImplementedException();
        }

        public LogicPolicy RemovePolicy(string id)
        {
            throw new NotImplementedException();
        }
    }
}
