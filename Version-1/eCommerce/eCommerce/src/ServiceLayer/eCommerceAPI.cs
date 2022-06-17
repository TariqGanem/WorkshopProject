using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.Notifications;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ExternalSystems;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Controllers;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using Newtonsoft.Json;

namespace eCommerce.src.ServiceLayer
{

    public interface IeCommerceAPI : IUserController, IGuestController, IRegisteredUserController, ISystemAdminController, IStoreStaffController { }
    public class eCommerceSystem 
    {
        public IUserController UserController { get; set; }
        public IGuestController GuestController { get; set; }
        public IRegisteredUserController RegisteredUserController { get; set; }
        public SystemAdminController SystemAdminController { get; set; }
        public IStoreStaffController StoreStaffController { get; set; }
        public SystemFacade systemFacade { get; set; }
        public NotificationsService notificationsService { get; set; }

        public IDataController dataController { get; set; }

        public eCommerceSystem(String config_path = @"..\..\src\Config.json" , string configData = "")
        {
            Config config;
            if (!(configData.Equals(String.Empty)))
            {
                config = JsonConvert.DeserializeObject<Config>(configData);

            }
            else
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(config_path));

            }

            //validate JSON
            if ((config.externalSystem_url is null) || (config.mongoDB_url is null)
                || (config.password is null) || (config.email is null) || (config.dbenv is null))
            {
                Logger.GetInstance().LogError("Invalid JSON format - One or more missing attribute has been found in the config JSON");
                Environment.Exit(1);
            }

