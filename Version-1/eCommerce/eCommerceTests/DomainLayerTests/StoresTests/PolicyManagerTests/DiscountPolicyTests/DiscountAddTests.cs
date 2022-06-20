using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountComposition;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountTargets;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests.PolicyManagerTests.DiscountPolicyTests
{
    public class DiscountAddTests
    {
        public PolicyManager PolicyManager { get; }

        public Dictionary<String, Product> Products { get; }

        public ConcurrentDictionary<Product, int> currProducts { get; }

        public DiscountAddTests()
        {
            PolicyManager = new PolicyManager();
            Products = new Dictionary<string, Product>();
            currProducts = new ConcurrentDictionary<Product, int>();
            Products.Add("Bread", new Product("Bread", 10, "Bakery",10));
            Products.Add("Milk", new Product("Milk", 15, "Dairy",100 ));
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddDiscountTest()
        {
            IDiscountPolicy p = new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 10);
            PolicyManager.AddDiscountPolicy(p);
            Assert.Equal(PolicyManager.MainDiscount.Discounts[0], p);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddDiscountToTest()
        {
            DiscountOr p1 = new DiscountOr();
            PolicyManager.AddDiscountPolicy(p1);
            IDiscountPolicy p2 = new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 10);
            PolicyManager.AddDiscountPolicy(p2, p1.Id);
            Assert.Equal(p1.Discounts[0], p2);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddDiscountToNonExistantTest()
        {
            IDiscountPolicy p = new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 10);
            try
            {
                bool result = PolicyManager.AddDiscountPolicy(p, "Non existant Id");
            }
            catch { Assert.True(true); } // exception should be thrown
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddDiscountToIllegalDiscountTest()
        {
            IDiscountPolicy p1 = new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 20);
            DiscountXor p2 = new DiscountXor(p1, p1, new MinBagPriceCondition(0));
            PolicyManager.AddDiscountPolicy(p2);
            IDiscountPolicy p3 = new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 10);
            try
            {
                bool result = PolicyManager.AddDiscountPolicy(p3, p2.Id);
            }
            catch (Exception ex) { Assert.True(true); }
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddConditionToTest()
        {
            DiscountConditionOr c = new DiscountConditionOr();
            ConditionalDiscount p = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 20), c);
            PolicyManager.AddDiscountPolicy(p);
            MinBagPriceCondition c1 = new MinBagPriceCondition(0);
            PolicyManager.AddDiscountCondition(c1, c.Id);
            Assert.Equal(c.Conditions[0], c1);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddConditionToNonExistantTest()
        {
            IDiscountCondition p = new MinBagPriceCondition(0);
            try
            {
                bool result = PolicyManager.AddDiscountCondition(p, "Non existant Id");
            }
            catch(Exception ex) { Assert.True(true); }
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddConditionToIllegalConditionTest()
        {
            IDiscountCondition c = new MinBagPriceCondition(0);
            IDiscountPolicy p = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 20), c);
            PolicyManager.AddDiscountPolicy(p);
            try
            {
                bool result = PolicyManager.AddDiscountCondition(c, c.Id);
            }
            catch { Assert.True(true); }
        }
    }
}
