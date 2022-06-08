using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class MinProductPolicyData : IPurchasePolicyData
    {
        public string ProductId { get; }
        public int Min { get; }
        public string Id { get; }

        public MinProductPolicyData(string productId, int min, string id)
        {
            this.ProductId = productId;
            this.Min = min;
            this.Id = id;
        }
    }
}
