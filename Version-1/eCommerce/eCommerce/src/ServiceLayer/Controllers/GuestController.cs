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
    public abstract class UserController : IUserController
    {
        protected ISystemFacade SystemFacade;
        protected UserController(ISystemFacade systemFacade) { SystemFacade = systemFacade; }

        public Result AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
            try
            {
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
            if (userId == null || userId == "")
                return new Result("The userId is empty!!!");
            try
            {
                SystemFacade.Logout(userId);
                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }
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
            if (username == null || username == "")
                return new Result<RegisteredUserSO>("The username is invalid!!!");
            if (password == null || password == "")
                return new Result<RegisteredUserSO>("The password is invalid!!!");
            try
            {
                RegisteredUserSO user = SystemFacade.Register(username, password);
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        #endregion
    }
}
