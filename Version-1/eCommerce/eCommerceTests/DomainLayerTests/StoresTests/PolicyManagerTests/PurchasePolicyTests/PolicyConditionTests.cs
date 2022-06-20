﻿using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies;
using eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests.PolicyManagerTests.PurchasePolicyTests
{
    public class PolicyConditionTests
    {
        public PolicyManager PolicyManager { get; }

        public Dictionary<String, Product> Products { get; }

        public ConcurrentDictionary<Product, int> currProducts { get; }

        public PolicyConditionTests()
        {
            PolicyManager = new PolicyManager();
            Products = new Dictionary<string, Product>();
            currProducts = new ConcurrentDictionary<Product, int>();
            Products.Add("Bread", new Product("Bread", 10, "Bakery", 100 , new LinkedList<string>()));
            Products.Add("Milk", new Product("Milk", 15, "Dairy", 100 , new LinkedList<string>()));
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, false)]
        [InlineData("Bread", 4, true)]
        [InlineData("Milk", 50, false)]
        [InlineData("Milk", 1, true)]
        public void MaxProductPolicyTest(String productName, int count, bool expectedResult)
        {
            PolicyManager.AddPurchasePolicy(new MaxProductPolicy(Products[productName].Id, 5));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.AdheresToPolicy(currProducts, null), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, true)]
        [InlineData("Bread", 4, false)]
        [InlineData("Milk", 50, true)]
        [InlineData("Milk", 1, false)]
        public void MinProductPolicyTest(String productName, int count, bool expectedResult)
        {
            PolicyManager.AddPurchasePolicy(new MinProductPolicy(Products[productName].Id, 5));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.AdheresToPolicy(currProducts, null), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, true)]
        [InlineData("Bread", 4, false)]
        [InlineData("Bread", 11, false)]
        [InlineData("Milk", 8, true)]
        [InlineData("Milk", 1, false)]
        [InlineData("Milk", 20, false)]
        public void AndPolicyTest(String productName, int count, bool expectedResult)
        {
            IPurchasePolicy p1 = new MinProductPolicy(Products[productName].Id, 5);
            IPurchasePolicy p2 = new MaxProductPolicy(Products[productName].Id, 10);
            List<IPurchasePolicy> pList = new List<IPurchasePolicy>();
            pList.Add(p1);
            pList.Add(p2);
            PolicyManager.AddPurchasePolicy(new AndPolicy(pList));
            currProducts.TryAdd(Products[productName], count);
            Assert.Equal(PolicyManager.AdheresToPolicy(currProducts, null), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, "Milk", 8, true)]
        [InlineData("Bread", 4, "Milk", 1, true)]
        [InlineData("Bread", 11, "Milk", 20, true)]
        [InlineData("Bread", 4, "Milk", 11, false)]
        public void OrPolicyTest(String productName1, int count1, String productName2, int count2, bool expectedResult)
        {
            IPurchasePolicy p1 = new MinProductPolicy(Products[productName1].Id, 5);
            IPurchasePolicy p2 = new MaxProductPolicy(Products[productName2].Id, 10);
            List<IPurchasePolicy> pList = new List<IPurchasePolicy>();
            pList.Add(p1);
            pList.Add(p2);
            PolicyManager.AddPurchasePolicy(new OrPolicy(pList));
            currProducts.TryAdd(Products[productName1], count1);
            currProducts.TryAdd(Products[productName2], count2);
            Assert.Equal(PolicyManager.AdheresToPolicy(currProducts, null), expectedResult);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData("Bread", 6, "Milk", 8, true)]
        [InlineData("Bread", 4, "Milk", 11, true)]
        [InlineData("Bread", 11, "Milk", 20, false)]
        [InlineData("Bread", 3, "Milk", 10, true)]
        public void ConditionalPolicyTest(String productName1, int count1, String productName2, int count2, bool expectedResult)
        {
            IPurchasePolicy p1 = new MinProductPolicy(Products[productName1].Id, 5);
            IPurchasePolicy p2 = new MaxProductPolicy(Products[productName2].Id, 10);
            PolicyManager.AddPurchasePolicy(new ConditionalPolicy(p1, p2));
            currProducts.TryAdd(Products[productName1], count1);
            currProducts.TryAdd(Products[productName2], count2);
            Assert.Equal(PolicyManager.AdheresToPolicy(currProducts, null), expectedResult);
        }
    }
}
