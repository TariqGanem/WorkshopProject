using eCommerce.src.DomainLayer.User;
using eCommerce.src.DomainLayer.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.DomainLayer
{
    public interface ISystemFacade : IUserFacade //TODO: , IStoreFacade
    {
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

        #region Guest methods 
        public GuestUser Login()
        {
            return userFacade.EnterSystem();
        }

        public void ExitSystem(string id)
        {
            userFacade.ExitSystem(id);
        }

        public RegisteredUser Register(string username, string email, string password)
        {
            return userFacade.Register(username, email, password);
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
