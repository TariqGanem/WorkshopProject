using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.Store;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.User.Roles;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using eCommerceIntegrationTests.Utils;
using System;
using System.Collections.Generic;
using Xunit;

namespace eCommerceIntegrationTests
{
    public class SystemFacadeTest : XeCommerceTestCase
    {
        public SystemFacade Facade { get; }
        public StoreFacade StoresFacade { get; }
        public UserFacade UserFacade { get; }
        public Store TestStore { get; }
        public RegisteredUser Founder { get; }
        public RegisteredUser RegisteredUser { get; }
        public GuestUser GuestUser { get; }

        public SystemFacadeTest() : base()
        {
            // Facade to check
            Facade = new SystemFacade("email","pass");

            // Facades to integrate
            StoresFacade = Facade.storeFacade;
            UserFacade = Facade.userFacade;

            // Initialize store for tests
            Founder = new RegisteredUser("yazan@hotmale.com", "qwerty1");
            TestStore = new Store("The Testore", Founder);
            StoresFacade.Stores.TryAdd(TestStore.Id, TestStore);

            // Initialize users for testing
            RegisteredUser = new RegisteredUser("tariq@gmail.com", "StrongPassword");
            GuestUser = new GuestUser();

            // Add all users to UserFacade
            UserFacade.RegisteredUsers.TryAdd(Founder.Id, Founder);
            UserFacade.RegisteredUsers.TryAdd(RegisteredUser.Id, RegisteredUser);
            UserFacade.GuestUsers.TryAdd(GuestUser.Id, GuestUser);
        }

        [Fact(Skip = "Pivotal changes in current version than the previos one , Changes should be made on test cast to cope with adding the Notifications")]
        public void OpenNewStoreTest()
        {
            // Registered User can open multiple stores and with the same name
            Facade.OpenNewStore("NiceToMeat", Founder.Id);
            Facade.OpenNewStore("NiceToMeat", Founder.Id);

            // Check both stores were added and with the same name
            // 3 because we've added the "The Teststore" store
            Assert.Equal(3, StoresFacade.Stores.Count);

            int count = 0;
            foreach (var store in StoresFacade.Stores)
            {
                if (store.Value.Name == "NiceToMeat")
                {
                    count++;
                }
            }

            Assert.Equal(2, count);

            // Registered User can open store
            Facade.OpenNewStore("Store2", RegisteredUser.Id);

            // Guest User cannot open a store
            Assert.Throws<Exception>(() => Facade.OpenNewStore("NoCannot", GuestUser.Id));

            // Check number of stores are correct
            Assert.Equal(4, StoresFacade.Stores.Count);

            foreach (var store in StoresFacade.Stores)
            {
                Assert.NotEqual("NoCannot", store.Value.Name);
            }
        }

        [Fact()]
        public void AddStoreOwnerTest()
        {
            // Success - Founder can add registered user to be store owner
            Facade.AddStoreOwner(RegisteredUser.Id, Founder.Id, TestStore.Id);

            // Fail - registered user is already an owner in the store
            Assert.Throws<Exception>(() => Facade.AddStoreOwner(RegisteredUser.Id, Founder.Id, TestStore.Id));

            // Check that founder and registered user are store owners
            Assert.True(TestStore.Owners.ContainsKey(Founder.Id));
            Assert.True(TestStore.Owners.ContainsKey(RegisteredUser.Id));

            // Fail - guest owner can not be a store owner
            Assert.Throws<Exception>(() => Facade.AddStoreOwner(GuestUser.Id, Founder.Id, TestStore.Id));
            Assert.False(TestStore.Owners.ContainsKey(GuestUser.Id));
        }

        [Fact()]
        public void AddStoreOwnerTest2()
        {
            StoreManager manager = new StoreManager(RegisteredUser, TestStore, new Permission(), TestStore.Founder);
            Facade.AddStoreOwner(RegisteredUser.Id, Founder.Id, TestStore.Id);
            Assert.False(TestStore.Managers.ContainsKey(manager.GetId()));
        }

        [Fact()]
        public void AddStoreManagerTest()
        {
            // Success - Founder can add registered user to be store manager
            Facade.AddStoreManager(RegisteredUser.Id, Founder.Id, TestStore.Id);

            // Fail - registered user is already a manager in the store
            Assert.Throws<Exception>(() => Facade.AddStoreManager(RegisteredUser.Id, Founder.Id, TestStore.Id));

            // Check that registered user is a store manager
            Assert.True(TestStore.Managers.ContainsKey(RegisteredUser.Id));

            // Fail - guest owner can not be a store manager
            Assert.Throws<Exception>(() => Facade.AddStoreManager(GuestUser.Id, Founder.Id, TestStore.Id));
            Assert.False(TestStore.Managers.ContainsKey(GuestUser.Id));
        }

