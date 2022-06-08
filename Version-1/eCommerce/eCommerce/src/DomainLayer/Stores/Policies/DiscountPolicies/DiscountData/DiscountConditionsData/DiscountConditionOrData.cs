using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class DiscountConditionOrData : AbstractDiscountConditionData
    {

        public List<IDiscountConditionData> Conditions { get; }

        public DiscountConditionOrData(List<IDiscountConditionData> conditions, String id = "") : base(id)
        {
            Conditions = conditions;
        }

    }
}
