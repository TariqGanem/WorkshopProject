using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.Stores.Policies;
using eCommerce.src.DomainLayer.Stores.Policies.PurchasePolicies;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests.PolicyManagerTests.PurchasePolicyTests
{
    public class PolicyRemoveTests
    {
        public PolicyManager PolicyManager { get; }

        public Dictionary<String, Product> Products { get; }

        public ConcurrentDictionary<Product, int> currProducts { get; }

        public PolicyRemoveTests()
        {
            PolicyManager = new PolicyManager();
            Products = new Dictionary<string, Product>();
            currProducts = new ConcurrentDictionary<Product, int>();
            Products.Add("Bread", new Product("Bread", 10, "Bakery", 100 , new LinkedList<string>()));
            Products.Add("Milk", new Product("Milk", 15, "Dairy", 100 , new LinkedList<string>()));
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void RemovePolicyTest()
        {
            IPurchasePolicy policy = new MaxProductPolicy(Products["Bread"].Id, 10);
            PolicyManager.AddPurchasePolicy(policy);
            Assert.Equal(PolicyManager.MainPolicy.Policy.Policies[0], policy);
            PolicyManager.RemovePurchasePolicy(policy.Id);
            Assert.Empty(PolicyManager.MainPolicy.Policy.Policies);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void RemovePolicyToTest()
        {
            IPurchasePolicy policy = new MinProductPolicy(Products["Bread"].Id, 10);
            AndPolicy andPolicy = new AndPolicy();
            PolicyManager.AddPurchasePolicy(andPolicy);
            PolicyManager.AddPurchasePolicy(policy, andPolicy.Id);
            IPurchasePolicy res = PolicyManager.RemovePurchasePolicy(policy.Id);
            Assert.Empty(andPolicy.Policies);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void RemoveNonexistantPolicyTest()
        {
            IPurchasePolicy policy = new MinProductPolicy(Products["Bread"].Id, 10);
            AndPolicy andPolicy = new AndPolicy();
            PolicyManager.AddPurchasePolicy(andPolicy);
            IPurchasePolicy res = PolicyManager.RemovePurchasePolicy(policy.Id);
            Assert.False(res != null);
        }
    }
}
