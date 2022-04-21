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
        Result<RegisteredUserSO> AddSystemAdmin(String userName);
        Result<RegisteredUserSO> RemoveSystemAdmin(String userName);
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
                UserHistorySO history = SystemFacade.GetUserPurchaseHistory(userId);
                return new Result<UserHistorySO>(history);
            }
            catch (Exception e)
            {
                return new Result<UserHistorySO>(e.Message);
            }
        }
        public Result<RegisteredUserSO> AddSystemAdmin(String userName)
        {
            try
            {
                RegisteredUserSO user = SystemFacade.AddSystemAdmin(userName);
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        public Result<RegisteredUserSO> RemoveSystemAdmin(String userName)
        {
            try
            {
                RegisteredUserSO user = SystemFacade.RemoveSystemAdmin(userName);
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        #endregion

        #region privateMethods
        private void ValidateCredentials(String userName, String password)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("Username is null!");
            }
            if (password == null)
            {
                throw new ArgumentNullException("Password is null!");
            }
        }

        #endregion
    }
}
