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

        public abstract bool IsSatisfiedCond(ConcurrentDictionary<Product, int> bag, User.User user);

    }
}
