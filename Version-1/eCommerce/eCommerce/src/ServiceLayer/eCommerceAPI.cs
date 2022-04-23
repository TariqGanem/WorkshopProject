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

    public interface IeCommerceAPI : IUserController, IGuestController, IRegisteredUserController, ISystemAdminController, IStoreStaffController { }
    public class eCommerceSystem : IeCommerceAPI
    {
        public IUserController UserController;
        public IGuestController GuestController { get; }
        public IRegisteredUserController RegisteredUserController { get; }
        public SystemAdminController SystemAdminController { get; }
        public IStoreStaffController StoreStaffController { get; }

        public eCommerceSystem()
        {
            SystemFacade systemFacade = new SystemFacade();

            UserController = new UserController(systemFacade);
            GuestController = new GuestController(systemFacade);
            RegisteredUserController = new RegisteredUserController(systemFacade);
            SystemAdminController = new SystemAdminController(systemFacade);
            StoreStaffController = new StoreStaffController(systemFacade);
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
        #endregion

        #region Store Related Methods
        public Result AddStoreOwner(String addedOwnerID, String currentlyOwnerID, String storeID)
        {
            return StoreStaffController.AddStoreOwner(addedOwnerID, currentlyOwnerID, storeID);
        }
        public Result AddStoreManager(String addedManagerID, String currentlyOwnerID, String storeID)
        {
            return StoreStaffController.AddStoreManager(addedManagerID, currentlyOwnerID, storeID);
        }
        public Result RemoveStoreManager(String removedManagerID, String currentlyOwnerID, String storeID)
        {
            return StoreStaffController.RemoveStoreManager(removedManagerID, currentlyOwnerID, storeID);
        }
        public Result SetPermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            return StoreStaffController.SetPermissions(storeID, managerID, ownerID, permissions);
        }
        public Result RemovePermissions(String storeID, String managerID, String ownerID, LinkedList<int> permissions)
        {
            return StoreStaffController.RemovePermissions(storeID, managerID, ownerID, permissions);
        }
        public Result<Dictionary<IStaffService, PermissionService>> GetStoreStaff(String ownerID, String storeID)
        {
            return StoreStaffController.GetStoreStaff(ownerID, storeID);
        }
        public Result<String> AddProductToStore(String userID, String storeID, String productName, double price, int initialQuantity, String category, LinkedList<String> keywords = null)
        {
            return StoreStaffController.AddProductToStore(userID, storeID, productName, price, initialQuantity, category, keywords);
        }
        public Result RemoveProductFromStore(String userID, String storeID, String productID)
        {
            return StoreStaffController.RemoveProductFromStore(userID, storeID, productID);
        }
        public Result EditProductDetails(String userID, String storeID, String productID, IDictionary<String, Object> details)
        {
            return StoreStaffController.EditProductDetails(userID, storeID, productID, details);
        }
        public Result<List<ProductService>> SearchProduct(IDictionary<String, Object> productDetails)
        {
            return StoreStaffController.SearchProduct(productDetails);
        }
        public Result<UserHistorySO> GetStorePurchaseHistory(String ownerID, String storeID, Boolean isSystemAdmin = false)
        {
            return !isSystemAdmin ? StoreStaffController.GetStorePurchaseHistory(ownerID, storeID) :
                                SystemAdminController.GetStorePurchaseHistory(ownerID, storeID);
        }

        public Result<StoreService> OpenNewStore(string storeName, string userId)
        {
            return RegisteredUserController.OpenNewStore(storeName, userId);
        }

        public Result CloseStore(string userId, string storeId)
        {
            return RegisteredUserController.CloseStore(userId, storeId);
        }
        #endregion
    }
}
