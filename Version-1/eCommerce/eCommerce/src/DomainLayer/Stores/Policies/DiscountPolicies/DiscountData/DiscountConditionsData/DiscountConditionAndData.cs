using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class DiscountConditionAndData : AbstractDiscountConditionData
    {

        public List<IDiscountConditionData> Conditions { get; }

        public DiscountConditionAndData(List<IDiscountConditionData> conditions, String id = "") : base(id)
        {
            Conditions = conditions;
        }

    }
}
