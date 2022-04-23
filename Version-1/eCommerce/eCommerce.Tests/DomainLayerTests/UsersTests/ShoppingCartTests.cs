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
    public class ShoppingCartTests
    {
        ShoppingCart cart = new ShoppingCart();
        public Store s1 = new Store("mashbir", null);
        public ShoppingBag bag1;
        public Store s2 = new Store("shofersal", null);
        public ShoppingBag bag2;

        [SetUp]
        public void Setup()
        {
            bag1 = new ShoppingBag("1", s1);
            bag1.Products[new Product("a", 2, "b", 4)] = 3;
            bag1.Products[new Product("aa", 3, "bb", 2)] = 1;

            bag2 = new ShoppingBag("2", s2);
            bag2.Products[new Product("aaa", 2, "bbb", 2)] = 1;
            bag2.Products[new Product("aaaa", 3, "bbbb", 5)] = 4;

            cart.ShoppingBags.TryAdd(s1.Id,bag1);
            cart.ShoppingBags.TryAdd(s2.Id,bag2);
        }

        [Fact]
        public void GetShoppingBag()
        {
            Assert.Equals(cart.GetShoppingBag(s1.Id),bag1);
            Assert.Equals(cart.GetShoppingBag(s2.Id),bag2);

            try
            {
                cart.GetShoppingBag("aaaaaaa");
            }
            catch (Exception ex1)
            {
                Assert.Equals($"Shopping bag not found for the store id: aaaaaaa.", ex1.Message);
            }
        }

        [Fact]
        public void AddShoppingBagToCart()
        {
            Store s3 = new Store("rami-levi", null);
            ShoppingBag bag3 = new ShoppingBag("3", s1);
            bag3.Products[new Product("a", 2, "b", 4)] = 3;
            bag3.Products[new Product("aa", 3, "bb", 2)] = 1;
            Assert.Equals(cart.ShoppingBags.Count, 3);
            Assert.IsTrue(cart.ShoppingBags.ContainsKey(s3.Id));
        }

        [Fact]
        public void GetTotalShoppingCartPrice()
        {
            Assert.Equals(cart.GetTotalShoppingCartPrice(), 10);
        }
    }
}
