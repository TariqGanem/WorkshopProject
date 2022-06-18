using eCommerce.src.DomainLayer;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IUserController
    {
        Result AddProductToCart(string userId, String productId, int quantity, String storeId);
        Result UpdateShoppingCart(string userId, string storeId, String productId, int quantity);
        Result<ShoppingCartSO> Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails);
        Result<ShoppingCartSO> GetUserShoppingCart(string userId);
        Result<Double> GetTotalShoppingCartPrice(String userId);
        Result Logout(String userId);  
        Result<UserHistorySO> GetUserPurchaseHistory(String userId);
        Result<bool> isAdminUser(string userid);
        Result<bool> isRegisteredUser(string userid);
        Result<string> getProductId(string storeid, string productname);
        Result<string> getStoreIdByProductId(string productId);
        Result<string> getUserIdByUsername(string username);
    }
    public class UserController : IUserController
    {
        public ISystemFacade SystemFacade { set; get; }
        private Logger logger = Logger.GetInstance();
        public UserController(ISystemFacade systemFacade) { SystemFacade = systemFacade; }

        public Result<UserHistorySO> GetUserPurchaseHistory(String userId)
        {
            try
            {
                ValidateId(userId);
                UserHistorySO res = SystemFacade.GetUserPurchaseHistory(userId);
                logger.LogInfo($"UserController --> user history for user {userId} was fetched");
                return new Result<UserHistorySO>(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                logger.LogError("UserController --> " + e.Message);
                return new Result<UserHistorySO>(e.Message);
            }
        }

        public Result AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
            try
            {
                ValidateId(userId);
                ValidateId(productId);
                ValidateId(storeId);
                SystemFacade.AddProductToCart(userId, productId, quantity, storeId);
                logger.LogInfo($"UserController --> Product with id: {productId}, has been added successfully to the cart.");
                return new Result();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("UserController --> " + e.Message);
                logger.LogError("UserController --> " + e.Message);
                return new Result<GuestUserSO>(e.Message);
            }
        }

        public Result<Double> GetTotalShoppingCartPrice(string userId)
        {
            try
            {
                ValidateId(userId);
                Double total = SystemFacade.GetTotalShoppingCartPrice(userId);
                logger.LogInfo($"UserController --> User with id: {userId}, successfully getting the total of his shopping cart price.");
                return new Result<Double>(total);
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result<Double>(e.Message);
            }
        }

        public Result<ShoppingCartSO> GetUserShoppingCart(string userId)
        {
            try
            {
                ValidateId(userId);
                ShoppingCartSO shoppingCart = SystemFacade.GetUserShoppingCart(userId);

                logger.LogInfo($"UserController --> User with id: {userId}, successfully getting his shopping cart.");
                return new Result<ShoppingCartSO>(shoppingCart);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("UserController --> " + e.Message);
                logger.LogError("UserController --> " + e.Message);
                return new Result<ShoppingCartSO>(e.Message);
            }
        }

        public Result<ShoppingCartSO> Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            try
            {
                ValidateId(userId);
                System.Threading.Tasks.Task<ShoppingCartSO> res = SystemFacade.Purchase(userId, paymentDetails, deliveryDetails);
                ShoppingCartSO shoppingCart = res.Result;
                logger.LogInfo($"UserController --> User with id: {userId}, has purchased the items successfully.");
                return new Result<ShoppingCartSO>(shoppingCart);
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result<ShoppingCartSO>(e.Message);
            }
        }

        public Result UpdateShoppingCart(string userId, string storeId, string productId, int quantity)
        {
            try
            {
                ValidateId(userId);
                ValidateId(storeId);
                ValidateId(productId);
                SystemFacade.UpdateShoppingCart(userId, storeId, productId, quantity);
                logger.LogInfo($"UserController --> User with id: {userId}, successfully updated his shopping cart.");
                return new Result();
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result<GuestUserSO>(e.Message);
            }
        }
        public Result Logout(string userId)
        {
            try
            {
                ValidateId(userId);
                SystemFacade.Logout(userId);
                logger.LogInfo($"UserController --> User with id: {userId}, successfully logged out.");
                return new Result();
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result(e.Message);
            }
        }

        public Result<bool> isAdminUser(string userid)
        {
            try
            {
                ValidateId(userid);
                return new Result<bool>(SystemFacade.IsSystemAdmin(userid));
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result<bool>(e.Message);
            }
        }

        public Result<bool> isRegisteredUser(string userid)
        {
            try
            {
                ValidateId(userid);
                return new Result<bool>(SystemFacade.isRegisteredUser(userid));
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result<bool>(e.Message);
            }
        }

        public Result<string> getProductId(string storeid, string productname)
        {
            try
            {
                ValidateId(storeid);
                return new Result<String>(SystemFacade.getProductId(storeid,productname),null);
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result<String>(e.Message);
            }
        }

        public Result<string> getStoreIdByProductId(string productId)
        {
            try
            {
                ValidateId(productId);
                return new Result<String>(SystemFacade.getStoreIdByProductId(productId),null);
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result<String>(e.Message);
            }
        }

        public Result<string> getUserIdByUsername(string username)
        {
            try
            {
                return new Result<String>(SystemFacade.getUserIdByUsername(username),null);
            }
            catch (Exception e)
            {
                logger.LogError("UserController --> " + e.Message);
                return new Result<String>(e.Message);
            }
        }



        #region Protected Validation Methods
        protected void ValidateCredentials(String userName, String password)
        {
            ValidateUserName(userName);
            if (password == null || password.Length == 0)
            {
                throw new ArgumentNullException("Password is null or empty!");
            }
        }

        protected void ValidateUserName(String userName)
        {
            if (userName == null || userName.Length == 0)
            {
                throw new ArgumentNullException("Username is null or empty!");
            }
        }
        protected void ValidateId(String userId)
        {
            if (userId == null || userId.Length == 0)
            {
                throw new ArgumentNullException("UserId is null or empty!");
            }
        }
        #endregion
    }


    public interface IGuestController
    {
        Result<GuestUserSO> Login();
        Result<RegisteredUserSO> Register(string username, string password);
        Result<List<StoreService>> SearchStore(IDictionary<String, Object> details);
        Result<List<ProductService>> SearchProduct(IDictionary<string, object> details);
        Result AddProductToCart(string userID, string ProductID, int ProductQuantity, string StoreID);
        Result<ShoppingCartSO> GetUserShoppingCart(string userID);
        Result<bool> SendOfferToStore(string storeID, string userID, string productID, int amount, double price);


    }
    public class GuestController : UserController, IGuestController
    {
        Logger logger = Logger.GetInstance();
        public GuestController(ISystemFacade systemFacade) : base(systemFacade) { }

        #region GuestControllerMethods
        public Result<GuestUserSO> Login()
        {
            try
            {
                return new Result<GuestUserSO>(SystemFacade.Login());
            }
            catch (Exception e)
            {
                logger.LogError("GuestController --> " + e.Message);
                return new Result<GuestUserSO>(e.Message);
            }
        }

        public Result<bool> SendOfferToStore(string storeID, string userID, string productID, int amount, double price)
        {
            try
            {
                ValidateId(userID);
                ValidateId(storeID);
                ValidateId(productID);
                bool res = this.SystemFacade.SendOfferToStore(storeID, userID, productID, amount, price);
                return new Result<bool>(res);
            }
            catch (Exception ex)
            {
                return new Result<bool>(ex.Message);
            }
        }

        public Result<RegisteredUserSO> Register(string username, string password)
        {
            try
            {
                ValidateCredentials(username, password);
                RegisteredUserSO user = SystemFacade.Register(username, password);
                logger.LogInfo($"GuestController --> A new user has been registered to the system with id: {username}.");
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                logger.LogError("GuestController --> " + e.Message);
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        public Result<List<ProductService>> SearchProduct(IDictionary<string, object> details)
        {
            try
            {
                return new Result<List<ProductService>>(this.SystemFacade.SearchProduct(details));
            }
            catch(Exception e)
            {
                logger.LogError("GuestController --> " + e.Message);
                return new Result<List<ProductService>>(e.Message);
            }
        }

        public Result<List<StoreService>> SearchStore(IDictionary<String, Object> details) {
            try
            {
                return new Result<List<StoreService>>(SystemFacade.SearchStore(details));
            }
            catch(Exception e)
            {
                logger.LogError("GuestController --> " + e.Message);
                return new Result<List<StoreService>>(e.Message);
            }
        }

        Result AddProductToCart(string userID, string ProductID, int ProductQuantity, string StoreID)
        {
            try
            {
                ValidateId(userID);
                ValidateId(ProductID);
                ValidateId(StoreID);
                SystemFacade.AddProductToCart(userID, ProductID, ProductQuantity, StoreID);
                logger.LogInfo($"GuestController --> Product with id: {ProductID}, has been added successfully to the cart.");
                return new Result();
            }
            catch (Exception e)
            {
                logger.LogError("GuestController --> " + e.Message);
                return new Result<GuestUserSO>(e.Message);
            }
        }


        #endregion
    }
}