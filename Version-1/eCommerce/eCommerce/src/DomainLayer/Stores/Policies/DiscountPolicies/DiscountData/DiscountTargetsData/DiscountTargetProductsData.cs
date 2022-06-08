using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class DiscountTargetProductsData : IDiscountTargetData
    {

        public List<string> ProductIds { get; }

        public DiscountTargetProductsData(List<string> productIds)
        {
            ProductIds = productIds;
        }

    }
}
