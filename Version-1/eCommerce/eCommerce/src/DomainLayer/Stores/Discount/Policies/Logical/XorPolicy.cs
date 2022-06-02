using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Discount.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount.Policies.Logical
{
    public class XorPolicy : DiscountPolicy
    {
        public IDiscountType Discount1 { get; }
        public IDiscountType Discount2 { get; }
        public IDiscountCondition ChoosingCondition { get; }

        public XorPolicy(IDiscountType discount1, IDiscountType discount2, IDiscountCondition choosingCondition)
        {
            Discount1 = discount1;
            Discount2 = discount2;
            ChoosingCondition = choosingCondition;
        }

        public override Dictionary<Product, double> CalculateDiscount(ConcurrentDictionary<Product, int> products, string code = "")
        {
            Dictionary<Product, double> result1 = Discount1.CalculateDiscount(products);
            Dictionary<Product, double> result2 = Discount2.CalculateDiscount(products);

            if (result1 == null)
                return new Dictionary<Product, double>();
            if (result2 == null)
                return new Dictionary<Product, double>();

            if (result1.Count == 0)
                return result2;
            if (result2.Count == 0)
                return result1;
            try
            {
                bool conditionResult = ChoosingCondition.isCond(products);
                if (conditionResult)
                    return result1;
            }
            catch (Exception ex) { }
            return result2;
        }

        public override bool AddDiscount(string id, IDiscountType discount)
        {
            if (Id.Equals(id))
                throw new Exception("Cant Add Discount");
            try
            {
                bool result = Discount1.AddDiscount(id, discount);
                if (result)
                    return result;
            }
            catch(Exception ex) { throw ex; }
            return Discount2.AddDiscount(id, discount);
        }

        public override bool RemoveDiscount(string id)
        {
            if (Discount1.Id.Equals(id))
                throw new Exception("Cant Remove Discount");
            if (Discount2.Id.Equals(id))
                throw new Exception("Cant Remove Discount");
            try
            {
                bool result = Discount1.RemoveDiscount(id);
                if (result)
                    return result;
            }
            catch( Exception ex) { throw ex; }
            return Discount2.RemoveDiscount(id);
        }
    }
}
