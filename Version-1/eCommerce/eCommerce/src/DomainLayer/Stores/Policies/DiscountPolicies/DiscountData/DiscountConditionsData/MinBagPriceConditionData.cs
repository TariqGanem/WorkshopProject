﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class MinBagPriceConditionData : AbstractDiscountConditionData
    {

        public Double MinPrice { get; }

        public MinBagPriceConditionData(Double minPrice, String id = "") : base(id)
        {
            MinPrice = minPrice;
        }

    }
}
