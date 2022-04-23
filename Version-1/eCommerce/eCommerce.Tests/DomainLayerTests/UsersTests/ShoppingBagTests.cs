using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Test]
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

        [Test]
        public void UpdateShoppingBagTest(Product product, int quantity)
        {

        }

        public void GetTotalPriceTest()
        {
            Assert.Equals(bag.GetTotalPrice(),5);
        }
    }
}

