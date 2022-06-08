using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountTargets
{
    public interface IDiscountTarget
    {
        List<Product> getTargets(ConcurrentDictionary<Product, int> products);

        String GetData();

        String getId();
    }
}