            DBUtil.getInstance(config.mongoDB_url, config.dbenv);
            Proxy.getInstance(config.externalSystem_url);
            systemFacade = new SystemFacade(config.email , config.password);
            UserController = new UserController(systemFacade);
            GuestController = new GuestController(systemFacade);
            RegisteredUserController = new RegisteredUserController(systemFacade);
            SystemAdminController = new SystemAdminController(systemFacade);
            StoreStaffController = new StoreStaffController(systemFacade);
            dataController = new DataController(systemFacade);
        }

        // Guest User Functionality
        public Result<GuestUserSO> GuestLogin()
        {
            return GuestController.Login();
        }

        public void GuestLogout(String guestid)
        {
            this.UserController.Logout(guestid);
        }

        public Result<RegisteredUserSO> Register(string email, string password)
        {
            return GuestController.Register(email, password);
        }

        public Result<List<StoreService>> SearchStore(IDictionary<string, object> details)
        {
            return GuestController.SearchStore(details);
        }

        public Result<List<ProductService>> SearchProduct(IDictionary<string, object> details)
        {
            return GuestController.SearchProduct(details);
        }

        public Result AddProductToCart(string userID, string ProductID, int ProductQuantity, string StoreID)
        {
            return UserController.AddProductToCart(userID, ProductID, ProductQuantity, StoreID);
        }

        public Result<ShoppingCartSO> GetUserShoppingCart(string userID)
        {
            return UserController.GetUserShoppingCart(userID);
        }

        public Result UpdateShoppingCart(string userID, string shoppingBagID, string productID, int quantity)
        {
            return UserController.UpdateShoppingCart(userID, shoppingBagID, productID, quantity);
        }

        public Result<ShoppingCartSO> Purchase(string userID, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            return UserController.Purchase(userID, paymentDetails, deliveryDetails);
        }

        public Result<UserHistorySO> GetUserPurchaseHistory(String userid)
        {
            return UserController.GetUserPurchaseHistory(userid);
        }

        public Result<double> GetTotalShoppingCartPrice(string userID)
        {
            return UserController.GetTotalShoppingCartPrice(userID);
        }

        public Result<bool> SendOfferToStore(string storeID, string userID, string productID, int amount, double price)
        {
            return GuestController.SendOfferToStore(storeID, userID, productID, amount, price);
        }

        public Result<bool> AnswerCounterOffer(string userID, string offerID, bool accepted)
        {
            return StoreStaffController.AnswerCounterOffer(userID, offerID, accepted);
        }

        // reg user functionality // search product + search store + sendoffer to store
        public Result<RegisteredUserSO> Login(string email, string password)
        {
            return RegisteredUserController.Login(email, password);
        }

        public Result Logout(string userid)
        {
            return UserController.Logout(userid);
        }

        public Result<StoreService> OpenNewStore(string storename,string userid)
        {
            return RegisteredUserController.OpenNewStore(storename, userid);
        }

        public Result CloseStore(string storeid,string userid)
        {
            return RegisteredUserController.CloseStore(storeid, userid);
        }

        public Result<StoreService> ReOpenStore(string storeid,string userid)
        {
            return RegisteredUserController.ReOpenStore(storeid, userid);
        }

        public Result<LinkedList<Notification>> getUserNotifications(string userid)
        {
            return RegisteredUserController.getUserNotifications(userid);

        }

        // store func

        public Result<String> AddProductToStore(string userID, string storeID, string productName, double price, int initialQuantity, string category, LinkedList<string> keywords = null)
        {
            return this.StoreStaffController.AddProductToStore(userID, storeID, productName, price, initialQuantity, category, keywords);
        }

        public Result RemoveProductFromStore(string userID, string storeID, string productID)
        {
            return StoreStaffController.RemoveProductFromStore(userID, storeID, productID);
        }

        public Result EditProductDetails(string userID, string storeID, string productID, IDictionary<string, object> details)
        {
            return StoreStaffController.EditProductDetails(userID, storeID, productID, details);
        }

        public Result AddStoreOwner(string addedOwnerEmail, string currentlyOwnerID, string storeID)
        {
            return StoreStaffController.AddStoreOwner(addedOwnerEmail, currentlyOwnerID, storeID);
        }

        public Result AddStoreManager(string addedManagerEmail, string currentlyOwnerID, string storeID)
        {
            return StoreStaffController.AddStoreManager(addedManagerEmail, currentlyOwnerID, storeID);
        }

        public Result SetPermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            return StoreStaffController.SetPermissions(storeID, managerID, ownerID, permissions);

        }

        public Result RemovePermissions(string storeID, string managerID, string ownerID, LinkedList<int> permissions)
        {
            return StoreStaffController.RemovePermissions(storeID, managerID, ownerID, permissions);
        }
        public Result<Dictionary<IStaffService, PermissionService>> GetStoreStaff(string ownerID, string storeID)
        {
            return StoreStaffController.GetStoreStaff(ownerID, storeID);
        }

        public Result<UserHistorySO> GetStorePurchaseHistory(string ownerID, string storeID, Boolean isSysAdmin = false)
        {
            return !isSysAdmin ? StoreStaffController.GetStorePurchaseHistory(ownerID, storeID) :
                                this.SystemAdminController.GetStorePurchaseHistory(ownerID, storeID);
        }

        public Result RemoveStoreManager(string removedManagerEmail, string currentlyOwnerID, string storeID)
        {
            return StoreStaffController.RemoveStoreManager(removedManagerEmail, currentlyOwnerID, storeID);
        }

        public Result RemoveStoreOwner(string removedOwnerEmail, string currentlyOwnerID, string storeID)
        {
            return StoreStaffController.RemoveStoreOwner(removedOwnerEmail, currentlyOwnerID, storeID);
        }

        public Result<bool> SendOfferResponseToUser(string storeID, string ownerID, string userID, string offerID, bool accepted, double counterOffer)
        {
            return StoreStaffController.SendOfferResponseToUser(storeID, ownerID, userID, offerID, accepted, counterOffer);
        }

        public Result<List<Dictionary<string, object>>> getStoreOffers(string storeID)
        {
            return StoreStaffController.getStoreOffers(storeID);
        }

        public Result<List<Dictionary<string, object>>> getUserOffers(string userId)
        {
            return StoreStaffController.getUserOffers(userId);
        }

        public Result<bool> AddStoreRating(String userid , String storeid , double rate)
        {
            return RegisteredUserController.AddStoreRating(userid,storeid,rate);
        }

        public Result<bool> AddProductRatingInStore(String userid, String storeid,String productid, double rate)
        {
            return RegisteredUserController.AddProductRatingInStore(userid, storeid,productid, rate);
        }



        // get income?

        // system admin func
        public Result<UserHistorySO> GetUserPurchaseHistory(string sysAdminID, string userID)
        {
            return SystemAdminController.GetUserPurchaseHistory(sysAdminID, userID);
        }

        public Result<RegisteredUserSO> AddSystemAdmin(string sysAdminID, string email)
        {
            return SystemAdminController.AddSystemAdmin(sysAdminID, email);
        }

        public Result<RegisteredUserSO> RemoveSystemAdmin(string sysAdminID, string email)
        {
            return SystemAdminController.RemoveSystemAdmin(sysAdminID, email);

        }

        public Result<bool> ResetSystem(String sysAdminID)
        {
            Result<Boolean> res = SystemAdminController.ResetSystem(sysAdminID);
            if (!res.ErrorOccured)
            {
                this.GuestController = new GuestController(systemFacade);
                this.RegisteredUserController = new RegisteredUserController(systemFacade);
                StoreStaffController = new StoreStaffController(systemFacade);
                this.SystemAdminController = new SystemAdminController(systemFacade);
                this.UserController = new UserController(systemFacade);
            }
            return res;
        }

        public Result<bool> isAdminUser(string userid)
        {
            return UserController.isAdminUser(userid);
        }
        // get income ?

        // display data

        public List<StoreService> GetAllStoresToDisplay()
        {
            return dataController.GetAllStoresToDisplay();
        }

        public List<ProductService> GetAllProductByStoreIDToDisplay(string storeID)
        {
            return dataController.GetAllProductByStoreIDToDisplay(storeID);
        }

        public Boolean[] GetPermission(string userID, string storeID)
        {
            return dataController.GetPermission(userID, storeID);
        }
        // notifications

        /*
         public List<Notification> GetPendingMessagesByUserID(string userId)
        {
            return this.notificationsService.GetPendingMessagesByUserID(userId);
        }
        */

        // policies

        public Result<bool> AddDiscountPolicy(string storeId, Dictionary<string, object> info)
        {
            return StoreStaffController.AddDiscountPolicy(storeId, info);
        }

        public Result<bool> AddDiscountPolicy(string storeId, Dictionary<string, object> info, String id)
        {
            return StoreStaffController.AddDiscountPolicy(storeId, info, id);
        }

        public Result<bool> AddDiscountCondition(string storeId, Dictionary<string, object> info, String id)
        {
            return StoreStaffController.AddDiscountCondition(storeId, info, id);
        }

        public Result<bool> RemoveDiscountPolicy(string storeId, String id)
        {
            return StoreStaffController.RemoveDiscountPolicy(storeId, id);
        }

        public Result<bool> RemoveDiscountCondition(string storeId, String id)
        {
            return StoreStaffController.RemoveDiscountCondition(storeId, id);
        }

        public Result<bool> EditDiscountPolicy(string storeId, Dictionary<string, object> info, String id)
        {
            return StoreStaffController.EditDiscountPolicy(storeId, info, id);
        }

        public Result<bool> EditDiscountCondition(string storeId, Dictionary<string, object> info, String id)
        {
            return StoreStaffController.EditDiscountCondition(storeId, info, id);
        }

        public Result<IDictionary<string, object>> GetDiscountPolicyData(string storeId)
        {
            return StoreStaffController.GetDiscountPolicyData(storeId);
        }

        public Result<IDictionary<string, object>> GetPurchasePolicyData(string storeId)
        {
            return StoreStaffController.GetPurchasePolicyData(storeId);
        }

        public Result<bool> AddPurchasePolicy(string storeId, Dictionary<string, object> info)
        {
            return StoreStaffController.AddPurchasePolicy(storeId, info);
        }

        public Result<bool> AddPurchasePolicy(string storeId, Dictionary<string, object> info, String id)
        {
            return StoreStaffController.AddPurchasePolicy(storeId, info, id);
        }

        public Result<bool> RemovePurchasePolicy(string storeId, String id)
        {
            return StoreStaffController.RemovePurchasePolicy(storeId, id);
        }

        public Result<bool> EditPurchasePolicy(string storeId, Dictionary<string, object> info, string id)
        {
            return StoreStaffController.EditPurchasePolicy(storeId, info, id);
        }

        public Result<bool> isRegisteredUser(string userid)
        {
            return UserController.isRegisteredUser(userid);
        }

        public Result<string> getProductId(string storeid, string productname)
        {
            return UserController.getProductId(storeid, productname);
        }

        public List<StoreService> GetStoresIManage(string userid)
        {
            return RegisteredUserController.GetStoresIManage(userid);
        }

        public List<StoreService> GetStoresIOwn(string userid)
        {
            return RegisteredUserController.GetStoresIOwn(userid);
        }

        public List<ProductService> GetAllProducts()
        {
            return dataController.GetAllProducts();
        }

        public Result<string> getStoreIdByProductId(string productId)
        {
            return UserController.getStoreIdByProductId(productId);
        }

        public Result<string> getUserIdByUsername(string username)
        {
            return UserController.getStoreIdByProductId(username);
        }
    }
}
