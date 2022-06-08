using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class AbstractDiscountConditionData : IDiscountConditionData
    {
        public string Id { get; }

        public AbstractDiscountConditionData(String id = "")
        {
            if (id.Equals(""))
                Id = Service.GenerateId();
            else
                Id = id;
        }

    }
}
