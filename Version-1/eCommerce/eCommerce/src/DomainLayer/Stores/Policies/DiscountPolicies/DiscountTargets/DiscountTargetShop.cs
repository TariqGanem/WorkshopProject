using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountTargets
{
    public class DiscountTargetShop : IDiscountTarget
    {
        public static IDiscountTarget create(Dictionary<string, object> info)
        {
            return new DiscountTargetShop();
        }

        public List<Product> getTargets(ConcurrentDictionary<Product, int> products)
        {
            List<Product> result = new List<Product>();
            foreach (KeyValuePair<Product, int> entry in products)
                result.Add(entry.Key);
            return result;
        }

        public String GetData()
        {
            return "the whole store";
        }

        public string getId()
        {
            return "";
        }
    }
}
