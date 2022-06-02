using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer.Stores.Discount
{
    public interface IDiscountCondition
    {
        bool isCond(ConcurrentDictionary<Product, int> products);
    }

    public class AndCondition : IDiscountCondition
    {
        public IDiscountCondition Condition1 { get; }
        public IDiscountCondition Condition2 { get; }

        public AndCondition(IDiscountCondition condition1, IDiscountCondition condition2)
        {
            Condition1 = condition1;
            Condition2 = condition2;
        }

        public bool isCond(ConcurrentDictionary<Product, int> products)
        {

            bool isEligible1 = Condition1.isCond(products);
            bool isEligible2 = Condition2.isCond(products);
            if (isEligible1 && isEligible2)
                return true;
            return false;
        }
    }

    public class OrCondition : IDiscountCondition
    {
        public IDiscountCondition Condition1 { get; }
        public IDiscountCondition Condition2 { get; }

        public OrCondition(IDiscountCondition condition1, IDiscountCondition condition2)
        {
            Condition1 = condition1;
            Condition2 = condition2;
        }

        public bool isCond(ConcurrentDictionary<Product, int> products)
        {
            bool isEligible1 = Condition1.isCond(products);
            bool isEligible2 = Condition2.isCond(products);
            if (isEligible1 || isEligible2)
                return true;
            return false;
        }
    }

    public class XorCondition : IDiscountCondition
    {
        public IDiscountCondition Condition1 { get; }
        public IDiscountCondition Condition2 { get; }

        public XorCondition(IDiscountCondition condition1, IDiscountCondition condition2)
        {
            Condition1 = condition1;
            Condition2 = condition2;
        }

        public bool isCond(ConcurrentDictionary<Product, int> products)
        {
            bool isEligible1 = Condition1.isCond(products);
            bool isEligible2 = Condition2.isCond(products);
            if ((isEligible1 && !isEligible2) | (!isEligible1 && isEligible2))
                return true;
            return false;
        }
    }


}
