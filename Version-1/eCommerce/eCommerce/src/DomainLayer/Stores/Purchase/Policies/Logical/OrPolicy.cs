using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Purchase.Policies
{
    internal class OrPolicy : LogicPolicy
    {
        public OrPolicy(string id = "") : base(id)
        {

        }
        public OrPolicy(List<IPurchasePolicy> policies, string id = "") : base(policies, id)
        {

        }

        public override bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user)
        {
            foreach (IPurchasePolicy policy in Policies)
            {
                if (policy.IsSatisfiedCond(bag, user))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
