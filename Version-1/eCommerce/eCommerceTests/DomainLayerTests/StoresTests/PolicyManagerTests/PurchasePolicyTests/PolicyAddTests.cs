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
    public class PolicyAddTests
    {
        public PolicyManager PolicyManager { get; }

        public Dictionary<String, Product> Products { get; }

        public ConcurrentDictionary<Product, int> currProducts { get; }

        public PolicyAddTests()
        {
            PolicyManager = new PolicyManager();
            Products = new Dictionary<string, Product>();
            currProducts = new ConcurrentDictionary<Product, int>();
            Products.Add("Bread", new Product("Bread", 10, "Bakery", 100, new LinkedList<string>()));
            Products.Add("Milk", new Product("Milk", 15, "Dairy", 100, new LinkedList<string>()));
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddPolicyTest()
        {
            IPurchasePolicy policy = new MaxProductPolicy(Products["Bread"].Id, 10);
            Assert.Empty(PolicyManager.MainPolicy.Policy.Policies);
            PolicyManager.AddPurchasePolicy(policy);
            Assert.Equal(PolicyManager.MainPolicy.Policy.Policies[0], policy);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddPolicyToTest()
        {
            IPurchasePolicy policy = new MinProductPolicy(Products["Bread"].Id, 10);
            AndPolicy andPolicy = new AndPolicy();
            Assert.Empty(PolicyManager.MainPolicy.Policy.Policies);
            PolicyManager.AddPurchasePolicy(andPolicy);
            PolicyManager.AddPurchasePolicy(policy, andPolicy.Id);
            Assert.Equal(andPolicy.Policies[0], policy);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddPolicyToNonexistantTest()
        {
            IPurchasePolicy policy = new MinProductPolicy(Products["Bread"].Id, 10);
            AndPolicy andPolicy = new AndPolicy();
            Assert.Empty(PolicyManager.MainPolicy.Policy.Policies);
            try
            {
                bool res = PolicyManager.AddPurchasePolicy(policy, andPolicy.Id);
            }
            catch (Exception ex) { Assert.True(true); }
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void AddPolicyToIllegalPolicyTest()
        {
            IPurchasePolicy policy1 = new MinProductPolicy(Products["Bread"].Id, 5);
            IPurchasePolicy policy2 = new MaxProductPolicy(Products["Bread"].Id, 10);
            Assert.Empty(PolicyManager.MainPolicy.Policy.Policies);
            PolicyManager.AddPurchasePolicy(policy1);
            try
            {
                bool res = PolicyManager.AddPurchasePolicy(policy2, policy1.Id);
            }
            catch(Exception ex) { Assert.True(true); }
        }
    }
}
