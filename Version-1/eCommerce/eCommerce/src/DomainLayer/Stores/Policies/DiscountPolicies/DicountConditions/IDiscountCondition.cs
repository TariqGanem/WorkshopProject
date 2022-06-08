using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions
{
    public interface IDiscountCondition
    {
        String Id { get; }
        bool isConditionMet(ConcurrentDictionary<Product, int> products);
        bool AddCondition(String id, IDiscountCondition condition);
        IDiscountCondition RemoveCondition(String id);
        bool EditCondition(Dictionary<string, object> info, string id);
        IDictionary<string, object> GetData();
    }
}
