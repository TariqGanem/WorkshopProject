using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Discount.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Policies.Primitive
{
    public class MaximumPolicy : DiscountType
    {
        public List<IDiscountType> Discounts { get; }

        public MaximumPolicy(String id = "") : base(id)
        {
            Discounts = new List<IDiscountType>();
        }

        public MaximumPolicy(List<IDiscountType> discounts, String id = "") : base(id)
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
        public override Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            if (Discounts.Count == 0)
                return new Dictionary<Product, double>();
            List<Dictionary<Product, Double>> discountsResultsList = CalculateAllDiscounts(products);
            return ChooseDiscountByResult(discountsResultsList, products);
        }
        private List<Dictionary<Product, Double>> CalculateAllDiscounts(ConcurrentDictionary<Product, int> products)
        {
            List<Dictionary<Product, Double>> discountsResultsList = new List<Dictionary<Product, double>>();
            foreach (IDiscountType discount in Discounts)
            {
                Dictionary<Product, Double> discountResultDictionary = discount.CalculateDiscount(products);
                if (discountResultDictionary == null)
                    discountResultDictionary = new Dictionary<Product, double>();
                discountsResultsList.Add(discountResultDictionary);
            }
            return discountsResultsList;
        }

        private Dictionary<Product, double> ChooseDiscountByResult(List<Dictionary<Product, Double>> discountsResultsList, ConcurrentDictionary<Product, int> products)
        {
            Dictionary<Product, double> chosenDiscount = discountsResultsList[0];
            Double chosenValue = CalculateDiscountsValue(chosenDiscount, products);

            foreach (Dictionary<Product, double> discount in discountsResultsList)
            {
                Double currDiscountValue = CalculateDiscountsValue(discount, products);
                if (currDiscountValue > chosenValue)
                {
                    chosenValue = currDiscountValue;
                    chosenDiscount = discount;
                }
            }
            return chosenDiscount;
        }

        private Double CalculateDiscountsValue(Dictionary<Product, Double> discountResult, ConcurrentDictionary<Product, int> products)
        {
            Double acc = 0;
            foreach (KeyValuePair<Product, Double> entry in discountResult)
            {
                acc += entry.Value * entry.Key.Price * products[entry.Key];
            }
            return acc;
        }
    }
}
