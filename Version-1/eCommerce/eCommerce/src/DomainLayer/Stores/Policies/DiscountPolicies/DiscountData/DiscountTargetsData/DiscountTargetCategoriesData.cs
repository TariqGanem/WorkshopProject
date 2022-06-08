using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountData
{
    class DiscountTargetCategoriesData : IDiscountTargetData
    {
        public List<string> Categories { get; }

        public DiscountTargetCategoriesData(List<string> categories)
        {
            Categories = categories;
        }

    }
}
