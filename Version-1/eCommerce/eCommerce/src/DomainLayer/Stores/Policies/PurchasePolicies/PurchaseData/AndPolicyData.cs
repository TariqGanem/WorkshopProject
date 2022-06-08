using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class AndPolicyData : IPurchasePolicyData
    {
        public List<IPurchasePolicyData> Policies { get; }
        public string Id { get; }

        public AndPolicyData(List<IPurchasePolicyData> policies, string id)
        {
            this.Policies = policies;
            this.Id = id;
        }
    }
}
