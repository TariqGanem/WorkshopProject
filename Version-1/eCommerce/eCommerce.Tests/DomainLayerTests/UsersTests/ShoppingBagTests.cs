using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace eCommerce.Tests.DomainLayerTests.UsersTests
{
    public class ShoppingBagTests
    {
        public ShoppingBag bag =  new ShoppingBag("1",null);

        [SetUp]
        public void Setup()
        {
            bag.Products[new Product("a", 2, "b", 4)] = 3;
            bag.Products[new Product("aa", 3, "bb", 2)] = 1;
        }

        [Fact]
        public void AddProtuctToShoppingBagTest()
        {
            Product p1 = new Product("bamba", 5, "snacks", 3);
            Assert.IsTrue(bag.AddProtuctToShoppingBag(p1,2));
            Assert.IsTrue(bag.Products.ContainsKey(p1));
            Assert.IsFalse(bag.AddProtuctToShoppingBag(p1, 1));
            Product p2 = new Product("pesli", 3, "snacks", 2);
            try
            {
                bag.AddProtuctToShoppingBag(p2, 4);
            } catch(Exception ex1)
            {
                Assert.Equals($"Asked quantity (4) of product {p2.Name} is higher than quantity in store ({p2.Quantity}).", ex1.Message);
                Assert.IsFalse(bag.Products.ContainsKey(p2));
            }

            try
            {
                bag.AddProtuctToShoppingBag(p2, -1);
            }
            catch (Exception ex1)
            {
                Assert.Equals($"Asked quantity (-1) of product {p2.Name} is higher than quantity in store ({p2.Quantity}).", ex1.Message);
                Assert.IsFalse(bag.Products.ContainsKey(p2));
            }
        }

        [Fact]
        public void UpdateShoppingBagTest()
        {
            Product p1 = new Product("bamba", 2, "snacks", 4);
            try
            {
                bag.UpdateShoppingBag(p1, 3);
            }catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, $"You did not add the product {p1.Name} to this shopping bag. Therefore attempt to update shopping bag faild!");
                Assert.IsFalse(bag.Products.ContainsKey(p1));
            }

            bag.Products[p1] = 2;
            try
            {
                bag.UpdateShoppingBag(p1, 5);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, $"Asked quantity (5) of product {p1.Name} is higher than quantity in store ({p1.Quantity}).");
                Assert.IsFalse(bag.Products[p1] == 5);
            }

            bag.UpdateShoppingBag(p1, 3);
            Assert.IsTrue(bag.Products[p1] == 3);
        }

        [Fact]
        public void GetTotalPriceTest()
        {
            Assert.Equals(bag.GetTotalPrice(),5);
        }
    }
}

