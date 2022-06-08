using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions
{
    public abstract class AbstractDiscountCondition : IDiscountCondition
    {
        public string Id { get; }

        public AbstractDiscountCondition(Dictionary<string, object> info, String id = "")
        {
            if (id.Equals(""))
                Id = Service.GenerateId();
            else
                Id = id;
        }

        public abstract bool isConditionMet(ConcurrentDictionary<Product, int> products);
        public abstract bool AddCondition(string id, IDiscountCondition condition);
        public abstract IDiscountCondition RemoveCondition(string id);
        public abstract bool EditCondition(Dictionary<string, object> info, string id);
        public abstract IDictionary<string, object> GetData();
    }
}
