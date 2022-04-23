using eCommerce.src.DomainLayer.User;
using NUnit.Framework;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace eCommerce.Tests.DomainLayerTests.UsersTests
{
    public class HistoryTests
    {
        public History history = new History();
        public ShoppingCart cart = new ShoppingCart();
        public Store mashbir = new Store("Mashbir", null);
        public Store shofersal = new Store("Shofersal", null);

        [SetUp]
        public void Setup()
        {
            history.ShoppingBags.AddLast(new ShoppingBag("1", null));
            cart.AddShoppingBagToCart(new ShoppingBag("2", mashbir));
            cart.AddShoppingBagToCart(new ShoppingBag("3", shofersal));
        }
        
        [Fact]
        public void AddPurchasedShoppingCartTest()
        {
            history.AddPurchasedShoppingCart(cart);
            Assert.That(history.ShoppingBags.Count, Is.EqualTo(3));
            Assert.IsTrue(history.ShoppingBags.Contains(cart.ShoppingBags[mashbir.Id]));
            Assert.IsTrue(history.ShoppingBags.Contains(cart.ShoppingBags[shofersal.Id]));
        }

        [Fact]
        public void AddPurchasedShoppingBagTest()
        {
            ShoppingBag shoppingBag = new ShoppingBag("2",null);
            history.AddPurchasedShoppingBag(shoppingBag);
            Assert.That(history.ShoppingBags.Count, Is.EqualTo(2));
            Assert.IsTrue(history.ShoppingBags.Contains(shoppingBag));
        }
    }
}
