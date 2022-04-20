using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using Xunit;

namespace eCommerce.Tests.DomainLayerTests.StoresTests
{
    public class StoreHistoryTests
    {
        public StoreHistory storehistory { get; }

        public StoreHistoryTests()
        {
            storehistory = new StoreHistory();
        }

        [Fact]
        [Trait("Category" ,"UnitTesting")]
        public void addShoppingBag()
        {
            ShoppingBag shoppingbag1 = new ShoppingBag("user1success", null);
            ShoppingBag shoppingbag2 = new ShoppingBag("user2fail", null);
            storehistory.addShoppingBasket(shoppingbag1);
            Assert.Contains(shoppingbag1, storehistory.history);
            Assert.DoesNotContain(shoppingbag2, storehistory.history);
        }
    }
}
