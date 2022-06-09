using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using eCommerce.src.DataAccessLayer;
using eCommerce.src.DomainLayer;
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

        public eCommerceSystem(String config_path = @"..\eCommerce\Config.json" , string configData = "")
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
            systemFacade = new SystemFacade();
            UserController = new UserController(systemFacade);
            GuestController = new GuestController(systemFacade);
            RegisteredUserController = new RegisteredUserController(systemFacade);
            SystemAdminController = new SystemAdminController(systemFacade);
            StoreStaffController = new StoreStaffController(systemFacade);
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
            return GuestController.AddProductToCart(userID, ProductID, ProductQuantity, StoreID);
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

        // reg user functionality
        public Result<RegisteredUserSO> Login(string email, string password)
        {
            return RegisteredUserController.Login(email, password);
        }

            // guest user login?






    }
}
