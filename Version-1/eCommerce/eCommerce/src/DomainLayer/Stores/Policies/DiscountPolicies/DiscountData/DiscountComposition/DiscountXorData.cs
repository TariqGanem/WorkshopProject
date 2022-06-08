using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class DiscountXorData : AbstractDiscountPolicyData
    {

        public IDiscountPolicyData Discount1 { get; }
        public IDiscountPolicyData Discount2 { get; }
        public IDiscountConditionData ChoosingCondition { get; }

        public DiscountXorData(IDiscountPolicyData discount1, IDiscountPolicyData discount2, IDiscountConditionData choosingCondition, String id = "") : base(id)
        {
            Discount1 = discount1;
            Discount2 = discount2;
            ChoosingCondition = choosingCondition;
        }

    }
}
