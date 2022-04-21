using System;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Controllers;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;

namespace eCommerce.src.ServiceLayer
{

    public interface IeCommerceAPI : IUserController, IGuestController, IRegisteredUserController, ISystemAdminController /*TODO:, IStoreStaffInterface*/{ }
    public class eCommerceSystem : IeCommerceAPI
    {
        public IUserController UserController;
        public IGuestController GuestController { get; }
        public IRegisteredUserController RegisteredUserController { get; }
        public ISystemAdminController SystemAdminController { get; }

        public eCommerceSystem()
        {
            SystemFacade systemFacade = new SystemFacade();

            UserController = new UserController(systemFacade);
            GuestController = new GuestController(systemFacade);
            RegisteredUserController = new RegisteredUserController(systemFacade);
            SystemAdminController = new SystemAdminController(systemFacade);
        }

        #region User Related Methods
        public Result AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
            return UserController.AddProductToCart(userId, productId, quantity, storeId);
        }

        public Result<RegisteredUserSO> AddSystemAdmin(string sysAdminId, string userName)
        {
            return SystemAdminController.AddSystemAdmin(sysAdminId, userName);
        }

        public Result<double> GetTotalShoppingCartPrice(string userId)
        {
            return UserController.GetTotalShoppingCartPrice(userId);
        }

        public Result<UserHistorySO> GetUserPurchaseHistory(string userId)
        {
            return RegisteredUserController.GetUserPurchaseHistory(userId);
        }

        public Result<ShoppingCartSO> GetUserShoppingCart(string userId)
        {
            return UserController.GetUserShoppingCart(userId);
        }

        public Result<GuestUserSO> Login()
        {
            return GuestController.Login();
        }

        public Result<RegisteredUserSO> Login(string userName, string password)
        {
            return RegisteredUserController.Login(userName, password);
        }

        public Result Logout(string userId)
        {
            return UserController.Logout(userId);
        }

        public Result<ShoppingCartSO> Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            return UserController.Purchase(userId, paymentDetails, deliveryDetails);
        }

        public Result<RegisteredUserSO> Register(string username, string password)
        {
            return GuestController.Register(username, password);
        }

        public Result<RegisteredUserSO> RemoveSystemAdmin(string sysAdminId, string userName)
        {
            return SystemAdminController.RemoveSystemAdmin(sysAdminId, userName);
        }

        public Result UpdateShoppingCart(string userId, string storeId, string productId, int quantity)
        {
            return UserController.UpdateShoppingCart(userId, storeId, productId, quantity);
        }

        Result<UserHistorySO> ISystemAdminController.GetUserPurchaseHistory(string sysAdminId, string userId)
        {
            return SystemAdminController.GetUserPurchaseHistory(sysAdminId, userId);
        }

        Result<UserHistorySO> ISystemAdminController.GetStorePurchaseHistory(string sysAdminId, string storeId)
        {
            return SystemAdminController.GetStorePurchaseHistory(sysAdminId, storeId);
        }
        #endregion
    }
}