        [Fact()]
        public void AddStoreManagerTest2()
        {
            StoreManager manager = new StoreManager(RegisteredUser, TestStore, new Permission(), TestStore.Founder);
            TestStore.Managers.TryAdd(manager.GetId(), manager);

            RegisteredUser user = new RegisteredUser("eran@gmail.com", "Password");
            UserFacade.RegisteredUsers.TryAdd(user.Id, user);

            //Fail - manager does not have permission to apoint user as manager
            Assert.Throws<Exception>(() => Facade.AddStoreManager(user.Id, manager.GetId(), TestStore.Id));
            Assert.False(TestStore.Managers.ContainsKey(user.Id));

            //Success - manager can add user as store manager
            manager.SetPermission(Methods.AddStoreManager, true);
            Facade.AddStoreManager(user.Id, manager.GetId(), TestStore.Id);
            Assert.True(TestStore.Managers.ContainsKey(user.Id));
        }

        [Fact()]
        public void RemoveStoreManagerTest()
        {
            StoreManager manager = new StoreManager(RegisteredUser, TestStore, new Permission(), TestStore.Founder);
            TestStore.Managers.TryAdd(manager.GetId(), manager);

            // Success - founder appointed manager as store manager therefore can remove him
            Facade.RemoveStoreManager(manager.GetId(), Founder.Id, TestStore.Id);
            Assert.False(TestStore.Managers.ContainsKey(manager.GetId()));
        }

        [Fact()]
        public void RemoveStoreManagerTest2()
        {
            RegisteredUser user = new RegisteredUser("randomuser@gmail.com", "Password");
            UserFacade.RegisteredUsers.TryAdd(user.Id, user);

            StoreManager manager = new StoreManager(user, TestStore, new Permission(), TestStore.Founder);
            TestStore.Managers.TryAdd(manager.GetId(), manager);

            // Fail - registered user did not appointe manager as store manager therefore cannot remove him
            Assert.Throws<Exception>(() => Facade.RemoveStoreManager(manager.GetId(), RegisteredUser.Id, TestStore.Id));
            Assert.True(TestStore.Managers.ContainsKey(manager.GetId()));
        }

        public void AddProductToCartTest(int productQuantity, Boolean expectedResult, Boolean expectedResult2)
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void AddProductToCartTest2()
        {
            // Add products to store
            Product product = new Product("Banana", 5.7, "Fruits", 100);
            TestStore.InventoryManager.Products.TryAdd(product.Id, product);

            Assert.Throws<Exception>(() => Facade.AddProductToCart(RegisteredUser.Id, product.Id, 5, "No Such Store"));
            Assert.Throws<Exception>(() => RegisteredUser.ShoppingCart.GetShoppingBag(TestStore.Id));
            Assert.Empty(RegisteredUser.ShoppingCart.ShoppingBags);
        }

        [Theory()]
        [Trait("Category", "Unit")]
        [InlineData(5, 5)]      // Success
        [InlineData(10, 5)]     // Fail : Higher quantity than quantity in store
        [InlineData(0, 0)]      // Success
        [InlineData(-1, 0)]     // Success
        public void UpdateShoppingCartTest(int quantity, int expectedQuantity)
        {
            // Add products to store
            Product product = new Product("Banana", 5.7, "Fruits", 5);
            TestStore.InventoryManager.Products.TryAdd(product.Id, product);

            // Add product to user shopping bag
            Facade.AddProductToCart(RegisteredUser.Id, product.Id, 5, TestStore.Id);

            if (quantity > 5)
            {
                Assert.Throws<Exception>(() => Facade.UpdateShoppingCart(RegisteredUser.Id, TestStore.Id, product.Id, quantity));
            }
            else
            {
                Facade.UpdateShoppingCart(RegisteredUser.Id, TestStore.Id, product.Id, quantity);
            }

            // Check bag
            RegisteredUser.ShoppingCart.ShoppingBags.TryGetValue(TestStore.Id, out ShoppingBag bag);
            bool res;
            int updatedQuantity;
            if (bag != null)
                res = bag.Products.TryGetValue(product, out updatedQuantity);
            else
            {
                res = false;
                updatedQuantity = 0;
            }

            if (res)
            {
                Assert.Equal(expectedQuantity, updatedQuantity);
            }
            else if (expectedQuantity == 0 && bag != null)
            {
                Assert.False(bag.Products.ContainsKey(product));
            }
            else
            {
                Assert.Empty(RegisteredUser.ShoppingCart.ShoppingBags);
            }
        }

