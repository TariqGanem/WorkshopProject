using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class MaxProductConditionData : AbstractDiscountConditionData
    {

        public int MaxQuantity { get; }
        public String ProductId { get; }

        public MaxProductConditionData(String productId, int maxQuantity, String id = "") : base(id)
        {
            ProductId = productId;
            MaxQuantity = maxQuantity;
        }

    }
}
