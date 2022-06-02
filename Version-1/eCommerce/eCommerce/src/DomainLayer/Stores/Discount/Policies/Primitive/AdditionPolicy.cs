using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Discount.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Policies.Primitive
{
    public class AdditionPolicy : DiscountPolicy
    {
        public List<IDiscountType> Discounts { get; }

        public AdditionPolicy(String id = "") : base(id)
        {
            Discounts = new List<IDiscountType>();
        }

        public AdditionPolicy(List<IDiscountType> discounts, String id = "") : base(id)
        {
            Discounts = discounts;
            if (Discounts == null)
                Discounts = new List<IDiscountType>();
        }
        public override bool AddDiscount(string id, IDiscountType discount)
        {
            if (Id.Equals(id))
            {
                Discounts.Add(discount);
                return true;
            }
            foreach (IDiscountType myDiscount in Discounts)
            {
                try
                {
                    bool result = myDiscount.AddDiscount(id, discount);
                    if (result)
                        return result;
                }
                catch (Exception ex)
                { throw ex; }
            }
            return false;
        }


        public override bool RemoveDiscount(string id)
        {
            if (Discounts.RemoveAll(discount => discount.Id.Equals(id)) >= 1)
                return true;
            foreach (IDiscountType myDiscount in Discounts)
            {
                try
                {
                    bool result = myDiscount.RemoveDiscount(id);
                    if (result)
                        return result;
                }
                catch (Exception ex)
                { throw ex; }
            }
            return false;
        }
        public override Dictionary<Product, Double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            Dictionary<Product, Double> result = new Dictionary<Product, Double>();

            foreach (IDiscountType discountPolicy in Discounts)
            {
                Dictionary<Product, Double> discountResult = discountPolicy.CalculateDiscount(products);
                if (discountResult == null)
                    continue;
                foreach (KeyValuePair<Product, Double> entry in discountResult)
                {
                    if (result.ContainsKey(entry.Key))
                        result[entry.Key] = result[entry.Key] + entry.Value;
                    else
                        result[entry.Key] = entry.Value;
                }
            }

            return result;
        }
    }
}
