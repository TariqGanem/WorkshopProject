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
        public Store mashbir = new Store("Mashbir", new RegisteredUser("ahmed1", "ahmed1"));
        public Store shofersal = new Store("Shofersal", new RegisteredUser("ahmed2", "ahmed2"));

        [SetUp]
        public void Setup() { }
        
        [Fact]
        public void AddPurchasedShoppingCartTesthappy()
        {
            history.ShoppingBags.AddLast(new ShoppingBag("1", mashbir));
            cart.AddShoppingBagToCart(new ShoppingBag("2", mashbir));
            cart.AddShoppingBagToCart(new ShoppingBag("3", shofersal));

            history.AddPurchasedShoppingCart(cart);
            //checking the number of shoppingBags
            Assert.That(history.ShoppingBags.Count, Is.EqualTo(3));
            //checking if the shoppingBags of the added shoppingCart had been added 
            Assert.IsTrue(history.ShoppingBags.Contains(cart.ShoppingBags[mashbir.Id]));
            Assert.IsTrue(history.ShoppingBags.Contains(cart.ShoppingBags[shofersal.Id]));
        }

        [Fact]
        public void AddPurchasedShoppingBagTesthappy()
        {
            history.ShoppingBags.AddLast(new ShoppingBag("1", mashbir));
            cart.AddShoppingBagToCart(new ShoppingBag("2", mashbir));
            cart.AddShoppingBagToCart(new ShoppingBag("3", shofersal));

            ShoppingBag shoppingBag = new ShoppingBag("4",new Store("shofersal", new RegisteredUser("ahmed", "ahmed")));
            history.AddPurchasedShoppingBag(shoppingBag);
            //chekcing the number of shoppingBags
            Assert.That(history.ShoppingBags.Count, Is.EqualTo(2));
            //checking if the shoppingBag had been added
            Assert.IsTrue(history.ShoppingBags.Contains(shoppingBag));
        }
    }
}
