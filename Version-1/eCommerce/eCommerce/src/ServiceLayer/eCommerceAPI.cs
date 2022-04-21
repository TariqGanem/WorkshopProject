using System;
using System.Collections.Generic;
using System.Text;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer;
using eCommerce.src.ServiceLayer.Controllers;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;

namespace eCommerce.src.ServiceLayer
{

    public interface IeCommerceAPI : IUserController, IGuestController, IRegisteredUserController /*TODO:, IStoreStaffInterface*/{ }
    public class eCommerceSystem : IeCommerceAPI
    {
        public eCommerceSystem()
        {

        }

        #region User Related Methods
        public Result AddProductToCart(string userId, string productId, int quantity, string storeId)
        {
            throw new NotImplementedException();
        }

        public Result<RegisteredUserSO> AddSystemAdmin(string userName)
        {
            throw new NotImplementedException();
        }

        public Result<double> GetTotalShoppingCartPrice(string userId)
        {
            throw new NotImplementedException();
        }

        public Result<UserHistorySO> GetUserPurchaseHistory(string userId)
        {
            throw new NotImplementedException();
        }

        public Result<ShoppingCartSO> GetUserShoppingCart(string userId)
        {
            throw new NotImplementedException();
        }

        public Result<GuestUserSO> Login()
        {
            throw new NotImplementedException();
        }

        public Result<RegisteredUserSO> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Result Logout(string userId)
        {
            throw new NotImplementedException();
        }

        public Result<ShoppingCartSO> Purchase(string userId, IDictionary<string, object> paymentDetails, IDictionary<string, object> deliveryDetails)
        {
            throw new NotImplementedException();
        }

        public Result<RegisteredUserSO> Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Result<RegisteredUserSO> RemoveSystemAdmin(string userName)
        {
            throw new NotImplementedException();
        }

        public Result UpdateShoppingCart(string userId, string storeId, string productId, int quantity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
