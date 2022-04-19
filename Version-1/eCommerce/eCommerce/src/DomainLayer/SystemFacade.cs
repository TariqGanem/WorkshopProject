using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer
{
    internal interface ISystemFacade : IUserFacade //TODO: , IStoreFacade
    {
    }

    internal class SystemFacade : ISystemFacade
    {
        private UserFacade userFacade;
        private StoreFacade storeFacade;
        public SystemFacade()
        {
            userFacade = new UserFacade();
            storeFacade = new StoreFacade();
        }

        #region Guest methods 
        public GuestUser EnterSystem()
        {
            return userFacade.EnterSystem();
        }

        public void ExitSystem(string id)
        {
            userFacade.ExitSystem(id);
        }
        #endregion

        #region UserFacadeMethods
        public RegisteredUser Login(String userName, String password) { return userFacade.Login(userName, password); }

        public void Logout(String userId) { userFacade.Logout(userId); }

        public bool AddProductToCart(string userId, Product product, int quantity, Store.Store store)
        {
            throw new NotImplementedException();
        }

        public RegisteredUser AddSystemAdmin(string userName)
        {
            throw new NotImplementedException();
        }

        public double GetTotalShoppingCartPrice(string userID)
        {
            throw new NotImplementedException();
        }

        public History GetUserPurchaseHistory(string userId)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetUserShoppingCart(string userId)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            throw new NotImplementedException();
        }

        public RegisteredUser Register(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public RegisteredUser RemoveSystemAdmin(string userName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateShoppingCart(string userId, string storeId, Product product, int quantity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
