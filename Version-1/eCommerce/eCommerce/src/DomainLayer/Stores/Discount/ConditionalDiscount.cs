using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Types
{
    public class ConditionalDiscount : DiscountType
    {
        public IDiscountCondition Condition { get; }
        public IDiscountType Discount { get; }

        public ConditionalDiscount(IDiscountType discount, IDiscountCondition condition, String id = "") : base(id)
        {
            Condition = condition;
            Discount = discount;
        }
        public override bool AddDiscount(string id, IDiscountType discount)
        {
            return Discount.AddDiscount(id, discount);
        }

        public override Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            bool isEligible = Condition.isCond(products);
            if (isEligible)
            {
                return Discount.CalculateDiscount(products);
            }
            return new Dictionary<Product, Double>();
        }

        public override bool RemoveDiscount(string id)
        {
            return Discount.RemoveDiscount(id);
        }
    }
}