        [Fact(Skip = "TOO MUCH CHANGES TO DOMAIN")]
        public void PurchaseTest()
        {
            Store store2 = this.StoresFacade.OpenNewStore(Founder,"NiceToMeat");
            // Add products to store
            //Product product = new Product("Banana", 5.7, "Fruits", 5);
            //Product product2 = new Product("Apple", 4.9, "Fruits", 5);
            string product1 = store2.AddNewProduct(Founder.Id, "Bannan", 5.7, 5, "Fruits");
            string product2 = store2.AddNewProduct(Founder.Id, "Apple", 4.9, 5, "Fruits");
            // Add product to user shopping bag
            Facade.AddProductToCart(RegisteredUser.Id, product1, 2, store2.Id);
            Facade.AddProductToCart(RegisteredUser.Id, product2, 1, store2.Id);

            IDictionary<String, Object> paymentDetails = new Dictionary<String, Object>();
            IDictionary<String, Object> deliveryDetails = new Dictionary<String, Object>();

            // The bag is not purchased yet
            Assert.Empty(RegisteredUser.History.ShoppingBags);

            ShoppingCartSO res = null;//Facade.Purchase(RegisteredUser.Id, paymentDetails, deliveryDetails);

            Assert.Equal(16.3, res.TotalPrice);
            Assert.Empty(RegisteredUser.ShoppingCart.ShoppingBags);

            Assert.Single(RegisteredUser.History.ShoppingBags);

            // Check User Historys bags
            History history = RegisteredUser.History;
            LinkedList<ShoppingBag> bags = history.ShoppingBags;
            ShoppingBag bagSO = bags.First.Value;

            Assert.Equal(16.3, bagSO.TotalBagPrice);

            //Check Store History
            History storeHistory = store2.History;
            LinkedList<ShoppingBag> storeBags = storeHistory.ShoppingBags;
            ShoppingBag storeBagSO = storeBags.First.Value;

            Assert.Equal(16.3, storeBagSO.TotalBagPrice);
        }

        [Fact(Skip = "Pivotal changes in current version than the previos one , Changes should be made on test cast to cope with adding the Notifications")]
        public void PurchaseTest2()
        {
            // Open another store
            Store store2 = new Store("Testore2", Founder);
            StoresFacade.Stores.TryAdd(store2.Id, store2);

            // Add products to store
            Product product = new Product("Banana", 5.7, "Fruits", 5);
            Product product2 = new Product("Apple", 4.9, "Fruits", 5);
            TestStore.InventoryManager.Products.TryAdd(product.Id, product);
            store2.InventoryManager.Products.TryAdd(product2.Id, product2);

            // Add product to user shopping bag
            Facade.AddProductToCart(RegisteredUser.Id, product.Id, 2, TestStore.Id);
            Facade.AddProductToCart(RegisteredUser.Id, product2.Id, 1, store2.Id);

            IDictionary<String, Object> paymentDetails = new Dictionary<String, Object>();
            IDictionary<String, Object> deliveryDetails = new Dictionary<String, Object>();

            // The bag is not purchased yet
            Assert.Empty(RegisteredUser.History.ShoppingBags);

            ShoppingCartSO res = null;//Facade.Purchase(RegisteredUser.Id, paymentDetails, deliveryDetails);

            Assert.Equal(16.3, res.TotalPrice);

            Assert.Equal(2, RegisteredUser.History.ShoppingBags.Count);

            //Check Stores History
            History storeHistory = TestStore.History;
            LinkedList<ShoppingBag> storeBags = storeHistory.ShoppingBags;
            ShoppingBag storeBag = storeBags.First.Value;

            Assert.Equal(11.4, storeBag.TotalBagPrice);

            History store2History = store2.History;
            LinkedList<ShoppingBag> store2Bags = store2History.ShoppingBags;
            ShoppingBag store2Bag = store2Bags.First.Value;

            Assert.Equal(4.9, store2Bag.TotalBagPrice);
        }

