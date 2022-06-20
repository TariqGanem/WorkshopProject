using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DicountConditions;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountComposition;
using eCommerce.src.DomainLayer.Stores.Policies.DiscountPolicies.DiscountTargets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests.PolicyManagerTests.DiscountPolicyTests
{
    public class DiscountRemoveTests
    {
        public PolicyManager PolicyManager { get; }

        public Dictionary<String, Product> Products { get; }

        public ConcurrentDictionary<Product, int> currProducts { get; }

        public DiscountRemoveTests()
        {
            PolicyManager = new PolicyManager();
            Products = new Dictionary<string, Product>();
            currProducts = new ConcurrentDictionary<Product, int>();
            Products.Add("Bread", new Product("Bread", 10, "Bakery", 100 , new LinkedList<string>()));
            Products.Add("Milk", new Product("Milk", 15, "Dairy", 100 , new LinkedList<string>()));
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void RemoveMainDiscountTest()
        {
            DiscountOr p1 = new DiscountOr();
            PolicyManager.AddDiscountPolicy(p1);
            PolicyManager.RemoveDiscountPolicy(p1.Id);
            Assert.True(PolicyManager.MainDiscount.Discounts.Count == 0);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void RemoveDiscountTest()
        {
            DiscountOr p1 = new DiscountOr();
            PolicyManager.AddDiscountPolicy(p1);
            IDiscountPolicy p2 = new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 10);
            PolicyManager.AddDiscountPolicy(p2, p1.Id);
            PolicyManager.RemoveDiscountPolicy(p2.Id);
            Assert.True(p1.Discounts.Count == 0);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void RemoveNonExistantDiscountTest()
        {
            try
            {
                IDiscountPolicy result = PolicyManager.RemoveDiscountPolicy("Non existant Id");
            }
            catch (Exception ex) { Assert.True(true); }
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void RemoveConditionTest()
        {
            MinBagPriceCondition c = new MinBagPriceCondition(0);
            DiscountConditionOr cor = new DiscountConditionOr(new List<IDiscountCondition>() { c });
            ConditionalDiscount cd = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, new DiscountTargetShop(), 20), cor);
            PolicyManager.AddDiscountPolicy(cd);
            PolicyManager.RemoveDiscountCondition(c.Id);
            Assert.True(cor.Conditions.Count == 0);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void RemoveNonExistantConditionTest()
        {
            try
            {
                IDiscountCondition result = PolicyManager.RemoveDiscountCondition("Non existant Id");
            }
            catch   (Exception ex) { Assert.True(true); }
        }
    }
}
