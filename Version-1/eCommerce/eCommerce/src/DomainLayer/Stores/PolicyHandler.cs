using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Purchase.Policies;
using eCommerce.src.DomainLayer.Stores.Purchase.Policies.Primitive;
using eCommerce.src.DomainLayer.Stores.Purchase.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace eCommerce.src.DomainLayer.Stores
{
    public class PolicyHandler
    {
        public IPurchaseType BuyNow { get; set; }

        public PolicyHandler()
        {
            BuyNow = new BuyNow();
        }

        public bool ValidPolicy(ConcurrentDictionary<Product, int> products, User.User user)
        {
            return BuyNow.IsSatisfiedCond(products, user);
        }

        public IPurchasePolicy CreatePurchasePolicy(Dictionary<string, object> info, IPurchasePolicy policy = null)
        {
            if (!info.ContainsKey("type"))
                throw new Exception("Can't create a purchase Policy without a type");

            string type = ((JsonElement)info["type"]).GetString();
            switch (type)
            {
                // LOGICAL POLICIES:
                case "AndPolicy":
                    return new AndPolicy();
                case "OrPolicy":
                    return new OrPolicy();
                case "ConditionalPolicy":
                    return new ConditionalPolicy();

                // PRIMITIVE POLICIES:
                case "AgePolicy":
                    return AgePolicy.Create(info);
                case "QuantityPolicy":
                    return QuantityPolicy.Create(info);
                case "TimePolicy":
                    return TimePolicy.Create(info, policy);

                default:
                    throw new Exception("Can't recognize this purchase policy type: " + type);

            }
        }
    }
}