        [Theory()]
        [InlineData(true)]
        [InlineData(false)]
        public void NotificationPurchaseTest(Boolean loggedin)
        {
            NotificationPublisher notificationManager = new NotificationPublisher(TestStore);
            TestStore.NotificationPublisher = notificationManager;

            // Add products to store
            Product product = new Product("Banana", 5.7, "Fruits",5 );
            Product product2 = new Product("Apple", 4.9, "Fruits",5 );
            TestStore.InventoryManager.Products.TryAdd(product.Id, product);
            TestStore.InventoryManager.Products.TryAdd(product2.Id, product2);

            // Update products Notification Manager manually
            product.NotificationPublisher = notificationManager;
            product2.NotificationPublisher = notificationManager;

            // Add product to user shopping bag
            Facade.AddProductToCart(RegisteredUser.Id, product.Id, 2, TestStore.Id);
            Facade.AddProductToCart(RegisteredUser.Id, product2.Id, 1, TestStore.Id);

            IDictionary<String, Object> paymentDetails = new Dictionary<String, Object>();
            IDictionary<String, Object> deliveryDetails = new Dictionary<String, Object>();

            Founder.Active = loggedin;

            Facade.Purchase(RegisteredUser.Id, paymentDetails, deliveryDetails);

            if (loggedin)
            {
                Assert.Empty(Founder.PendingNotification);
            }
            else
            {
                Assert.Equal(2, Founder.PendingNotification.Count); // one for each product and not as quantity

                foreach (Notification n in Founder.PendingNotification)
                {
                    Assert.False(n.isOpened);
                    Assert.True(n.isStoreStaff);
                    Assert.Equal(DateTime.Now.ToString("MM/dd/yyyy HH:mm"), n.Date.ToString("MM/dd/yyyy HH:mm"));
                    Assert.Equal(Founder.Id, n.ClientId);
                }
            }
        }

        [Theory()]
        [InlineData(true)]
        [InlineData(false)]
        public void NotificationCloseStoreTest(Boolean loggedin)
        {
            // Create Notification Manager for store
            NotificationPublisher notificationManager = new NotificationPublisher(TestStore);
            TestStore.NotificationPublisher = notificationManager;

            // Add 1 owner and 1 manager to store
            StoreOwner owner = new StoreOwner(RegisteredUser, TestStore.Id, TestStore.Founder);
            RegisteredUser user = new RegisteredUser("randomuser@gmail.com", "Password");
            UserFacade.RegisteredUsers.TryAdd(user.Id, user);
            StoreManager manager = new StoreManager(user, TestStore, new Permission(), TestStore.Founder);
            TestStore.Owners.TryAdd(owner.GetId(), owner);
            TestStore.Managers.TryAdd(manager.GetId(), manager);

            Founder.Active = loggedin;
            owner.User.Active = loggedin;
            manager.User.Active = loggedin;

            Facade.CloseStore(Founder.Id, TestStore.Id);

            if (loggedin)
            {
                Assert.Empty(Founder.PendingNotification);
                Assert.Empty(owner.User.PendingNotification);
                Assert.Empty(manager.User.PendingNotification);
            }
            else
            {
                Assert.Single(Founder.PendingNotification);
                Assert.Single(owner.User.PendingNotification);
                Assert.Single(manager.User.PendingNotification);

                foreach (Notification n in Founder.PendingNotification)
                {
                    Assert.False(n.isOpened);
                    Assert.True(n.isStoreStaff);
                    Assert.Equal(DateTime.Now.ToString("MM/dd/yyyy HH:mm"), n.Date.ToString("MM/dd/yyyy HH:mm"));
                    Assert.Equal(Founder.Id, n.ClientId);

                }

                foreach (Notification n in owner.User.PendingNotification)
                {
                    Assert.False(n.isOpened);
                    Assert.True(n.isStoreStaff);
                    Assert.Equal(DateTime.Now.ToString("MM/dd/yyyy HH:mm"), n.Date.ToString("MM/dd/yyyy HH:mm"));
                    Assert.Equal(owner.GetId(), n.ClientId);
                }

                foreach (Notification n in manager.User.PendingNotification)
                {
                    Assert.False(n.isOpened);
                    Assert.True(n.isStoreStaff);
                    Assert.Equal(DateTime.Now.ToString("MM/dd/yyyy HH:mm"), n.Date.ToString("MM/dd/yyyy HH:mm"));
                    Assert.Equal(manager.GetId(), n.ClientId);

                }
            }
        }

    }
}
