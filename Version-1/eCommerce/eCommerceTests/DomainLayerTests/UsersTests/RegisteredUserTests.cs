using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using eCommerceIntegrationTests.Utils;
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
    public class RegisteredUserTests: XeCommerceTestCase
    {
        public RegisteredUser user = new RegisteredUser("ahmed","ahmed");

        [SetUp]
        public void Setup()
        {

        }

        [Fact]
        public void AddProductToCartTest()
        {
            Product p1 = new Product("bamba", 3, "snacks", 4);
            Store s1 = new Store("shofersal", new RegisteredUser("ahmed1","ahmed1"));
            ShoppingBag bag1 = new ShoppingBag("1",s1);
            user.ShoppingCart.AddShoppingBagToCart(bag1);
            user.AddProductToCart(p1, 2, s1);
            Assert.IsTrue(user.ShoppingCart.ShoppingBags[s1.Id].Products.ContainsKey(p1));

            Product p2 = new Product("bamba2", 3, "snacks", 4);
            Store s2 = new Store("shofersal2", new RegisteredUser("ahmed2", "ahmed2"));
            ShoppingBag bag2 = new ShoppingBag("2", s1);
            user.AddProductToCart(p2, 2, s2);
            Assert.IsTrue(user.ShoppingCart.ShoppingBags[s2.Id].Products.ContainsKey(p2));
        }

        [Fact]
        public void UpdateShoppingCartTest()
        {
            Product p1 = new Product("bamba", 3, "snacks", 4);
            Store s1 = new Store("shofersal", new RegisteredUser("ahmed", "ahmed"));
            ShoppingBag bag1 = new ShoppingBag("1", s1);
            bag1.AddProtuctToShoppingBag(p1, 3);
            user.ShoppingCart.AddShoppingBagToCart(bag1);
            user.UpdateShoppingCart(s1.Id, p1, 2);
            Assert.IsTrue(bag1.Products[p1] == 2);
        }

        [Fact]
        public void PurchaseTest()
        {
            IDictionary<string, object> payments = new Dictionary<string, object>();
            IDictionary<string, object> delivery = new Dictionary<string, object>();

            try
            {
                //user.Purchase(payments, delivery);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "The shopping cart is empty!");
            }

            Store s1 = new Store("shofersal", new RegisteredUser("ahmed", "ahmed"));
            ShoppingBag bag1 = new ShoppingBag("1", s1);
            Product p1 = new Product("bamba", 3, "snacks", 4);
            bag1.Products[p1] = 5;
            user.ShoppingCart.AddShoppingBagToCart(bag1);

            try
            {
                //user.Purchase(payments, delivery);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Notice - The store is out of stock!");
            }

            user.ShoppingCart.ShoppingBags.TryRemove(bag1.Store.Id,out ShoppingBag s);
            Product p2 = new Product("besli", 3, "snacks", 4);
            user.AddProductToCart(p2,2,s1);
            ShoppingCart cart1 = user.ShoppingCart;
            //ShoppingCart cart2 = user.Purchase(payments, delivery);
            //Assert.AreEqual(cart2.Id, cart1.Id);
            //Assert.AreEqual(cart2.TotalCartPrice, cart1.TotalCartPrice);
            //Assert.AreEqual(cart2.ShoppingBags, cart1.ShoppingBags);
            Assert.IsTrue(user.ShoppingCart != cart1);
            Assert.IsTrue(user.ShoppingCart.ShoppingBags.Count == 0);
        }

        [Fact]
        public void Login()
        {
            try
            {
                user.Login("a");
            }catch(Exception e)
            {
                Assert.AreEqual(e.Message, "Wrong password!");
                Assert.IsFalse(user.Active);
            }

            user.Login("ahmed");
            Assert.IsTrue(user.Active);
        }

        [Fact]
        public void Logout()
        {
            user.Login("ahmed");
            user.Logout();
            Assert.IsFalse(user.Active);

            try
            {
                user.Logout();
            }catch(Exception e)
            {
                Assert.AreEqual(e.Message, "User already loged out!");
                Assert.IsFalse(user.Active);
            }
        }
    }
}
