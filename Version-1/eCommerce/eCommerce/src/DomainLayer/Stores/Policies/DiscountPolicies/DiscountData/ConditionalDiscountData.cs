using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class ConditionalDiscountData : AbstractDiscountPolicyData
    {

        public IDiscountConditionData Condition { get; }
        public IDiscountPolicyData Discount { get; }

        public ConditionalDiscountData(IDiscountPolicyData discount, IDiscountConditionData condition, String id = "") : base(id)
        {
            Condition = condition;
            Discount = discount;
        }

    }
}
