using eCommerce.src.DomainLayer;
using eCommerce.src.DomainLayer.User;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface IRegisteredUserController
    {
        Result<RegisteredUserSO> Login(String userName, String password);
        Result<UserHistorySO> GetUserPurchaseHistory(String userId);
        Result OpenNewStore(String storeName, String userId);
        Result CloseStore(string userId, string storeId);
    }

    public class RegisteredUserController : UserController, IRegisteredUserController
    {
        public RegisteredUserController(ISystemFacade systemFacade) : base(systemFacade) { }

        #region RegisteredtUserInterfaceMethods
        public Result<RegisteredUserSO> Login(String userName, String password)
        {
            try
            {
                ValidateCredentials(userName, password);
                RegisteredUserSO user = SystemFacade.Login(userName, password);
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        public Result<UserHistorySO> GetUserPurchaseHistory(String userId)
        {
            try
            {
                ValidateId(userId);
                UserHistorySO history = SystemFacade.GetUserPurchaseHistory(userId);
                return new Result<UserHistorySO>(history);
            }
            catch (Exception e)
            {
                return new Result<UserHistorySO>(e.Message);
            }
        }

        Result IRegisteredUserController.OpenNewStore(string storeName, string userId)
        {
            try
            {
                ValidateId(userId);
                if (storeName == null || storeName.Length == 0)
                    return new Result("Store name can't be null or empty!");
                SystemFacade.OpenNewStore(storeName, userId);
                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }

        Result IRegisteredUserController.CloseStore(string userId, string storeId)
        {
            try
            {
                ValidateId(userId);
                ValidateId(storeId);
                SystemFacade.CloseStore(userId, storeId);
                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }
        #endregion
    }
}
