﻿using eCommerce.src.DomainLayer.Store;
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
    public class DiscountCalculationTests
    {
        public PolicyManager PolicyManager { get; }

        public Dictionary<String, Product> Products { get; }

        public ConcurrentDictionary<Product, int> currProducts { get; }

        public DiscountCalculationTests()
        {
            PolicyManager = new PolicyManager();
            Products = new Dictionary<string, Product>();
            Products.Add("Bread", new Product("Bread", 10, "Bakery", 100 , new LinkedList<string>()));
            Products.Add("Milk", new Product("Milk", 20, "Dairy", 100 , new LinkedList<string>()));
            Products.Add("Cup", new Product("Cup", 30, "Dishes", 100 , new LinkedList<string>()));
            currProducts = new ConcurrentDictionary<Product, int>();
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 5, 40)]
        [InlineData("Milk", 5, 80)]
        public void VisibleDiscountTest(String productName, int sum, Double expectedPrice)
        {
            IDiscountTarget target = new DiscountTargetShop();
            PolicyManager.AddDiscountPolicy(new VisibleDiscount(DateTime.MaxValue, target, 20));
            currProducts.TryAdd(Products[productName], sum);
            Double price = PolicyManager.GetTotalBagPrice(currProducts);

            Assert.True(price == expectedPrice);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 20, 200)]
        [InlineData("Bread", 5, 40)]
        [InlineData("Milk", 20, 400)]
        [InlineData("Milk", 5, 100)]
        public void MaxProductDiscountTest(String productName, int count, Double expectedPrice)
        {
            DiscountTargetProducts target = new DiscountTargetProducts(new List<string>() { Products["Bread"].Id });
            PolicyManager.AddDiscountPolicy(new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, target, 20), new MaxProductCondition(Products["Bread"].Id, 10)));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedPrice);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 20, 160)]
        [InlineData("Bread", 5, 50)]
        [InlineData("Milk", 20, 400)]
        [InlineData("Milk", 5, 100)]
        public void MinProductDiscountTest(String productName, int count, Double expectedPrice)
        {
            DiscountTargetProducts target = new DiscountTargetProducts(new List<String>() { Products["Bread"].Id });
            PolicyManager.AddDiscountPolicy(new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, target, 20), new MinProductCondition(Products["Bread"].Id, 10)));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedPrice);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, 48)]
        [InlineData("Bread", 4, 40)]
        [InlineData("Bread", 11, 110)]
        [InlineData("Milk", 8, 160)]
        [InlineData("Milk", 1, 20)]
        [InlineData("Milk", 20, 400)]
        public void AndDiscountTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget target = new DiscountTargetProducts(new List<string>() { Products[productName].Id });
            IDiscountCondition c1 = new MinProductCondition(Products["Bread"].Id, 5);
            IDiscountPolicy p1 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, target, 20), c1);
            IDiscountCondition c2 = new MaxProductCondition(Products["Bread"].Id, 10);
            IDiscountPolicy p2 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, target, 20), c2);
            PolicyManager.AddDiscountPolicy(new DiscountAnd(new List<IDiscountPolicy>() { p1, p2 }));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, 48)]
        [InlineData("Bread", 4, 40)]
        [InlineData("Bread", 11, 110)]
        [InlineData("Milk", 8, 160)]
        [InlineData("Milk", 1, 20)]
        [InlineData("Milk", 20, 400)]
        public void AndDiscountConditionTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget target = new DiscountTargetProducts(new List<string>() { Products[productName].Id });
            IDiscountCondition c1 = new MinProductCondition(Products["Bread"].Id, 5);
            IDiscountCondition c2 = new MaxProductCondition(Products["Bread"].Id, 10);
            IDiscountCondition c = new DiscountConditionAnd(new List<IDiscountCondition>() { c1, c2 });
            IDiscountPolicy p = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, target, 20), c);
            PolicyManager.AddDiscountPolicy(p);
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, 60)]
        [InlineData("Bread", 4, 32)]
        [InlineData("Bread", 11, 88)]
        [InlineData("Milk", 8, 160)]
        [InlineData("Milk", 1, 20)]
        [InlineData("Milk", 20, 400)]
        public void OrDiscountTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget target = new DiscountTargetProducts(new List<string>() { Products[productName].Id });
            IDiscountCondition c1 = new MinProductCondition(Products["Bread"].Id, 10);
            IDiscountPolicy p1 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, target, 20), c1);
            IDiscountCondition c2 = new MaxProductCondition(Products["Bread"].Id, 5);
            IDiscountPolicy p2 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, target, 20), c2);
            PolicyManager.AddDiscountPolicy(new DiscountOr(new List<IDiscountPolicy>() { p1, p2 }));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, 60)]
        [InlineData("Bread", 4, 32)]
        [InlineData("Bread", 11, 88)]
        [InlineData("Milk", 8, 160)]
        [InlineData("Milk", 1, 20)]
        [InlineData("Milk", 20, 400)]
        public void OrDiscountConditionTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget target = new DiscountTargetProducts(new List<string>() { Products[productName].Id });
            IDiscountCondition c1 = new MinProductCondition(Products["Bread"].Id, 10);
            IDiscountCondition c2 = new MaxProductCondition(Products["Bread"].Id, 5);
            IDiscountCondition c = new DiscountConditionOr(new List<IDiscountCondition>() { c1, c2 });
            IDiscountPolicy p = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, target, 20), c);
            PolicyManager.AddDiscountPolicy(p);
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 1, "Milk", 1, 30)]
        [InlineData("Bread", 1, "Milk", 5, 88)]
        [InlineData("Bread", 5, "Milk", 1, 63)]
        [InlineData("Bread", 5, "Milk", 5, 135)]
        public void XorDiscountTest(String productName1, int count1, String productName2, int count2, Double expectedResult)
        {
            IDiscountTarget t = new DiscountTargetShop();
            IDiscountCondition c1 = new MinProductCondition(Products[productName1].Id, 5);
            IDiscountPolicy p1 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 10), c1);
            IDiscountCondition c2 = new MinProductCondition(Products[productName2].Id, 5);
            IDiscountPolicy p2 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 20), c2);
            PolicyManager.AddDiscountPolicy(new DiscountXor(p1, p2, c1));
            currProducts.TryAdd(Products[productName1], count1);
            currProducts.TryAdd(Products[productName2], count2);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 1, 10)]
        [InlineData("Bread", 5, 45)]
        [InlineData("Bread", 10, 80)]
        public void MaxDiscountTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget t = new DiscountTargetShop();
            IDiscountCondition c1 = new MinProductCondition(Products[productName].Id, 5);
            IDiscountPolicy p1 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 10), c1);
            IDiscountCondition c2 = new MinProductCondition(Products[productName].Id, 10);
            IDiscountPolicy p2 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 20), c2);
            PolicyManager.AddDiscountPolicy(new DiscountMax(new List<IDiscountPolicy>() { p1, p2 }));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 1, 10)]
        [InlineData("Bread", 5, 45)]
        [InlineData("Bread", 10, 90)]
        public void MinDiscountTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget t = new DiscountTargetShop();
            IDiscountCondition c1 = new MinProductCondition(Products[productName].Id, 5);
            IDiscountPolicy p1 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 10), c1);
            IDiscountCondition c2 = new MinProductCondition(Products[productName].Id, 10);
            IDiscountPolicy p2 = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 20), c2);
            PolicyManager.AddDiscountPolicy(new DiscountMin(new List<IDiscountPolicy>() { p1, p2 }));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 1, 10)]
        [InlineData("Bread", 5, 40)]
        [InlineData("Bread", 10, 80)]
        public void MinBagPriceConditionTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget t = new DiscountTargetShop();
            IDiscountCondition c = new MinBagPriceCondition(50);
            IDiscountPolicy p = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 20), c);
            PolicyManager.AddDiscountPolicy(p);
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 1, 8)]
        [InlineData("Milk", 1, 16)]
        [InlineData("Cup", 1, 30)]
        public void DiscountTargetProductsTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget t = new DiscountTargetProducts(new List<string>() { Products["Bread"].Id, Products["Milk"].Id });
            IDiscountCondition c = new MinBagPriceCondition(0);
            IDiscountPolicy p = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 20), c);
            PolicyManager.AddDiscountPolicy(p);
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 1, 8)]
        [InlineData("Milk", 1, 16)]
        [InlineData("Cup", 1, 30)]
        public void DiscountTargetCategoriesTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget t = new DiscountTargetCategories(new List<String>() { "Bakery", "Dairy" });
            IDiscountCondition c = new MinBagPriceCondition(0);
            IDiscountPolicy p = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 20), c);
            PolicyManager.AddDiscountPolicy(p);
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 1, 8)]
        [InlineData("Milk", 1, 16)]
        [InlineData("Cup", 1, 24)]
        public void DiscountTargetShopTest(String productName, int count, Double expectedResult)
        {
            IDiscountTarget t = new DiscountTargetShop();
            IDiscountCondition c = new MinBagPriceCondition(0);
            IDiscountPolicy p = new ConditionalDiscount(new VisibleDiscount(DateTime.MaxValue, t, 20), c);
            PolicyManager.AddDiscountPolicy(p);
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 1, "secret code", 8)]
        [InlineData("Bread", 1, "wrong code", 10)]
        public void DiscreetDiscountTest(String productName, int count, String code, Double expectedResult)
        {
            IDiscountTarget t = new DiscountTargetShop();
            IDiscountPolicy p = new DiscreetDiscount(new VisibleDiscount(DateTime.MaxValue, t, 20), "secret code");
            PolicyManager.AddDiscountPolicy(p);
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.GetTotalBagPrice(currProducts, code), expectedResult);
        }
    }
}
