using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.DomainLayer.User.Roles;
using System.Threading;

namespace eCommerce.src.DomainLayer
{
    public interface ISystemFacade
    {
        StoreService OpenNewStore(String storeName, String userID);
        void CloseStore(string userID, string storeID);
        #region Inventory Management
        String AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        void RemoveProductFromStore(String userID, String storeID, String productID);
        void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        List<ProductService> SearchProduct(IDictionary<String, Object> productDetails);
        #endregion

        #region Staff Management
        UserHistorySO GetStorePurchaseHistory(string userID, string storeID, bool systemAdmin = false);
        void AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID);
        void AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID);
        void RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID);
        void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        void RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Dictionary<IStaffService, PermissionService> GetStoreStaff(String ownerID, String storeID);
        #endregion

        #region User Actions - UserFacade
        GuestUserSO Login();
        RegisteredUserSO Register(string username, string password);
        RegisteredUserSO Login(String userName, String password);
        void Logout(String userId);
        void AddProductToCart(string userId, String productId, int quantity, String storeId);
        Double GetTotalShoppingCartPrice(string userId);
        UserHistorySO GetUserPurchaseHistory(string userId);
        ShoppingCartSO GetUserShoppingCart(string userId);
        ShoppingCartSO Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails);
        void UpdateShoppingCart(string userId, string storeId, String productId, int quantity);
        #endregion

        #region System Management
        RegisteredUserSO AddSystemAdmin(string userName);
        RegisteredUserSO RemoveSystemAdmin(string userName);
        Boolean IsSystemAdmin(String userId);
        #endregion

    }

    public class SystemFacade : ISystemFacade
    {
        public UserFacade userFacade { get; }
        public StoreFacade storeFacade { get; }
        private readonly object my_lock = new object();

        public SystemFacade()
        {
            userFacade = new UserFacade();
            storeFacade = new StoreFacade();
        }

        #region UserFacadeMethods
        public GuestUserSO Login()
        {
            GuestUser user = userFacade.Login();
            return new GuestUserSO(user);
        }

        public RegisteredUserSO Register(string username, string password)
        {
            RegisteredUser user = userFacade.Register(username, password);
            return new RegisteredUserSO(user);
        }
        public RegisteredUserSO Login(String userName, String password)
        {
            RegisteredUser user = userFacade.Login(userName, password);
            return new RegisteredUserSO(user);
        }

        public void Logout(String userId) { userFacade.Logout(userId); }

        public void AddProductToCart(string userId, String productId, int quantity, String storeId)
        {
            Store.Store store = storeFacade.GetStore(storeId);
            Product product = store.GetProduct(productId);
            userFacade.AddProductToCart(userId, product, quantity, store);
        }

        public RegisteredUserSO AddSystemAdmin(string userName)
        {
            RegisteredUser user = userFacade.AddSystemAdmin(userName);
            return new RegisteredUserSO(user);
        }

        public Boolean IsSystemAdmin(String userId)
        {
            return userFacade.SystemAdmins.ContainsKey(userId);
        }


        public Double GetTotalShoppingCartPrice(string userId)
        {
            return userFacade.GetTotalShoppingCartPrice(userId);
        }

        public UserHistorySO GetUserPurchaseHistory(string userId)
        {
            History history = userFacade.GetUserPurchaseHistory(userId);
            return new UserHistorySO(history);

        }

        public ShoppingCartSO GetUserShoppingCart(string userId)
        {
            ShoppingCart shoppingCart = userFacade.GetUserShoppingCart(userId);
            return new ShoppingCartSO(shoppingCart);
        }

        public ShoppingCartSO Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            try
            {
                Monitor.TryEnter(my_lock);
                try
                {
                    ShoppingCart purchasedCart = userFacade.Purchase(userId, paymentDetails, deliveryDetails);

                    ConcurrentDictionary<String, ShoppingBag> purchasedBags = purchasedCart.ShoppingBags;
                    foreach (var bag in purchasedBags)
                    {
                        Store.Store store = storeFacade.GetStore(bag.Key);
                        store.UpdateInventory(bag.Value);
                        store.History.AddPurchasedShoppingBag(bag.Value);
                    }
                    return new ShoppingCartSO(purchasedCart);
                }
                finally
                {
                    Monitor.Exit(my_lock);
                }
            }
            catch (SynchronizationLockException SyncEx)
            {
                throw new Exception("A SynchronizationLockException occurred. Message:" + SyncEx.Message);
            }
        }

        public RegisteredUserSO RemoveSystemAdmin(string userName)
        {
            RegisteredUser user = userFacade.RemoveSystemAdmin(userName);
            return new RegisteredUserSO(user);
        }

        public void UpdateShoppingCart(string userId, string storeId, String productId, int quantity)
        {
            Store.Store resStore = storeFacade.GetStore(storeId);
            Product resProduct = resStore.GetProduct(productId);
            userFacade.UpdateShoppingCart(userId, resStore.Id, resProduct, quantity);
        }
        public Boolean isSystemAdmin(String userId)
        {
            return userFacade.SystemAdmins.ContainsKey(userId);
        }
        #endregion

        #region StoreFacadeMethods
        public StoreService OpenNewStore(String storeName, String userID)
        {
            if (userFacade.RegisteredUsers.TryGetValue(userID, out RegisteredUser founder))  // Check if userID is a registered user
            {
                Store.Store s = storeFacade.OpenNewStore(founder, storeName);
                return new StoreService(s.Id, s.Name, s.Founder.GetId(), new LinkedList<string>(s.Owners.Keys), new LinkedList<string>(s.Managers.Keys), new UserHistorySO(s.History), s.Rate, s.NumberOfRates);
            }
            else
            {
                throw new Exception($"Failed to open store {storeName}: {userID} is not a registered user");
            }
        }

        public void CloseStore(string userID, string storeID)
        {
            if (userFacade.RegisteredUsers.TryGetValue(userID, out RegisteredUser founder))  // Check if userID is a registered user
            {
                storeFacade.CloseStore(founder, storeID);
            }
            else
            {
                throw new Exception($"Failed to close store with id {storeID}: {userID} is not a registered user");
            }
        }

        public String AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            return storeFacade.AddProductToStore(userID, storeID, productName, price, initialQuantity, category, keywords);
        }

        public void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details)
        {
            storeFacade.EditProductDetails(userID, storeID, productID, details);
        }

        public List<ProductService> SearchProduct(IDictionary<String, Object> productDetails)
        {
            List<Product> products = storeFacade.SearchProduct(productDetails);
            List<ProductService> result = new List<ProductService>();
            foreach (Product p in products)
            {
                result.Add(new ProductService(p.Id, p.Name, p.Price, p.Quantity, p.Category));
            }
            return result;
        }

        public void AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID)
        {
            if (userFacade.RegisteredUsers.TryGetValue(addedOwnerID, out RegisteredUser futureOwner))  // Check if addedOwnerID is a registered user
            {
                storeFacade.AddStoreOwner(futureOwner, currentlyOwnerID, storeID);
            }
            else
            {
                throw new Exception($"Failed to appoint store owner: {addedOwnerID} is not a registered user");
            }
        }

        public void AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID)
        {
            if (userFacade.RegisteredUsers.TryGetValue(addedManagerID, out RegisteredUser futureManager))  // Check if addedManagerID is a registered user
            {
                storeFacade.AddStoreManager(futureManager, currentlyOwnerID, storeID);
            }
            else
            {
                throw new Exception($"Failed to appoint store manager: {addedManagerID} is not a registered user");
            }
        }

        public void RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID)
        {
            if (userFacade.RegisteredUsers.ContainsKey(removedManagerID))  // Check if addedManagerID is a registered user
            {
                storeFacade.RemoveStoreManager(removedManagerID, currentlyOwnerID, storeID);
            }
            else
            {
                throw new Exception($"Failed to remove store manager: {removedManagerID} is not a registered user");
            }
        }

        public void RemoveProductFromStore(String userID, String storeID, String productID)
        {
            storeFacade.RemoveProductFromStore(userID, storeID, productID);
        }

        public void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            storeFacade.SetPermissions(storeID, managerID, ownerID, permissions);
        }

        public void RemovePermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            storeFacade.RemovePermissions(storeID, managerID, ownerID, permissions);
        }

        public Dictionary<IStaffService, PermissionService> GetStoreStaff(string userID, string storeID)
        {
            Dictionary<IStaff, Permission> storeStaff = storeFacade.GetStoreStaff(userID, storeID);
            Dictionary<IStaffService, PermissionService> storeStaffResult = new Dictionary<IStaffService, PermissionService>();
            foreach (var user in storeStaff)
            {
                storeStaffResult.Add(new IStaffService(user.Key.GetId()), new PermissionService(user.Value.functionsBitMask, user.Value.isOwner));
            }
            return storeStaffResult;
        }

        public UserHistorySO GetStorePurchaseHistory(string userID, string storeID, bool systemAdmin = false)
        {
            History history = storeFacade.GetStorePurchaseHistory(userID, storeID, systemAdmin);
            return new UserHistorySO(history);
        }
        #endregion
    }
}