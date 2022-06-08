using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies
{
    public class RestrictedHoursPolicyData : IPurchasePolicyData
    {
        public DateTime StartRestrict { get; }
        public DateTime EndRestrict { get; }
        public string ProductId { get; }
        public string Id { get; }

        public RestrictedHoursPolicyData(DateTime startRestrict, DateTime endRestrict, string productId, string id)
        {
            this.StartRestrict = startRestrict;
            this.EndRestrict = endRestrict;
            this.ProductId = productId;
            this.Id = id;
        }
    }
}
