using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class MaxProductPolicyData : IPurchasePolicyData
    {
        public string ProductId { get; }
        public int Max { get; }
        public string Id { get; }

        public MaxProductPolicyData(string productId, int max, string id)
        {
            this.ProductId = productId;
            this.Max = max;
            this.Id = id;
        }
    }
}

