using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class DiscountAdditionData : AbstractDiscountPolicyData
    {

        public List<IDiscountPolicyData> Discounts { get; }

        public DiscountAdditionData(List<IDiscountPolicyData> discounts, String id = "") : base(id)
        {
            Discounts = discounts;
        }

    }
}
