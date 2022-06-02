using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Discount.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Policies.Logical
{
    public class AndPolicy : DiscountType
    {
        public List<IDiscountType> Discounts { get; }

        public AndPolicy(String id = "") : base(id)
        {
            Discounts = new List<IDiscountType>();
        }

        public AndPolicy(List<IDiscountType> discounts, String id = "") : base(id)
        {
            Discounts = discounts;
            if (Discounts == null)
                Discounts = new List<IDiscountType>();
        }
        public override Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            List<Dictionary<Product, double>> results = new List<Dictionary<Product, double>>();

            foreach (IDiscountType myDiscount in Discounts)
            {
                Dictionary<Product, double> result = myDiscount.CalculateDiscount(products, code);

                if (result == null)
                    throw new Exception("Error occured => result = null");
                if (result.Count == 0)
                    return result;
                results.Add(result);
            }

            return combineAndDiscounts(results);
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

        private Dictionary<Product, double> combineAndDiscounts(List<Dictionary<Product, double>> discounts)
        {
            if (discounts == null)
                return new Dictionary<Product, double>();
            if (discounts.Count == 0)
                return new Dictionary<Product, double>();

            Dictionary<Product, double> acc = discounts[0];
            foreach (Dictionary<Product, double> discount in discounts)
            {
                foreach (KeyValuePair<Product, double> entry in discount)
                {
                    if (!acc.ContainsKey(entry.Key))
                        acc[entry.Key] = entry.Value;
                    else if (acc[entry.Key] < entry.Value)
                        acc[entry.Key] = entry.Value;
                }
            }
            return acc;
        }
    }
}
