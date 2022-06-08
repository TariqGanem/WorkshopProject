using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class MinProductConditionData : AbstractDiscountConditionData
    {

        public int MinQuantity { get; }
        public String ProductId { get; }

        public MinProductConditionData(String productId, int minQuantity, String id = "") : base(id)
        {
            ProductId = productId;
            MinQuantity = minQuantity;
        }

    }
}
