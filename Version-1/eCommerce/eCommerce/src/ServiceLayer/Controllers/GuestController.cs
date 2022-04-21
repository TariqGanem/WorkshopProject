using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;
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
    }
    public class UserController : IUserController
    {
        public ISystemFacade SystemFacade;
        public UserController(ISystemFacade systemFacade) { SystemFacade = systemFacade; }

        public Result AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
<<<<<<< HEAD
            SystemFacade = systemFacade;
        }
        #endregion
        /* 
        #region GuestControllerMethods
               public Response<guestUser> EnterSystem()
                {
                    GuestUser output = SystemFacade.Login();
                    guestUser guestUser = new guestUser(output);
                    return new Response<guestUser>(guestUser,null);
                }

                public Response<string> ExitSystem(string userID)
                {
                    if (userID == null || userID == "")
                        return new Response<string>("the userId is empty!!!");
                    SystemFacade.ExitSystem(userID);
                    return new Response<string>(null);
                }*/

        /*
        public Response<registeredUser> Register(string username, string email, string password)
        {
            if (username == null || username == "")
                return new Response<registeredUser>("The username is invalid!!!");
            if (email == null || email == "")
                return new Response<registeredUser>("The email is invalid!!!");
            if (password == null || password == "")
                return new Response<registeredUser>("The password is invalid!!!");
=======
>>>>>>> dev1-Version1
            try
            {
                ValidateId(userId);
                ValidateId(productId);
                ValidateId(storeId);
                SystemFacade.AddProductToCart(userId, productId, quantity, storeId);
                return new Result();
            }
            catch (Exception e)
            {
                return new Result<GuestUserSO>(e.Message);
            }
        }

        public Result<Double> GetTotalShoppingCartPrice(string userId)
        {
            try
            {
                ValidateId(userId);
                Double total = SystemFacade.GetTotalShoppingCartPrice(userId);
                return new Result<Double>(total);
            }
            catch (Exception e)
            {
                return new Result<Double>(e.Message);
            }
        }

        public Result<ShoppingCartSO> GetUserShoppingCart(string userId)
        {
            try
            {
                ValidateId(userId);
                ShoppingCartSO shoppingCart = SystemFacade.GetUserShoppingCart(userId);
                return new Result<ShoppingCartSO>(shoppingCart);
            }
            catch (Exception e)
            {
                return new Result<ShoppingCartSO>(e.Message);
            }
        }

        public Result<ShoppingCartSO> Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            try
            {
                ValidateId(userId);
                ShoppingCartSO shoppingCart = SystemFacade.Purchase(userId, paymentDetails, deliveryDetails);
                return new Result<ShoppingCartSO>(shoppingCart);
            }
            catch (Exception e)
            {
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
                return new Result();
            }
            catch (Exception e)
            {
                return new Result<GuestUserSO>(e.Message);
            }
        }
        public Result Logout(string userId)
        {
            try
            {
                ValidateId(userId);
                SystemFacade.Logout(userId);
                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
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

    }

    public class GuestController : UserController, IGuestController
    {

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
                return new Result<GuestUserSO>(e.Message);
            }
        }

        public Result<RegisteredUserSO> Register(string username, string password)
        {
            try
            {
                ValidateCredentials(username, password);
                RegisteredUserSO user = SystemFacade.Register(username, password);
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        #endregion
        */
    }
}