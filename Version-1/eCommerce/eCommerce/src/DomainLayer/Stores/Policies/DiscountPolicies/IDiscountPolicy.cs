using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies
{
    public interface IDiscountPolicy
    {
        String Id { get; }

        Dictionary<Product, Double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "");

        bool AddDiscount(String id, IDiscountPolicy discount);

        bool AddCondition(String id, IDiscountCondition condition);

        IDiscountPolicy RemoveDiscount(String id);

        IDiscountCondition RemoveCondition(String id);

        bool EditDiscount(Dictionary<string, object> info, string id);

        bool EditCondition(Dictionary<string, object> info, string id);

        IDictionary<string, object> GetData();

    }
}
