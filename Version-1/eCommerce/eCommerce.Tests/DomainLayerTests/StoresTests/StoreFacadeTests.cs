using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests
{
    public class StoreFacadeTests
    {
        public StoreFacade storefacade { get; set; }
        public Store store1 { get; set; }
        public Store store2 { get; set; }
        public RegisteredUser founder1 { get; set; }
        public RegisteredUser founder2 { get; set; }

        public StoreFacadeTests()
        {
            storefacade = new StoreFacade();
            founder1 = new RegisteredUser("RazerFounder", "Razer>Logitech");
            store1 = new Store("Razer", founder1);
            store1.AddRating(3);
            founder2 = new RegisteredUser("LogitechFounder", "Logitech>Razer");
            store2 = new Store("Logitech",founder2);
        }

        [Theory()]
        [Trait("Category","UnitTesting")]
        [InlineData("name","RazerKeyboard")]
        [InlineData("category","Keyboards")]
        [InlineData("storerating",2.9)]
        [InlineData("name", "LogitechKeyboard")]
        public void SearchProductTest(String attribute , object value)
        {
            store1.AddNewProduct(founder1.Id, "RazerKeyboard", 449.99, 25, "Keyboards");
            IDictionary<String, Object> searchAttributes = new Dictionary<String, Object>()
                                                            {{ attribute, value }};
            try
            {
                List<Product> products = store1.SearchProduct(searchAttributes);
                Assert.NotEmpty(products);
            }
            catch (Exception ex)
            {
                Assert.Equal("No item has been found", ex.Message);
            }
        }

        [Fact]
        [Trait("Category","UnitTesting")]
        public void CloseOpenStroeTest()
        {
            this.storefacade.OpenNewStore(founder1, store1.Name);
            Store tempstore = storefacade.Stores.Values.First();
            Assert.True(storefacade.Stores.ContainsKey(tempstore.Id) & tempstore.Active);
            Assert.Throws<Exception>(()=>this.storefacade.CloseStore(founder2, tempstore.Id));
            Assert.True(storefacade.Stores.ContainsKey(tempstore.Id) & tempstore.Active);
            this.storefacade.CloseStore(founder1, tempstore.Id);
            Assert.False(tempstore.Active);
        }

        [Fact]
        [Trait("Category","UnitTesting")]
        public void AddRemoveProduct()
        {
            this.storefacade.OpenNewStore(founder1, store1.Name);
            Store tempstore = storefacade.Stores.Values.First();
            Console.WriteLine(tempstore.Name);
            Assert.Throws<Exception>(()=>storefacade.AddProductToStore(founder2.Id, tempstore.Id, "RazerKeyboard", 449.99, 25, "Keyboard"));
            storefacade.AddProductToStore(founder1.Id, tempstore.Id, "RazerKeyboard", 449.99, 25, "Keyboard");
            Product tempproduct = tempstore.InventoryManager.Products.Values.First();
            Assert.NotNull(tempproduct);
            Assert.Throws<Exception>(() => storefacade.RemoveProductFromStore(founder2.Id, store1.Id, tempproduct.Id));
            Assert.True(tempstore.InventoryManager.Products.ContainsKey(tempproduct.Id));
            storefacade.RemoveProductFromStore(founder1.Id, tempstore.Id, tempproduct.Id);
            Assert.False(store1.InventoryManager.Products.ContainsKey(tempproduct.Id));
        }

        // @toadd : 1) setPermissions
                  //2) removePermissions
    }

}
