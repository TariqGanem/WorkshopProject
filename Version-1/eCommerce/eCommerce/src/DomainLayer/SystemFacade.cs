using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
<<<<<<< HEAD
using eCommerce.src.ServiceLayer.Objects;
=======
using eCommerce.src.DomainLayer.User.Roles;
>>>>>>> dev1-Version1

namespace eCommerce.src.DomainLayer
{
    public interface ISystemFacade
    {
        void OpenNewStore(String storeName, String userID);
        void CloseStore(string userID, string storeID);
        #region Inventory Management
        void AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null);
        void RemoveProductFromStore(String userID, String storeID, String productID);
        void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details);
        List<Product> SearchProduct(IDictionary<String, Object> productDetails);
        #endregion

        #region Staff Management
        StoreHistory GetStorePurchaseHistory(string userID, string storeID, bool systemAdmin = false);
        void AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID);
        void AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID);
        void RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID);
        void SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        void RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions);
        Dictionary<IStaff, Permission> GetStoreStaff(String ownerID, String storeID);
        #endregion

        #region User Actions - UserFacade
        GuestUserSO Login();
        RegisteredUserSO Register(string username, string password);
        RegisteredUser Login(String userName, String password);
        void Logout(String userId);
        void AddProductToCart(string userId, String productId, int quantity, String storeId);
        Double GetTotalShoppingCartPrice(string userId);
        History GetUserPurchaseHistory(string userId);
        ShoppingCart GetUserShoppingCart(string userId);
        ShoppingCart Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails);
        void UpdateShoppingCart(string userId, string storeId, String productId, int quantity);
        #endregion

        #region System Management
        RegisteredUser AddSystemAdmin(string userName);
        RegisteredUser RemoveSystemAdmin(string userName);
        Boolean IsSystemAdmin(String userId);
        #endregion

    }

    public class SystemFacade : ISystemFacade
    {
        private UserFacade userFacade;
        private StoreFacade storeFacade;

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
            return new RegisteredUserSO(user.Id, user.Active, user.ShoppingCart, username);
        }
        public RegisteredUser Login(String userName, String password) { return userFacade.Login(userName, password); }

        public void Logout(String userId) { userFacade.Logout(userId); }

        public void AddProductToCart(string userId, String productId, int quantity, String storeId)
        {
            Store.Store store = storeFacade.GetStore(storeId);
            Product searchProductRes = store.GetProduct(productId);
            Product product = searchProductRes;
            userFacade.AddProductToCart(userId, product, quantity, store);
        }

        public RegisteredUser AddSystemAdmin(string userName)
        {
            RegisteredUser result = userFacade.AddSystemAdmin(userName);
            return result;
        }

        public Boolean IsSystemAdmin(String userId)
        {
            return userFacade.SystemAdmins.ContainsKey(userId);
        }


        public Double GetTotalShoppingCartPrice(string userId)
        {
            return userFacade.GetTotalShoppingCartPrice(userId);
        }

        public History GetUserPurchaseHistory(string userId)
        {
            return userFacade.GetUserPurchaseHistory(userId);

        }

        public ShoppingCart GetUserShoppingCart(string userId)
        {
            return userFacade.GetUserShoppingCart(userId);
        }

        public ShoppingCart Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            // TODO - lock products ?
            ShoppingCart purchasedCart = userFacade.Purchase(userId, paymentDetails, deliveryDetails);

            ConcurrentDictionary<String, ShoppingBag> purchasedBags = purchasedCart.ShoppingBags;
            foreach (var bag in purchasedBags)
            {
                Store.Store store = storeFacade.GetStore(bag.Key);
                store.UpdateInventory(bag.Value);
                store.History.addShoppingBasket(bag.Value);
            }
            return purchasedCart;

        }

        public RegisteredUser RemoveSystemAdmin(string userName)
        {
            RegisteredUser result = userFacade.RemoveSystemAdmin(userName);
            return result;
        }

        public void UpdateShoppingCart(string userId, string storeId, String productId, int quantity)
        {
            Store.Store resStore = storeFacade.GetStore(storeId);
            Product resProduct = resStore.GetProduct(productId);
            userFacade.UpdateShoppingCart(userId, resStore.Id, resProduct, quantity);
        }
        #endregion

        #region StoreFacadeMethods
        public void OpenNewStore(String storeName, String userID)
        {
            if (userFacade.RegisteredUsers.TryGetValue(userID, out RegisteredUser founder))  // Check if userID is a registered user
            {
                storeFacade.OpenNewStore(founder, storeName);
            }
            throw new Exception($"Failed to open store {storeName}: {userID} is not a registered user");
        }

        public void CloseStore(string userID, string storeID)
        {
            if (userFacade.RegisteredUsers.TryGetValue(userID, out RegisteredUser founder))  // Check if userID is a registered user
            {
                storeFacade.CloseStore(founder, storeID);
            }
            throw new Exception($"Failed to close store with id {storeID}: {userID} is not a registered user");
        }

        public void AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            storeFacade.AddProductToStore(userID, storeID, productName, price, initialQuantity, category, keywords);
        }

        public void EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details)
        {
            storeFacade.EditProductDetails(userID, storeID, productID, details);
        }

        public List<Product> SearchProduct(IDictionary<String, Object> productDetails)
        {
            return storeFacade.SearchProduct(productDetails);
        }

        public void AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID)
        {
            if (userFacade.RegisteredUsers.TryGetValue(addedOwnerID, out RegisteredUser futureOwner))  // Check if addedOwnerID is a registered user
            {
                storeFacade.AddStoreOwner(futureOwner, currentlyOwnerID, storeID);
            }
            throw new Exception($"Failed to appoint store owner: {addedOwnerID} is not a registered user");
        }

        public void AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID)
        {
            if (userFacade.RegisteredUsers.TryGetValue(addedManagerID, out RegisteredUser futureManager))  // Check if addedManagerID is a registered user
            {
                storeFacade.AddStoreManager(futureManager, currentlyOwnerID, storeID);
            }
            throw new Exception($"Failed to appoint store manager: {addedManagerID} is not a registered user");
        }

        public void RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID)
        {
            if (userFacade.RegisteredUsers.ContainsKey(removedManagerID))  // Check if addedManagerID is a registered user
            {
                storeFacade.RemoveStoreManager(removedManagerID, currentlyOwnerID, storeID);
            }
            throw new Exception($"Failed to remove store manager: {removedManagerID} is not a registered user");
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

        public Dictionary<IStaff, Permission> GetStoreStaff(string userID, string storeID)
        {
            return storeFacade.GetStoreStaff(userID, storeID);
        }

        public StoreHistory GetStorePurchaseHistory(string userID, string storeID, bool systemAdmin = false)
        {
            return storeFacade.GetStorePurchaseHistory(userID, storeID, systemAdmin);
        }
        #endregion
    }
}