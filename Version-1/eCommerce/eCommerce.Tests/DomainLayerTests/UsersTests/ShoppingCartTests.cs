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
        public Store s1 = new Store("mashbir", new RegisteredUser("ahmed1","ahmed1"));
        public ShoppingBag bag1;
        public Store s2 = new Store("shofersal", new RegisteredUser("ahmed2","ahmed2"));
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
            bag1 = new ShoppingBag("1", s1);
            bag1.Products[new Product("a", 2, "b", 4)] = 3;
            bag1.Products[new Product("aa", 3, "bb", 2)] = 1;

            bag2 = new ShoppingBag("2", s2);
            bag2.Products[new Product("aaa", 2, "bbb", 2)] = 1;
            bag2.Products[new Product("aaaa", 3, "bbbb", 5)] = 4;

            cart.ShoppingBags.TryAdd(s1.Id, bag1);
            cart.ShoppingBags.TryAdd(s2.Id, bag2);

            ShoppingBag new_bag1 = cart.GetShoppingBag(s1.Id);
            Assert.AreEqual(new_bag1.Id,bag1.Id);
            Assert.AreEqual(new_bag1.Store, bag1.Store);
            Assert.AreEqual(new_bag1.UserId, bag1.UserId);
            Assert.AreEqual(new_bag1.Products, bag1.Products);
            Assert.AreEqual(new_bag1.TotalBagPrice,bag1.TotalBagPrice);

            try
            {
                cart.GetShoppingBag("aaaaaaa");
            }
            catch (Exception ex1)
            {
                Assert.AreEqual($"Shopping bag not found for the store id: aaaaaaa.", ex1.Message);
            }
        }

        [Fact]
        public void AddShoppingBagToCart()
        {
            bag1 = new ShoppingBag("1", s1);
            bag1.Products[new Product("a", 2, "b", 4)] = 3;
            bag1.Products[new Product("aa", 3, "bb", 2)] = 1;

            bag2 = new ShoppingBag("2", s2);
            bag2.Products[new Product("aaa", 2, "bbb", 2)] = 1;
            bag2.Products[new Product("aaaa", 3, "bbbb", 5)] = 4;

            cart.ShoppingBags.TryAdd(s1.Id, bag1);
            cart.ShoppingBags.TryAdd(s2.Id, bag2);

            Store s3 = new Store("rami-levi", new RegisteredUser("ahmed3","ahmed3"));
            ShoppingBag bag3 = new ShoppingBag("3", s3);
            bag3.Products[new Product("a", 2, "b", 4)] = 3;
            bag3.Products[new Product("aa", 3, "bb", 2)] = 1;
            cart.AddShoppingBagToCart(bag3);
            Assert.AreEqual(cart.ShoppingBags.Count, 3);
            Assert.IsTrue(cart.ShoppingBags.ContainsKey(s3.Id));
        }

        [Fact]
        public void GetTotalShoppingCartPrice()
        {
            bag1 = new ShoppingBag("1", s1);
            bag1.Products[new Product("a", 2, "b", 4)] = 3;
            bag1.Products[new Product("aa", 3, "bb", 2)] = 1;

            bag2 = new ShoppingBag("2", s2);
            bag2.Products[new Product("aaa", 2, "bbb", 2)] = 1;
            bag2.Products[new Product("aaaa", 3, "bbbb", 5)] = 4;

            cart.ShoppingBags.TryAdd(s1.Id, bag1);
            cart.ShoppingBags.TryAdd(s2.Id, bag2);

            Assert.AreEqual(cart.GetTotalShoppingCartPrice(), 10);
        }
    }
}
