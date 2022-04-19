using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace eCommerce.src.DomainLayer
{
    public interface ISystemFacade
    {
        #region User Actions - UserFacade
        GuestUser Login();
        RegisteredUser Register(string username, string password);
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
        #region parameters
        private UserFacade userFacade;
        private StoreFacade storeFacade;
        #endregion

        #region constructors
        public SystemFacade()
        {
            userFacade = new UserFacade();
            storeFacade = new StoreFacade();
        }
        #endregion


        #region UserFacadeMethods
        public GuestUser Login()
        {
            return userFacade.Login();
        }

        public RegisteredUser Register(string username, string password)
        {
            return userFacade.Register(username, password);
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
                // TODO - store.UpdateInventory(bag.Value);
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
    }
}