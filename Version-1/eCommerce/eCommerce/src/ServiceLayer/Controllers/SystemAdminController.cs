using eCommerce.src.DomainLayer;
using eCommerce.src.ServiceLayer.Objects;
using eCommerce.src.ServiceLayer.ResultService;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.src.ServiceLayer.Controllers
{
    public interface ISystemAdminController
    {
        Result<RegisteredUserSO> AddSystemAdmin(string sysAdminId, String userName);
        Result<RegisteredUserSO> RemoveSystemAdmin(string sysAdminId, String userName);
        Result<UserHistorySO> GetUserPurchaseHistory(string sysAdminId, String userId);
    }
    public class SystemAdminController : RegisteredUserController, ISystemAdminController
    {
        Logger logger = Logger.GetInstance();
        public SystemAdminController(ISystemFacade systemFacade) : base(systemFacade) { }

        #region Public Methods
        public Result<UserHistorySO> GetStorePurchaseHistory(string sysAdminId, string storeId)
        {
            try
            {
                ValidateId(sysAdminId);
                ValidateId(storeId);
                checkSystemAdmin(sysAdminId);
                UserHistorySO history = SystemFacade.GetStorePurchaseHistory(sysAdminId, storeId, true);
                logger.LogInfo($"User with id: {sysAdminId} getting store with id {storeId} purchase history successfully.");
                return new Result<UserHistorySO>(history);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new Result<UserHistorySO>(e.Message);
            }
        }

        public Result<UserHistorySO> GetUserPurchaseHistory(string sysAdminId, string userId)
        {
            try
            {
                ValidateId(sysAdminId);
                ValidateId(userId);
                checkSystemAdmin(sysAdminId);
                UserHistorySO history = SystemFacade.GetUserPurchaseHistory(userId);
                logger.LogInfo($"User with id: {sysAdminId} getting user with id {userId} purchase history successfully.");
                return new Result<UserHistorySO>(history);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new Result<UserHistorySO>(e.Message);
            }
        }

        public Result<RegisteredUserSO> AddSystemAdmin(string sysAdminId, String userName)
        {
            try
            {
                ValidateUserName(userName);
                checkSystemAdmin(sysAdminId);
                RegisteredUserSO user = SystemFacade.AddSystemAdmin(userName);
                logger.LogInfo($"User with id: {sysAdminId} added a new system admin with id: {userName} successfully.");
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        public Result<RegisteredUserSO> RemoveSystemAdmin(string sysAdminId, String userName)
        {
            try
            {
                ValidateUserName(userName);
                checkSystemAdmin(sysAdminId);
                RegisteredUserSO user = SystemFacade.RemoveSystemAdmin(userName);
                logger.LogInfo($"User with id: {sysAdminId} removed a system admin with id {userName} successfully.");
                return new Result<RegisteredUserSO>(user);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new Result<RegisteredUserSO>(e.Message);
            }
        }
        #endregion
        #region private methods
        private void checkSystemAdmin(String userId)
        {
            ValidateId(userId);
            if (!SystemFacade.IsSystemAdmin(userId))
                throw new Exception($"user:{userId} is not system admin!");
        }
        #endregion

    }
}
