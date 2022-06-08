using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public interface IPurchasePolicy
    {
        string Id { get; }
        bool IsConditionMet(ConcurrentDictionary<Product, int> bag, User.User user);
        bool AddPolicy(IPurchasePolicy policy, string id);
        IPurchasePolicy RemovePolicy(string id);
        IDictionary<string, object> GetData();
        bool EditPolicy(Dictionary<string, object> info, string id);
    }
}
