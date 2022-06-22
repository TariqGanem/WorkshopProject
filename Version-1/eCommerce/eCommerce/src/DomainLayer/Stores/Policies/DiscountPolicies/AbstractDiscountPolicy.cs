using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies
{
    public abstract class AbstractDiscountPolicy : IDiscountPolicy
    {
        public string Id { get; }

        public AbstractDiscountPolicy(Dictionary<string, object> info, String id = "")
        {
            if (id.Equals(""))
                Id = Service.GenerateId();
            else
                Id = id;
        }


        public abstract Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "");
        public abstract bool AddDiscount(String id, IDiscountPolicy discount);
        public abstract IDiscountPolicy RemoveDiscount(String id);
        public abstract bool AddCondition(string id, IDiscountCondition condition);
        public abstract IDiscountCondition RemoveCondition(string id);
        public abstract bool EditDiscount(Dictionary<string, object> info, string id);
        public abstract bool EditCondition(Dictionary<string, object> info, string id);
        public abstract IDictionary<string, object> GetData();

    }
}
